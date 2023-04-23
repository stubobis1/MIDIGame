using Minis;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

// NoteCallback.cs - This script shows how to define a callback to get notified
// on MIDI note-on/off events.
sealed class MidiController : MonoBehaviour
{
    public static MidiDevice CurrentMidiDevice;
    public static MidiController Instance;


    public static Action<MidiNoteControl, float> NoteOnActions;
    public static Action<MidiNoteControl> NoteOffActions;
    public static Action<MidiValueControl, float> ControlChangeActions;

    public bool debug = true;


    public MidiController()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
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
                CurrentMidiDevice = midiDevice;
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

            

            midiDevice.onWillNoteOn += NoteOnActions;
            midiDevice.onWillNoteOff += NoteOffActions;
            midiDevice.onWillControlChange += ControlChangeActions;
        };
    }

}