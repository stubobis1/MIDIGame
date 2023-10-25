using Minis;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// NoteCallback.cs - This script shows how to define a callback to get notified
// on MIDI note-on/off events.
sealed class MidiController : MonoBehaviour
{
    public MidiDevice CurrentMidiDevice;
    public static MidiController Instance;


    public static event Action<MidiNoteControl, float> NoteOnActions
    {
        // Action list lazy allocation
        add => (_NoteOnActions = _NoteOnActions ??
                new List<Action<MidiNoteControl, float>>()).Add(value);
        remove => _NoteOnActions.Remove(value);
    }
    static List<Action<MidiNoteControl, float>> _NoteOnActions;


    public static event Action<MidiNoteControl> NoteOffActions
    {
        // Action list lazy allocation
        add => (_NoteOffActions = _NoteOffActions ??
                new List<Action<MidiNoteControl>>()).Add(value);
        remove => _NoteOffActions.Remove(value);
    }
    static List<Action<MidiNoteControl>> _NoteOffActions;

    //public static Action<MidiValueControl, float> ControlChangeActions;

    public bool debug = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one MidiController!");
        }

        Instance = this;

        _NoteOffActions = new();
        _NoteOnActions = new();
    }

    private void OnDestroy()
    {
        if (CurrentMidiDevice != null)
        {
            CurrentMidiDevice.onWillNoteOn -= doOnActions;
            CurrentMidiDevice.onWillNoteOff -= doOffActions;
        }
    }
    void Start()
    {
        var usedMidi = InputSystem.GetDevice<MidiDevice>();
        if (usedMidi != null)
        {
            CurrentMidiDevice = usedMidi;
            CurrentMidiDevice.onWillNoteOn += doOnActions;
            CurrentMidiDevice.onWillNoteOff += doOffActions;
        }
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change != InputDeviceChange.Added) return;

            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null)
            {
                return;
            }
            else
            {
                if (CurrentMidiDevice != null)
                {
                    CurrentMidiDevice.onWillNoteOn -= doOnActions;
                    CurrentMidiDevice.onWillNoteOff -= doOffActions;
                }
                CurrentMidiDevice = midiDevice;
                CurrentMidiDevice.onWillNoteOn += doOnActions;
                CurrentMidiDevice.onWillNoteOff += doOffActions;
            }

            #region debug print actions
            if (debug)
            {
                midiDevice.onWillNoteOn += (note, velocity) =>
                {
                    // Note that you can't use note.velocity because the state
                    // hasn't been updated yet (as this is "will" event). The note
                    // object is only useful to specify the target note (note
                    // number, channel number, device name, etc.) Use the velocity
                    // argument as an input note velocity.
                    Debug.Log(string.Format(
                        "Note On #{0} ({1}) vel:{2:0.00} ch:{3} dev:'{4}'",
                        note.noteNumber,
                        note.shortDisplayName,
                        velocity,
                        (note.device as Minis.MidiDevice)?.channel,
                        note.device.description.product
                    ));
                    //Debug.Log($"Number of subscribed actions {midiDevice.willNoteOnActionList.Count}, {midiDevice.willNoteOffActionList.Count}");

                };

                midiDevice.onWillNoteOff += (note) =>
                {
                    Debug.Log(string.Format(
                        "Note Off #{0} ({1}) ch:{2} dev:'{3}'",
                        note.noteNumber,
                        note.shortDisplayName,
                        (note.device as Minis.MidiDevice)?.channel,
                        note.device.description.product
                    ));
                };
            }
            #endregion
             
           
            //midiDevice.onWillControlChange += ControlChangeActions;
        };
      
    }


    void doOnActions(MidiNoteControl note, float velo)
    {
        _NoteOnActions.ForEach(z => z(note, velo));
    }
    private void doOffActions(MidiNoteControl obj)
    {
        _NoteOffActions.ForEach(z => z(obj));
    }

}