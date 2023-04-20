using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleGame : MonoBehaviour
{
    public ScrollingNote Note;
    public float scrollSpeed = 5f;
    public float spaceBetweenNotes = 1f;
    public float spaceBeforeStartNote = 1f;

    #region Scales

    // Major scales
    public string[] cMajor = { "C4", "D4", "E4", "F4", "G4", "A4", "B4", "C5" };
    public string[] cSharpMajor = { "C#4", "D#4", "F4", "F#4", "G#4", "A#4", "C5", "C#5" };
    public string[] dMajor = { "D4", "E4", "F#4", "G4", "A4", "B4", "C#5", "D5" };
    public string[] dSharpMajor = { "D#4", "F4", "G4", "G#4", "A#4", "C5", "D5", "D#5" };
    public string[] eMajor = { "E4", "F#4", "G#4", "A4", "B4", "C#5", "D#5", "E5" };
    public string[] fMajor = { "F4", "G4", "A4", "A#4", "C5", "D5", "E5", "F5" };
    public string[] fSharpMajor = { "F#4", "G#4", "A#4", "B4", "C#5", "D#5", "F5", "F#5" };
    public string[] gMajor = { "G4", "A4", "B4", "C5", "D5", "E5", "F#5", "G5" };
    public string[] gSharpMajor = { "G#4", "A#4", "C5", "C#5", "D#5", "F5", "G5", "G#5" };
    public string[] aMajor = { "A4", "B4", "C#5", "D5", "E5", "F#5", "G#5", "A5" };
    public string[] aSharpMajor = { "A#4", "C5", "D5", "D#5", "F5", "G5", "A5", "A#5" };
    public string[] bMajor = { "B4", "C#5", "D#5", "E5", "F#5", "G#5", "A#5", "B5" };

    // Minor scales
    public string[] cMinor = { "C4", "D4", "D#4", "F4", "G4", "G#4", "A#4", "C5" };
    public string[] cSharpMinor = { "C#4", "D#4", "E4", "F#4", "G#4", "A4", "B4", "C#5" };
    public string[] dMinor = { "D4", "E4", "F4", "G4", "A4", "A#4", "C5", "D5" };
    public string[] dSharpMinor = { "D#4", "F4", "F#4", "G#4", "A#4", "B4", "C#5", "D#5" };
    public string[] eMinor = { "E4", "F#4", "G4", "A4", "B4", "C5", "D5", "E5" };
    public string[] fMinor = { "F4", "G4", "G#4", "A#4", "C5", "C#5", "D#5", "F5" };
    public string[] fSharpMinor = { "F#4", "G#4", "A4", "B4", "C#5", "D5", "E5", "F#5" };
    public string[] gMinor = { "G4", "A4", "A#4", "C5", "D5", "D#5", "F5", "G5" };
    public string[] gSharpMinor = { "G#4", "A#4", "B4", "C#5", "D#5", "E5", "F#5", "G#5" };
    public string[] aMinor = { "A4", "B4", "C5", "D5", "E5", "F5", "G5", "A5" };
    public string[] aSharpMinor = { "A#4", "C5", "C#5", "D#5", "F5", "F#5", "G#5", "A#5" };
    public string[] bMinor = { "B4", "C#5", "D5", "E5", "F#5", "G5", "A5", "B5" };

    // Major Pentatonic scales
    public string[] cMajorPentatonic = { "C4", "D4", "E4", "G4", "A4", "C5" };
    public string[] cSharpMajorPentatonic = { "C#4", "D#4", "F4", "G#4", "A#4", "C#5" };
    public string[] dMajorPentatonic = { "D4", "E4", "F#4", "A4", "B4", "D5" };
    public string[] dSharpMajorPentatonic = { "D#4", "F4", "G4", "A#4", "C5", "D#5" };
    public string[] eMajorPentatonic = { "E4", "F#4", "G#4", "B4", "C#5", "E5" };
    public string[] fMajorPentatonic = { "F4", "G4", "A4", "C5", "D5", "F5" };
    public string[] fSharpMajorPentatonic = { "F#4", "G#4", "A#4", "C#5", "D#5", "F#5" };
    public string[] gMajorPentatonic = { "G4", "A4", "B4", "D5", "E5", "G5" };
    public string[] gSharpMajorPentatonic = { "G#4", "A#4", "C5", "D#5", "F5", "G#5" };
    public string[] aMajorPentatonic = { "A4", "B4", "C#5", "E5", "F#5", "A5" };
    public string[] aSharpMajorPentatonic = { "A#4", "C5", "D5", "F5", "G5", "A#5" };
    public string[] bMajorPentatonic = { "B4", "C#5", "D#5", "F#5", "G#5", "B5" };

    // Minor Pentaptonic scales
    public string[] cMinorPentatonic = { "C4", "D#4", "F4", "G4", "A#4", "C5" };
    public string[] cSharpMinorPentatonic = { "C#4", "E4", "F#4", "G#4", "B4", "C#5" };
    public string[] dMinorPentatonic = { "D4", "F4", "G4", "A4", "C5", "D5" };
    public string[] dSharpMinorPentatonic = { "D#4", "F#4", "G#4", "A#4", "C#5", "D#5" };
    public string[] eMinorPentatonic = { "E4", "G4", "A4", "B4", "D5", "E5" };
    public string[] fMinorPentatonic = { "F4", "G#4", "A#4", "C5", "D#5", "F5" };
    public string[] fSharpMinorPentatonic = { "F#4", "A4", "B4", "C#5", "E5", "F#5" };
    public string[] gMinorPentatonic = { "G4", "A#4", "C5", "D5", "F5", "G5" };
    public string[] gSharpMinorPentatonic = { "G#4", "B4", "C#5", "D#5", "F#5", "G#5" };
    public string[] aMinorPentatonic = { "A4", "C5", "D5", "E5", "G5", "A5" };
    public string[] aSharpMinorPentatonic = { "A#4", "C#5", "D#5", "F5", "G#5", "A#5" };
    public string[] bMinorPentatonic = { "B4", "D5", "E5", "F#5", "A5", "B5" };

    #endregion

    public List<GameObject> Notes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        


      

        GenerateNotesForScale(UpAndDownScale(dSharpMinorPentatonic));
    }

    private string[] UpAndDownScale(string[] scale)
    {
        var longScale = new string[scale.Length * 2];
        scale.CopyTo(longScale, 0);
        Array.Reverse(scale);
        scale.CopyTo(longScale, scale.Length);
        return longScale;
    }
    private void GenerateNotesForScale(string[] scale)
    {
        for (int i = 0; i < scale.Length; i++)
        {
            var noteName = scale[i];
            
            var notePos = MIDIHelper.RemoveSharpFromNoteName(noteName);

            var obj = Instantiate(Note.gameObject);
            obj.SetActive(true);
            obj.transform.position = new Vector3(
                spaceBeforeStartNote + (i * spaceBetweenNotes),
                GlobalSingletonCleftPositions.Instance.Positions[notePos].position.y,
                Note.gameObject.transform.position.z
                ) ;

            var noteScript = obj.GetComponent<ScrollingNote>();
            noteScript.NoteName = noteName;
            noteScript.Speed = this.scrollSpeed;
            Notes.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
