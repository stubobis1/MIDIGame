using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LillypadGame
{
    
    public class LillyPadLogic : MonoBehaviour
    {

        //public Vector3 scrollSpeed;
        public GameObject NotePrefab;
        public NotePositions NotePositions;

        public MusicBeatValue beatWeight = MusicBeatValue.quarter;

        public float targetBeat;
        public Vector3 targetPos = Vector3.zero;

        Vector3 Startpos;

        [DisplayWithoutEdit()]
        public float targetDspTime;
        // Start is called before the first frame update
        void Start()
        {
            Startpos= transform.position;
            
            if (NotePositions == null)
            { 
                NotePositions = GetComponent<NotePositions>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Conductor.Instance == null)
            {
                Debug.LogError("no conductor instance found", this);
            }

            if (targetBeat != 0)
            {
                try
                {
                    this.transform.position = Vector3.Lerp(Startpos, targetPos, Conductor.Instance.songPositionInBeats / targetBeat);
                }
                catch
                {
                    Debug.Log($"Conductor.Instance.songPositionInBeats: {Conductor.Instance.songPositionInBeats}\n targetBeat: {targetBeat}");
                }
            }
            
            //this.transform.position = this.transform.position + (Time.deltaTime * scrollSpeed);
        }

        internal void AddNote(string noteName)
        {
            var notePos = MusicHelper.RemoveSharpFromNoteName(noteName);

            var note = Instantiate(NotePrefab, this.transform);
            note.GetComponent<BasicNote>().NoteName = noteName;
            note.transform.position = NotePositions.Positions[notePos].position;


            /*
             *                 obj.transform.position = new Vector3(
                    Player.transform.position.x + (i * offsetBetweenNotes.x),
                    GlobalSingletonCleftPositions.Instance.Positions[notePos].position.y,
                    Note.gameObject.transform.position.z
                    ) ;
            */

        }

    }
}