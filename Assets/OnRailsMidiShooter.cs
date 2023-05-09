using Minis;
using System.Collections.Generic;
using UnityEngine;

public class OnRailsMidiShooter : MidiGameObject
{
    public List<TargetGame3dTarget> targets = new();
    public float DistanceToHit = 1f;
    public static OnRailsMidiShooter Instance;
    public bool debug = true;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        

        if (Instance != null)
        {
            Debug.LogError("More than one OnRailsMIDIShooter");
        }
        Instance = this;
        base.Awake();
    }

    void Update()
    {

    }

    protected override void OnNotePressed(MidiNoteControl note, float velocity)
    {

        var name = note.shortDisplayName;
        var posName = name.Replace("#", "");
        var isSharp = name.Contains("#");


        var inDict = GlobalSingletonCleftPositions.Instance.Positions.TryGetValue(posName, out var transform);
        if(!inDict)
        {
            Debug.LogWarning("Note Missing: " + posName);
            return;
        }
        var pos = transform.position;
        //var pos = GlobalSingletonCleftPositions.Instance.Positions[posName].position;

        var c = new Vector3(pos.x, pos.y, pos.z + 1f);
        var plane = new Plane(this.transform.position, pos, c);
        if (debug)
        {
            DrawDebugPlane(this.transform.position, pos, c);
        }
        foreach (var t in targets)
        {
            var dist = plane.GetDistanceToPoint(t.transform.position);
            if (debug)
            {
                //print(dist);
                print("Miss");
            }
            if (dist < DistanceToHit)
            {
                print("HITT");
                t.OnHit();
            }
        }

        //this.transform.position = new Vector3(pos.x, pos.y, this.transform.position.z);

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (debug)
        {
            print("OnRailsMidiShooter Destoryed WTFF?!??");
        }
    }

    public void DrawDebugPlane(Vector3 a, Vector3 b, Vector3 c)
    {
        Debug.DrawLine(a, b, Color.green, 1f, false);
        Debug.DrawLine(b, c, Color.red, 1f, false);
        Debug.DrawLine(c, a, Color.blue, 1f, false);
    }

    protected override void OnNoteReleased(MidiNoteControl note)
    {

    }
}
