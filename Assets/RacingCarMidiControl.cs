using Minis;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacingCarMidiControl : MidiGameObject
{
    public TMP_Text outputUI;
    CarController ControlledCar;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool Brake { get; private set; }


    public string[] currentChord;
    public string[] currentKey;
    public string noteCutoff;

    protected override void Awake()
    {
        base.Awake();
        ControlledCar = GetComponent<CarController>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnNotePressed(MidiNoteControl note, float velocity)
    {
        float BeatCheckInterval = 1f;

        var beatsFromLastBeat = Conductor.Instance.songPositionInBeats % BeatCheckInterval;
        var beatsUntilNextBeat = Math.Abs(beatsFromLastBeat - BeatCheckInterval);

        bool isLate = false;
        float beatOffset;
        if (beatsFromLastBeat < beatsUntilNextBeat)
        { 
            isLate = true;
            beatOffset = beatsFromLastBeat;
        }
        else
        {
            beatOffset = beatsUntilNextBeat;
        }
        var earlyLateText = isLate ? "LATE " : "EARLY";
        outputUI.text = $"{beatOffset} beats {earlyLateText}";
        outputUI.color = Color.Lerp(Color.green, Color.red, beatOffset * 2);
        
    }

    protected override void OnNoteReleased(MidiNoteControl note)
    {

    }

    // Update is called once per frame
    void Update()
    {
        //var Horizontal = Input.GetAxis("Horizontal");
        //var Vertical = Input.GetAxis("Vertical");
        //var Brake = Input.GetButton("Jump");


        ControlledCar.UpdateControls(Horizontal, Vertical, Brake);


    }
}
