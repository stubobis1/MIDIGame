using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LillypadGame
{
    public class LillyPadLogic : MonoBehaviour
    {

        public Vector3 scrollSpeed;
        public GameObject NotePrefab;
        public NotePositions NotePositions;


        // Start is called before the first frame update
        void Start()
        {
            if (NotePositions == null)
            { 
                NotePositions = GetComponent<NotePositions>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = this.transform.position + (Time.deltaTime * scrollSpeed);
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