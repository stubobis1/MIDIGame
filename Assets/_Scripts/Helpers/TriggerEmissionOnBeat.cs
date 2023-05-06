using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TriggerEmissionOnBeat : TriggerOnBeat
{
    ParticleSystem ParticleSystem;
    ParticleSystem.MainModule psMain;


    private void Awake()
    {
        beatActions.Add(1f, x => onBeat(x));
        beatActions.Add(2f, x => onHalfBeat(x));
    }


    // Start is called before the first frame update
    public override void Start()
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
            psMain.startColor= Color.red;
            ParticleSystem.Emit(100);
        }
        else
        {
            psMain.startColor= Color.blue;
            ParticleSystem.Emit(100);
        }
        
    }
    private void onHalfBeat(int totalBeats)
    {
        psMain.startColor = Color.white;

        ParticleSystem.Emit(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
