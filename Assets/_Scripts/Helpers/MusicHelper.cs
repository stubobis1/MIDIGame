using Minis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class MusicHelper 
{
    public static MidiDevice CurrentMidiDevice;
    


    public static Action<MidiNoteControl, float> NoteOnActions;
    public static Action<MidiNoteControl> NoteOffActions;
    public static Action<MidiValueControl, float> ControlChangeActions;

    public static bool debugOnPress = true;

    public static string RemoveSharpFromNoteName(string noteName)
    {
        return noteName.Replace("#", "");
    }

    #region setup, used in midicontroller
    /// <summary>
    /// Run this on start() -- 
    /// </summary>
    //public static void SetupActions()
    //{
    //    InputSystem.onDeviceChange += (device, change) =>
    //    {
    //        if (change != InputDeviceChange.Added) return;

    //        var midiDevice = device as Minis.MidiDevice;
    //        if (midiDevice == null)
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            CurrentMidiDevice = midiDevice;
    //        }

    //        #region debug print actions
    //        if (debugOnPress)
    //        {
    //            midiDevice.onWillNoteOn += (note, velocity) =>
    //            {
    //                // Note that you can't use note.velocity because the state
    //                // hasn't been updated yet (as this is "will" event). The note
    //                // object is only useful to specify the target note (note
    //                // number, channel number, device name, etc.) Use the velocity
    //                // argument as an input note velocity.
    //                Debug.Log(string.Format(
    //                    "Note On #{0} ({1}) vel:{2:0.00} ch:{3} dev:'{4}'",
    //                    note.noteNumber,
    //                    note.shortDisplayName,
    //                    velocity,
    //                    (note.device as Minis.MidiDevice)?.channel,
    //                    note.device.description.product
    //                ));


    //            };

    //            midiDevice.onWillNoteOff += (note) =>
    //            {
    //                Debug.Log(string.Format(
    //                    "Note Off #{0} ({1}) ch:{2} dev:'{3}'",
    //                    note.noteNumber,
    //                    note.shortDisplayName,
    //                    (note.device as Minis.MidiDevice)?.channel,
    //                    note.device.description.product
    //                ));
    //            };
    //        }
    //        #endregion

    //        midiDevice.onWillNoteOn += NoteOnActions;
    //        midiDevice.onWillNoteOff += NoteOffActions;
    //        midiDevice.onWillControlChange += ControlChangeActions;
    //    };
    //}
    #endregion

    public static T[] CopyAndReverseArray<T>(T[]  scale)
    {
        var longScale = new T[scale.Length * 2];
        scale.CopyTo(longScale, 0);
        Array.Reverse(longScale);
        scale.CopyTo(longScale, 0);
        return longScale;
    }

    #region Scales

    // Major scales
    public readonly static string[] cMajor = { "C4", "D4", "E4", "F4", "G4", "A4", "B4", "C5" };
    public readonly static string[] cSharpMajor = { "C#4", "D#4", "F4", "F#4", "G#4", "A#4", "C5", "C#5" };
    public readonly static string[] dMajor = { "D4", "E4", "F#4", "G4", "A4", "B4", "C#5", "D5" };
    public readonly static string[] dSharpMajor = { "D#4", "F4", "G4", "G#4", "A#4", "C5", "D5", "D#5" };
    public readonly static string[] eMajor = { "E4", "F#4", "G#4", "A4", "B4", "C#5", "D#5", "E5" };
    public readonly static string[] fMajor = { "F4", "G4", "A4", "A#4", "C5", "D5", "E5", "F5" };
    public readonly static string[] fSharpMajor = { "F#4", "G#4", "A#4", "B4", "C#5", "D#5", "F5", "F#5" };
    public readonly static string[] gMajor = { "G4", "A4", "B4", "C5", "D5", "E5", "F#5", "G5" };
    public readonly static string[] gSharpMajor = { "G#4", "A#4", "C5", "C#5", "D#5", "F5", "G5", "G#5" };
    public readonly static string[] aMajor = { "A4", "B4", "C#5", "D5", "E5", "F#5", "G#5", "A5" };
    public readonly static string[] aSharpMajor = { "A#4", "C5", "D5", "D#5", "F5", "G5", "A5", "A#5" };
    public readonly static string[] bMajor = { "B4", "C#5", "D#5", "E5", "F#5", "G#5", "A#5", "B5" };

    // Minor scales
    public readonly static string[] cMinor = { "C4", "D4", "D#4", "F4", "G4", "G#4", "A#4", "C5" };
    public readonly static string[] cSharpMinor = { "C#4", "D#4", "E4", "F#4", "G#4", "A4", "B4", "C#5" };
    public readonly static string[] dMinor = { "D4", "E4", "F4", "G4", "A4", "A#4", "C5", "D5" };
    public readonly static string[] dSharpMinor = { "D#4", "F4", "F#4", "G#4", "A#4", "B4", "C#5", "D#5" };
    public readonly static string[] eMinor = { "E4", "F#4", "G4", "A4", "B4", "C5", "D5", "E5" };
    public readonly static string[] fMinor = { "F4", "G4", "G#4", "A#4", "C5", "C#5", "D#5", "F5" };
    public readonly static string[] fSharpMinor = { "F#4", "G#4", "A4", "B4", "C#5", "D5", "E5", "F#5" };
    public readonly static string[] gMinor = { "G4", "A4", "A#4", "C5", "D5", "D#5", "F5", "G5" };
    public readonly static string[] gSharpMinor = { "G#4", "A#4", "B4", "C#5", "D#5", "E5", "F#5", "G#5" };
    public readonly static string[] aMinor = { "A4", "B4", "C5", "D5", "E5", "F5", "G5", "A5" };
    public readonly static string[] aSharpMinor = { "A#4", "C5", "C#5", "D#5", "F5", "F#5", "G#5", "A#5" };
    public readonly static string[] bMinor = { "B4", "C#5", "D5", "E5", "F#5", "G5", "A5", "B5" };

    // Major Pentatonic scales
    public readonly static string[] cMajorPentatonic = { "C4", "D4", "E4", "G4", "A4", "C5" };
    public readonly static string[] cSharpMajorPentatonic = { "C#4", "D#4", "F4", "G#4", "A#4", "C#5" };
    public readonly static string[] dMajorPentatonic = { "D4", "E4", "F#4", "A4", "B4", "D5" };
    public readonly static string[] dSharpMajorPentatonic = { "D#4", "F4", "G4", "A#4", "C5", "D#5" };
    public readonly static string[] eMajorPentatonic = { "E4", "F#4", "G#4", "B4", "C#5", "E5" };
    public readonly static string[] fMajorPentatonic = { "F4", "G4", "A4", "C5", "D5", "F5" };
    public readonly static string[] fSharpMajorPentatonic = { "F#4", "G#4", "A#4", "C#5", "D#5", "F#5" };
    public readonly static string[] gMajorPentatonic = { "G4", "A4", "B4", "D5", "E5", "G5" };
    public readonly static string[] gSharpMajorPentatonic = { "G#4", "A#4", "C5", "D#5", "F5", "G#5" };
    public readonly static string[] aMajorPentatonic = { "A4", "B4", "C#5", "E5", "F#5", "A5" };
    public readonly static string[] aSharpMajorPentatonic = { "A#4", "C5", "D5", "F5", "G5", "A#5" };
    public readonly static string[] bMajorPentatonic = { "B4", "C#5", "D#5", "F#5", "G#5", "B5" };

    // Minor Pentaptonic scales
    public readonly static string[] cMinorPentatonic = { "C4", "D#4", "F4", "G4", "A#4", "C5" };
    public readonly static string[] cSharpMinorPentatonic = { "C#4", "E4", "F#4", "G#4", "B4", "C#5" };
    public readonly static string[] dMinorPentatonic = { "D4", "F4", "G4", "A4", "C5", "D5" };
    public readonly static string[] dSharpMinorPentatonic = { "D#4", "F#4", "G#4", "A#4", "C#5", "D#5" };
    public readonly static string[] eMinorPentatonic = { "E4", "G4", "A4", "B4", "D5", "E5" };
    public readonly static string[] fMinorPentatonic = { "F4", "G#4", "A#4", "C5", "D#5", "F5" };
    public readonly static string[] fSharpMinorPentatonic = { "F#4", "A4", "B4", "C#5", "E5", "F#5" };
    public readonly static string[] gMinorPentatonic = { "G4", "A#4", "C5", "D5", "F5", "G5" };
    public readonly static string[] gSharpMinorPentatonic = { "G#4", "B4", "C#5", "D#5", "F#5", "G#5" };
    public readonly static string[] aMinorPentatonic = { "A4", "C5", "D5", "E5", "G5", "A5" };
    public readonly static string[] aSharpMinorPentatonic = { "A#4", "C#5", "D#5", "F5", "G#5", "A#5" };
    public readonly static string[] bMinorPentatonic = { "B4", "D5", "E5", "F#5", "A5", "B5" };

    #endregion
}
