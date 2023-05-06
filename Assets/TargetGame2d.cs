using Minis;
using ScaleGame2d;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetGame2d : MonoBehaviour
{
    public GameObject targetObject;
    Action<MidiNoteControl, float> NoteOnAction;
    Action<MidiNoteControl> NoteOffAction;

    public float timeToHide = 1f;
    public float timeToShow = 2f;
    bool isVisable;
    float nextTriggerTime;
    List<string> AvailableNotePositions;
    public string targetShortNoteName;
    // Start is called before the first frame update
    void Start()
    {
        AvailableNotePositions = GlobalSingletonCleftPositions.Instance.Positions.Keys.ToList<string>();
        nextTriggerTime = Time.time + timeToHide;
        NoteOnAction = (MidiNoteControl note, float velocity) =>
        {
            OnNotePressed(note, velocity);
        };
        NoteOffAction = (MidiNoteControl note) =>
        {
            OnNoteReleased(note);
        };
    }

    private void OnNoteReleased(MidiNoteControl note)
    {
        
    }

    private void OnNotePressed(MidiNoteControl note, float velocity)
    {
        
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
        if (Time.time > nextTriggerTime)
        {
            if (isVisable)
            {
                targetObject.SetActive(false);
                nextTriggerTime = Time.time + timeToHide;
                isVisable = false;
            }
            else 
            {
                
                int index = (int)(UnityEngine.Random.value * AvailableNotePositions.Count);
                var note = AvailableNotePositions[index];
                targetShortNoteName = note;
                targetObject.SetActive(true);
                targetObject.transform.position = GlobalSingletonCleftPositions.Instance.Positions[note].position;
                nextTriggerTime = Time.time + timeToShow;
                isVisable = true;
            }
        }
    }
}
