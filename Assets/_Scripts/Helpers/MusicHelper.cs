using Minis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class MusicHelper 
{
    
    public static MidiDevice CurrentMidiDevice;

    public static Dictionary<MusicBeatValue, float> ValueBeatLengthInBeats = new Dictionary<MusicBeatValue, float>()
    { //We are atm only doing 4/4 time, we could use the conductor's beats per measure to modify these
        {MusicBeatValue.whole,     4f},
        {MusicBeatValue.dottedHalf, 3f},
        {MusicBeatValue.half, 2f},
        {MusicBeatValue.dottedQuater, 1.5f},
        {MusicBeatValue.quarter, 1f},
        {MusicBeatValue.dottedEighth, .75f},
        {MusicBeatValue.eighth, .5f},
        {MusicBeatValue.sixteenth, .25f},
        {MusicBeatValue.triplet, 1f/3f} //yikes

    };

    public static Action<MidiNoteControl, float> NoteOnActions;
    public static Action<MidiNoteControl> NoteOffActions;
    public static Action<MidiValueControl, float> ControlChangeActions;

    public static bool debugOnPress = true;

    public static string RemoveSharpFromNoteName(string noteName)
    {
        return noteName.Replace("#", "");
    }

    public static T[] CopyAndReverseArray<T>(T[]  scale)
    {
        var longScale = new T[scale.Length * 2];
        scale.CopyTo(longScale, 0);
        Array.Reverse(longScale);
        scale.CopyTo(longScale, 0);
        return longScale;
    }

    #region Scales and Chords

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

    // Major Chords
    public readonly static string[] cMajorChord = { "C", "E", "G" };
    public readonly static string[] cSharpMajorChord = { "C#", "F", "G#" };
    public readonly static string[] dMajorChord = { "D", "F#", "A" };
    public readonly static string[] dSharpMajorChord = { "D#", "G", "A#" };
    public readonly static string[] eMajorChord = { "E", "G#", "B" };
    public readonly static string[] fMajorChord = { "F", "A", "C" };
    public readonly static string[] fSharpMajorChord = { "F#", "A#", "C#" };
    public readonly static string[] gMajorChord = { "G", "B", "D" };
    public readonly static string[] gSharpMajorChord = { "G#", "C", "D#" };
    public readonly static string[] aMajorChord = { "A", "C#", "E" };
    public readonly static string[] aSharpMajorChord = { "A#", "D", "F" };
    public readonly static string[] bMajorChord = { "B", "D#", "F#" };

    // Minor Chords
    public readonly static string[] cMinorChord = { "C", "D#", "G" };
    public readonly static string[] cSharpMinorChord = { "C#", "E", "G#" };
    public readonly static string[] dMinorChord = { "D", "F", "A" };
    public readonly static string[] dSharpMinorChord = { "D#", "F#", "A#" };
    public readonly static string[] eMinorChord = { "E", "G", "B" };
    public readonly static string[] fMinorChord = { "F", "G#", "C" };
    public readonly static string[] fSharpMinorChord = { "F#", "A", "C#" };
    public readonly static string[] gMinorChord = { "G", "A#", "D" };
    public readonly static string[] gSharpMinorChord = { "G#", "B", "D#" };
    public readonly static string[] aMinorChord = { "A", "C", "E" };
    public readonly static string[] aSharpMinorChord = { "A#", "C#", "F" };
    public readonly static string[] bMinorChord = { "B", "D", "F#" };

    // Diminished Chords
    public readonly static string[] cDimChord = { "C", "D#", "F#" };
    public readonly static string[] cSharpDimChord = { "C#", "E", "G" };
    public readonly static string[] dDimChord = { "D", "F", "G#" };
    public readonly static string[] dSharpDimChord = { "D#", "F#", "A" };
    public readonly static string[] eDimChord = { "E", "G", "A#" };
    public readonly static string[] fDimChord = { "F", "G#", "B" };
    public readonly static string[] fSharpDimChord = { "F#", "A", "C" };
    public readonly static string[] gDimChord = { "G", "A#", "C#" };
    public readonly static string[] gSharpDimChord = { "G#", "B", "D" };
    public readonly static string[] aDimChord = { "A", "C", "D#" };
    public readonly static string[] aSharpDimChord = { "A#", "C#", "E" };
    public readonly static string[] bDimChord = { "B","D","F" };

    // Dominant 7th Chords
    public readonly static string[] cDom7Chord = { "C", "E", "G", "A#" };
    public readonly static string[] cSharpDom7Chord = { "C#", "F", "G#", "B" };
    public readonly static string[] dDom7Chord = { "D", "F#", "A", "C" };
    public readonly static string[] dSharpDom7Chord = { "D#", "G", "A#", "C#" };
    public readonly static string[] eDom7Chord = { "E", "G#", "B", "D" };
    public readonly static string[] fDom7Chord = { "F", "A", "C", "D#" };
    public readonly static string[] fSharpDom7Chord = { "F#", "B", "C#", "E" };
    public readonly static string[] gDom7Chord = { "G", "B", "D", "F" };
    public readonly static string[] gSharpDom7Chord = { "G#", "C", "D#", "F#" };
    public readonly static string[] aDom7Chord = { "A", "C#", "E", "G" };
    public readonly static string[] aSharpDom7Chord = { "A#", "D", "F", "G#" };
    public readonly static string[] bDom7Chord = { "B", "D#", "F#", "A" };

    public readonly static Dictionary<string, string[]> Chords = new Dictionary<string, string[]>() {
        // Major
        { "C", cMajorChord },
        { "C#", cSharpMajorChord },
        { "D", dMajorChord },
        { "D#", dSharpMajorChord },
        { "E", eMajorChord },
        { "F", fMajorChord },
        { "F#", fSharpMajorChord },
        { "G", gMajorChord },
        { "G#", gSharpMajorChord },
        { "A", aMajorChord },
        { "A#", aSharpMajorChord },
        { "B", bMajorChord },

        // Minor
        { "Cm", cMinorChord},
        { "C#m", cSharpMinorChord},
        { "Dm", dMinorChord},
        { "D#m", dSharpMinorChord},
        { "Em", eMinorChord},
        { "Fm", fMinorChord},
        { "F#m", fSharpMinorChord},
        { "Gm", gMinorChord},
        { "G#m", gSharpMinorChord},
        { "Am", aMinorChord},
        { "A#m", aSharpMinorChord},
        { "Bm", bMinorChord},

        // Diminished
        { "Cdim", cDimChord },
        { "C#dim", cSharpDimChord },
        { "Ddim", dDimChord },
        { "D#dim", dSharpDimChord },
        { "Edim", eDimChord },
        { "Fdim", fDimChord },
        { "F#dim", fSharpDimChord },
        { "Gdim", gDimChord },
        { "G#dim", gSharpDimChord },
        { "Adim", aDimChord },
        { "A#dim", aSharpDimChord },
        { "Bdim", bDimChord },

        // Dominant 7th
        {"C7", cDom7Chord },
        {"C#7", cSharpDom7Chord },
        {"D7", dDom7Chord },
        {"D#7", dSharpDom7Chord },
        {"E7", eDom7Chord },
        {"F7", fDom7Chord },
        {"F#7", fSharpDom7Chord },
        {"G7", gDom7Chord },
        {"G#7", gSharpDom7Chord },
        {"A7", aDom7Chord },
        {"A#7", aSharpDom7Chord },
        {"B7", bDom7Chord }
    };
    #endregion
}


public enum MusicBeatValue 
{
    whole, 
    dottedHalf, 
    half, 
    dottedQuater, 
    quarter, 
    dottedEighth, 
    eighth, 
    sixteenth, 
    triplet
}
