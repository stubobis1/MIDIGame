using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerOnBeat : MonoBehaviour
{

    protected Dictionary<float, Action<int>> beatActions = new();

    public float Interval = 0;

    public int skipEveryXIntervals = 0;
    public int BeatOffset = 0;
    
    protected virtual void Awake()
    {
        this.beatActions.Add(Interval, x => TriggerBeatAction(x));
    }

    private void TriggerBeatAction(int Beat)
    {
        if (skipEveryXIntervals == 0)
        {
            BeatAction(Beat);
            return;
        }

        if ((Beat + BeatOffset) % skipEveryXIntervals == 0)
        { 
            BeatAction(Beat);
            return;
        }
    }

    /// <summary>
    /// Empty method to override
    /// </summary>
    /// <param name="Beat"></param>
    public virtual void BeatAction(int Beat) { }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        subscribeFuncs();
    }

    private void subscribeFuncs()
    {
        foreach (var func in beatActions)
        {
            Conductor.Instance.AddBeatAction(func.Key, func.Value);
        }
    }

    private void OnDestroy()
    {
        unsubscribeFuncs();
    }

    private void unsubscribeFuncs()
    {
        foreach (var func in beatActions)
        {
            Conductor.Instance.RemoveBeatAction(func.Key, func.Value);
        }
    }
}
