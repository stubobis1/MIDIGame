using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minis;
using Unity.VisualScripting;

public class MidiMovement : MonoBehaviour
{
    #region setup
    // Used this https://stackoverflow.com/questions/8465239/how-to-remove-a-method-from-an-action-delegate-in-c-sharp
    // declare on a class fields level
    Action<MidiNoteControl, float> NoteOnAction;
    Action<MidiNoteControl> NoteOffAction;

    public GameObject Sharp;
    public GameObject Flat;
    public GameObject OutOfStaffLine;

    // in constructor initialize
    public MidiMovement()
    {
        NoteOnAction = (MidiNoteControl note, float velocity) =>
        {
            OnNotePressed(note, velocity);
        };
        NoteOffAction = (MidiNoteControl note) =>
        {
            OnNoteReleased(note);
        };
       

    }

    ~MidiMovement()
    {
        UnsubscribeNoteFuncs();
    }

    private void subscibeNoteFuncs()
    {
        MidiController.NoteOnActions += NoteOnAction;
        MidiController.NoteOffActions+= NoteOffAction;
    }

    private void UnsubscribeNoteFuncs()
    {
        MidiController.NoteOnActions -= NoteOnAction;
        MidiController.NoteOffActions -= NoteOffAction;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        subscibeNoteFuncs();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnNotePressed(MidiNoteControl note, float velocity) 
    {
        //        if (note.shortDisplayName.Length > 0) { }
        var name = note.shortDisplayName;
        var posName = name.Replace("#", "");
        if (name.Contains('#'))
        { 
            Sharp.SetActive(true);
        }
        else 
        {
            Sharp.SetActive(false);
        }

        if (posName == "C4")
        { 
            OutOfStaffLine.SetActive(true);
        }
        else
        {
            OutOfStaffLine.SetActive(false);
        }
        var pos = GlobalSingletonCleftPositions.Instance.Positions[posName].position;
        this.transform.position = new Vector3(pos.x, pos.y, this.transform.position.z);
    }
    void OnNoteReleased(MidiNoteControl note)
    {

    }
}
