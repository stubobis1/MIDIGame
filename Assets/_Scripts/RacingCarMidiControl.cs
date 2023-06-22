using Minis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RacingCarMidiControl : MidiGameObject
{
    public TMP_Text outputUI;
    CarController ControlledCar;

    public float NotePower { get; private set; }
    public float NotePowerDecayRate;
    public float NoteSteer { get; private set; }
    public float NoteSteerDecayRate;
    public bool Brake { get; private set; }


    public string[] currentChord;
    public string[] currentKey;
    [SerializeField] private string _noteCutoff = "C4";
    private int _noteCutoffValue = -1;
    public float steerMagnitudePerNote;

    public string noteCutoff
    {
        get
        {
            return _noteCutoff;
        }
        set
        {
            _noteCutoff = value;
            _noteCutoffValue = MusicHelper.NoteNameToMidiNumber(value);
        }
    }
    public int noteCutoffValue { get { return _noteCutoffValue; } }




    [SerializeField] private string _noteMidSteerPoint = "C5";
    private int _noteMidSteerPointValue = -1;

    public string noteMidSteerPoint
    {
        get
        {
            return _noteMidSteerPoint;
        }
        set
        {
            _noteMidSteerPoint = value;
            _noteMidSteerPointValue = MusicHelper.NoteNameToMidiNumber(value);
        }
    }
    public int noteMidSteerPointValue { get { return _noteMidSteerPointValue; } }



    protected override void Awake()
    {
        base.Awake();
        ControlledCar = GetComponent<CarController>();
        _noteCutoffValue = MusicHelper.NoteNameToMidiNumber(_noteCutoff);
        _noteMidSteerPointValue = MusicHelper.NoteNameToMidiNumber(_noteMidSteerPoint);


        this.currentChord = MusicHelper.cMajorChord;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnNotePressed(MidiNoteControl note, float velocity)
    {
        float BeatCheckInterval = 0.5f;

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
        if (note.noteNumber < noteCutoffValue) // Bass note
        {
            if (currentChord.Contains(MusicHelper.RemoveOctiveFromNoteName(note.shortDisplayName)))
            {
                NoteSteer = (beatOffset / BeatCheckInterval);
            }
        }
        else
        {
            float diff = note.noteNumber - noteMidSteerPointValue;
            diff *= steerMagnitudePerNote;
            NotePower = Math.Clamp(diff, -1, 1);
        }


        var earlyLateText = isLate ? "LATE " : "EARLY";
        outputUI.text = $"{beatOffset} beats {earlyLateText}";
        outputUI.color = Color.Lerp(Color.green, Color.red, beatOffset * 2 * (1f / BeatCheckInterval));


        print($"H,V: {NotePower}, {NoteSteer}");
    }

    protected override void OnNoteReleased(MidiNoteControl note)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
       // NoteSteer = Mathf.Lerp(NoteSteer, 0, Time.deltaTime * NoteSteerDecayRate);
        //NotePower = Mathf.Lerp(NotePower, 0, Time.deltaTime * NotePowerDecayRate);
        //NotePower *= NotePowerDecayRate * Time.deltaTime;
        ControlledCar.UpdateControls(NotePower, NoteSteer, Brake);


    }
}
