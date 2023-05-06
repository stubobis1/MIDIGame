using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnBeat : MonoBehaviour
{

    protected Dictionary<float, Action<int>> beatActions = new();

    // Start is called before the first frame update
    public virtual void Start()
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
