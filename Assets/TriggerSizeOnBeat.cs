using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSizeOnBeat : TriggerOnBeat
{

    public float pulseSize = 1.15f;
    public float returnSpeed = 5f;
    public float interval = 1f;
    Vector3 startSize;


    private void Awake()
    {
        this.beatActions.Add(interval, x => transform.localScale = startSize * pulseSize);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        this.startSize = this.transform.localScale;
        base.Start();  
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale= Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * returnSpeed);
    }
}
