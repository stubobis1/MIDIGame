using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterSeconds : MonoBehaviour
{
    public float DestroyAfterSeconds = 2f;

    float killTime;
    void Start()
    {
        killTime = Time.time + DestroyAfterSeconds;
    }

    
    void Update()
    {
        if(Time.time > killTime)
        {
            Destroy(this.gameObject);
        }
    }
}
