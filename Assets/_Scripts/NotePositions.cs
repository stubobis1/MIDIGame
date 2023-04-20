using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePositions : MonoBehaviour
{
    private void Start()
    {
    }

    public GenericDictionary<string,Transform> Positions = new GenericDictionary<string,Transform>();
}
