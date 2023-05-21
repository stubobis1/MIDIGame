using System;
using System.Collections.Generic;
using UnityEngine;


public class Conductor : MonoBehaviour
{

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    public int songBeatsPerMeasure = 4;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //The number of seconds for each song beat
    [DisplayWithoutEdit()]
    public float secPerBeat;

    //Current song position, in seconds
    [DisplayWithoutEdit()]
    public float songPosition;

    //Current song position, in beats
    [DisplayWithoutEdit()]
    public float songPositionInBeats;

    [DisplayWithoutEdit()]
    public float songBps;

    [DisplayWithoutEdit()]
    //How many seconds have passed since the song started
    public float dspSongTime;


    public static Conductor Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Conductor!");
        }
        Instance = this;

        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;
        songBps = songBpm * 60;
    }


    Dictionary<float, List<Action<int>>> beatActions = new Dictionary<float, List<Action<int>>>();
    Dictionary<float, int> lastTriggeredBeats = new();

    /// <summary>
    /// Add action to happen at a beat
    /// </summary>
    /// <param name="FrequencyPerBeat">how many times per beat this should happen (2 means half beat, 0.5 means every other beat) </param>
    /// <param name="action">delagate</param>
    public void AddBeatAction(float FrequencyPerBeat, Action<int> action)
    {

        if (!beatActions.ContainsKey(FrequencyPerBeat))
        {
            beatActions[FrequencyPerBeat] = new List<Action<int>>();
        }
        beatActions[FrequencyPerBeat].Add(action);
    }

    public void RemoveBeatAction(float FrequencyPerBeat, Action<int> action)
    {
        beatActions[FrequencyPerBeat].Remove(action);
    }

    public void RemoveBeatAction(Action<int> action)
    {
        foreach (var item in beatActions)
        {
            item.Value.Remove(action);
        }
    }





    void Start()
    {
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        foreach (var interval in beatActions.Keys)
        {
            checkForBeatActions(interval);
        }

    }

    private void checkForBeatActions(float interval)
    {
        if (!lastTriggeredBeats.ContainsKey(interval))
        {
            lastTriggeredBeats[interval] = 0;
            return;
        }
        var ticks = (int)(songPositionInBeats * interval);
        if (ticks > lastTriggeredBeats[interval])
        {
            lastTriggeredBeats[interval] = ticks;
            if (beatActions[interval] != null)
            {
                foreach (var action in beatActions[interval])
                {
                    action(ticks);
                }
            }
        }

    }
}
