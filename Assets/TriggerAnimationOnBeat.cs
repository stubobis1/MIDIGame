using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TriggerAnimationOnBeat : TriggerOnBeat
{
    // public float Interval = 1f;
    public float IntervalsPerLoop = 1f;
    public string BeatAnimationName = "Dizzy";
    public Animator Animator;

    AnimationClip AnimationClip;
    float BeatAnimationTime;
    

    
    protected override void Awake()
    {
        if (this.Animator == null)
        {
            this.Animator = this.GetComponent<Animator>();
        }
        //beatActions.Add(Interval, x => triggerOnBeat(x));
        foreach (var ani in Animator.runtimeAnimatorController.animationClips) {
            if (ani.name == BeatAnimationName)
            {
                BeatAnimationTime = ani.length;
                AnimationClip = ani;
            }
        }
        
    }

    public bool startedAni = false;

    public override void BeatAction(int x)
    {
        if (startedAni)
        {
            this.Animator.Play(BeatAnimationName, 0, 0f);
            startedAni = true;
        }
        this.Animator.speed = ((Conductor.Instance.songBpm / 60) * BeatAnimationTime) / IntervalsPerLoop;
        
        // this.Animator.speed something something sync to beat
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
