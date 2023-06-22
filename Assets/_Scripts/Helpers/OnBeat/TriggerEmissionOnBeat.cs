using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TriggerEmissionOnBeat : TriggerOnBeat
{
    ParticleSystem ParticleSystem;
    ParticleSystem.MainModule psMain;

    //public Color EmissionColor = Color.white;
    public ParticleSystem.MinMaxGradient EmissionColor = Color.white;
    ParticleSystem.MinMaxGradient startingColor;

    public int EmissionAmount;

    
    protected override void Start()
    {
        base.Start();
        if (ParticleSystem == null)
        {
            ParticleSystem = GetComponent<ParticleSystem>();
            psMain = ParticleSystem.main;
            startingColor = psMain.startColor;
        }
    }

    public override void BeatAction(int Beat) 
    {
        psMain.startColor = EmissionColor;
        ParticleSystem.Emit(EmissionAmount);
        psMain.startColor = startingColor;
    }
}
