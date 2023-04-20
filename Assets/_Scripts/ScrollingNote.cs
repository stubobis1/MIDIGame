using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollingNote : MonoBehaviour
{
    public Vector3 Direction = Vector3.left;
    public float Speed;
    public GameObject SharpGameObject;
    public GameObject OutOfStaffLine;

    private string _NoteName;
    public string NoteName
    {
        get { return _NoteName; }
        set 
        { 
            this.IsSharp = value.Contains('#');
            OutOfStaffLine.SetActive(value == "C4" || value == "C#4");
            _NoteName = value; 
        }
    }


    private bool _IsSharp;
    public bool IsSharp 
    { 
        get { return _IsSharp; } 
        set 
        { 
            _IsSharp= value;
            SharpGameObject.SetActive(value);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = transform.position + (Speed * Time.deltaTime * Direction);
    }
}
