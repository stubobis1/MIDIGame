using Minis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPitchedSound : MonoBehaviour
{
    public float transposeInSemitone = 0;
    public AudioSource voice;
    // Start is called before the first frame update

    Action<MidiNoteControl, float> NoteOnAction;
    Action<MidiNoteControl> NoteOffAction;
    Dictionary<MidiNoteControl, AudioSource> MidiDic = new Dictionary<MidiNoteControl, AudioSource>();
    public bool debug;

    void Start()
    {
        if (voice == null)
        { 
            voice = GetComponent<AudioSource>();
        }

        NoteOnAction = (MidiNoteControl note, float velocity) =>
        {
            OnNotePressed(note, velocity);
        };
        NoteOffAction = (MidiNoteControl note) =>
        {
            OnNoteReleased(note);
        };

        subscibeNoteFuncs();

    }

    private void OnNoteReleased(MidiNoteControl note)
    {
        
    }

    // from https://answers.unity.com/questions/141771/whats-a-good-way-to-do-dynamically-generated-music.html
    private void OnNotePressed(MidiNoteControl note, float velocity)
    {
        if (debug)
        {
            Debug.LogWarning($"Play Pitched Sound: {note.noteNumber}");
        }
        voice.pitch = Mathf.Pow(2, (note.noteNumber + transposeInSemitone) / 12.0f);
        voice.Play();
    }

    private void OnDestroy()
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

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playPitched(MidiNoteControl note)
    {

        
    }
}
