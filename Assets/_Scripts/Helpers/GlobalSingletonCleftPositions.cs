using UnityEngine;

public class GlobalSingletonCleftPositions : MonoBehaviour
{
    public static GlobalSingletonCleftPositions Instance { get; private set; }
    public GenericDictionary<string, Transform> Positions = new GenericDictionary<string, Transform>();
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GlobalSingletonCleftPositions");
        }
        Instance = this;
        
    }

    private void Start()
    {
    }

    public static string RemoveSharpFromNoteName(string noteName)
    {
        return noteName.Replace("#", "");
    }

}
