using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlaySoundOnBeat : TriggerOnBeat
{
    public AudioClip AudioClip;
    public AudioSource PlaySource;
    //AudioSource AudioSource;

    protected override void Awake()
    {
        base.Awake();
        if(PlaySource == null)
        {
            if (!TryGetComponent<AudioSource>(out PlaySource))
            {
                PlaySource = gameObject.AddComponent<AudioSource>();
            }
        }
        if (AudioClip == null)
        {
            AudioClip = PlaySource.clip;
        }
        else
        {
            PlaySource.clip = AudioClip;
        }
    }
    public override void BeatAction(int Beat)
    {
        PlaySource.Play();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
       
        base.Start();  
    }

    // Update is called once per frame
    void Update()
    {
    }
}
