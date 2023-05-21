using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TriggerEmissionOnBeat : TriggerOnBeat
{
    ParticleSystem ParticleSystem;
    ParticleSystem.MainModule psMain;

    public Color EvenBeatColor = Color.white;
    public Color OddBeatColor = Color.white;
    public Color HalfBeatColor = Color.white;

    public int EmissionAmount;
    public int HalfBeatEmissionAmount;

    new public float Interval = 1f;

    protected override void Awake()
    {
        beatActions.Add(Interval, x => onBeat(x));
        if (HalfBeatEmissionAmount > 0)
        {
            beatActions.Add(Interval * 2, x => onHalfBeat(x));
        }
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (ParticleSystem == null)
        {
            ParticleSystem = GetComponent<ParticleSystem>();
            psMain = ParticleSystem.main;
        }
    }

    private void onBeat(int totalBeats)
    {
        if (totalBeats % Conductor.Instance.songBeatsPerMeasure == 0 || totalBeats % Conductor.Instance.songBeatsPerMeasure == 2)
        {
            psMain.startColor = EvenBeatColor;
            ParticleSystem.Emit(EmissionAmount);
        }
        else
        {
            psMain.startColor = OddBeatColor;
            ParticleSystem.Emit(EmissionAmount);
        }
        
    }
    private void onHalfBeat(int totalBeats)
    {
        if (totalBeats % 2 == 0)
        {
            psMain.startColor = HalfBeatColor;
        }

        ParticleSystem.Emit(HalfBeatEmissionAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
