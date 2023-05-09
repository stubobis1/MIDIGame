using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGame3dTarget : MonoBehaviour
{
    public bool IsAlive = true;
    public string AnimationToPlayWhenDead;
    public float TimeToLiveAfterHit = 0.4f;
    public bool debug = true;
    private void Start()
    {
        OnRailsMidiShooter.Instance.targets.Add(this);
    }



    private void OnDestroy()
    {
        if (OnRailsMidiShooter.Instance != null)
        {
            OnRailsMidiShooter.Instance.targets.Remove(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit()
    {
        if (debug)
            print("GAMEOBJECT HIT");
        IsAlive = false;
        var beat = GetComponent<TriggerAnimationOnBeat>();
        if (beat != null)
        {
            beat.enabled = false;
            var ani = GetComponent<Animation>();
            if (ani != null)
            {
                ani.Play(AnimationToPlayWhenDead);
                
            }
        }
        //Destroy(this, TimeToLiveAfterHit);
        Destroy(this.gameObject);

    }
}


