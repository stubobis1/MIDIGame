using Minis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MidiGameObject : MonoBehaviour
{
    protected Action<MidiNoteControl, float> NoteOnAction;
    protected Action<MidiNoteControl> NoteOffAction;
    protected abstract void OnNoteReleased(MidiNoteControl note);
    protected abstract void OnNotePressed(MidiNoteControl note, float velocity);

    protected virtual void Awake()
    {
        NoteOnAction =  (MidiNoteControl note, float velocity) =>
        {
            try
            {
                OnNotePressed(note, velocity);
            }
            catch (Exception e)
            {
                print("?>?");
                throw;
            }
            
        };
        NoteOffAction = (MidiNoteControl note) =>
        {
            OnNoteReleased(note);
        };
    }

    protected virtual void Start()
    {
        subscibeNoteFuncs();
    }

    protected virtual void OnDestroy()
    {
        UnsubscribeNoteFuncs();
    }

    private void subscibeNoteFuncs()
    {
        MidiController.NoteOnActions += NoteOnAction;
        MidiController.NoteOffActions += NoteOffAction;
    }

    private void UnsubscribeNoteFuncs()
    {
        MidiController.NoteOnActions -= NoteOnAction;
        MidiController.NoteOffActions -= NoteOffAction;
    }
}
