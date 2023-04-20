using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSingletonCleftPositions : MonoBehaviour
{
    public GlobalSingletonCleftPositions ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
    }

    public static string RemoveSharpFromNoteName(string noteName) {
        return noteName.Replace("#", "");
    }
    public static GlobalSingletonCleftPositions Instance { get; private set; }
    public GenericDictionary<string,Transform> Positions = new GenericDictionary<string,Transform>();
}
