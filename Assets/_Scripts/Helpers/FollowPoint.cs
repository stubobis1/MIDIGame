using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            this.transform.position = target.position;
        }
    }
}
