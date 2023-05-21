using System.Collections.Generic;
using UnityEngine;

namespace LillypadGame
{
    public class LillyPadGameManager : MonoBehaviour
    {
        public GameObject LilyPadPrefab;
        public GameObject Player;

        public Vector3 scrollDirection;
        public Vector3 offsetBetweenNotes;
        public Vector3 offsetBetweenNoteTargets;

        public static LillyPadGameManager Instance;

        public List<LillyPadLogic> LilyPads = new();
        public string[] currentScale;

        // Start is called before the first frame update
        void Start()
        {
            //scrollDirection = Vector3.Normalize(scrollDirection);
            if (Instance != null)
            {
                Debug.LogError("More than one LillyPadGameManager");
            }

            Instance = this;

            if (currentScale.Length == 0)
            {
                //currentScale = MusicHelper.CopyAndReverseArray(MusicHelper.dSharpMinorPentatonic);
                //currentScale = MusicHelper.CopyAndReverseArray(MusicHelper.cMajor);
                currentScale= new string[] { 
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                    "C4",
                };
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
                
                

                obj.transform.position = Player.transform.position + (offsetBetweenNotes * (i + 1));
                var lillyPadLogic = obj.GetComponent<LillyPadLogic>();
                lillyPadLogic.targetBeat = (i + 4);
                lillyPadLogic.targetPos = Player.transform.position + (offsetBetweenNoteTargets * (i + 1));
                LilyPads.Add(lillyPadLogic);
                lillyPadLogic.AddNote(noteName);

                //lillyPadLogic.scrollSpeed = (1f / Conductor.Instance.secPerBeat) * offsetBetweenNotes.x * -this.scrollDirection;
                //lillyPadLogic.scrollSpeed = this.scrollSpeed;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
