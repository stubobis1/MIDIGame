using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LillypadGame
{
    public class LillyPadGameManager : MonoBehaviour
    {
        public GameObject LilyPadPrefab;
        public GameObject Player;

        public Vector3 scrollSpeed;
        public Vector3 offsetBetweenNotes;

        public static LillyPadGameManager Instance;


        public List<LillyPadLogic> LilyPads = new List<LillyPadLogic>();
        public string[] currentScale;

        // Start is called before the first frame update
        void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            if (currentScale.Length == 0)
            {
                //currentScale = MusicHelper.CopyAndReverseArray(MusicHelper.dSharpMinorPentatonic);
                currentScale = MusicHelper.CopyAndReverseArray(MusicHelper.cMajor);
            }
            GenerateNotesForScale(currentScale);
        }

        private void GenerateNotesForScale(string[] scale)
        {
            for (int i = 0; i < scale.Length; i++)
            {
                var noteName = scale[i];
                

                var obj = Instantiate(LilyPadPrefab);
                obj.SetActive(true);
                //TODO: this and speed should be replaced with a conductor
                obj.transform.position = Player.transform.position + (offsetBetweenNotes * (i + 1) ); 
                var lillyPadLogic = obj.GetComponent<LillyPadLogic>();
                LilyPads.Add(lillyPadLogic);
                lillyPadLogic.AddNote(noteName);
                lillyPadLogic.scrollSpeed = this.scrollSpeed;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
