using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScaleGame2d
{
    public class ScaleGame2d : MonoBehaviour
    {
        public ScrollingNote Note;
        public float scrollSpeed = 5f;
        public float spaceBetweenNotes = 1f;
        public float spaceBeforeStartNote = 1f;



        public List<GameObject> Notes = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            GenerateNotesForScale(UpAndDownScale(MusicHelper.dSharpMinorPentatonic));
        }

        private string[] UpAndDownScale(string[] scale)
        {
            var longScale = new string[scale.Length * 2];
            scale.CopyTo(longScale, 0);
            Array.Reverse(scale);
            scale.CopyTo(longScale, scale.Length);
            return longScale;
        }
        private void GenerateNotesForScale(string[] scale)
        {
            for (int i = 0; i < scale.Length; i++)
            {
                var noteName = scale[i];

                var notePos = MusicHelper.RemoveSharpFromNoteName(noteName);

                var obj = Instantiate(Note.gameObject);
                obj.SetActive(true);
                obj.transform.position = new Vector3(
                    spaceBeforeStartNote + (i * spaceBetweenNotes),
                    GlobalSingletonCleftPositions.Instance.Positions[notePos].position.y,
                    Note.gameObject.transform.position.z
                    );

                var noteScript = obj.GetComponent<ScrollingNote>();
                noteScript.NoteName = noteName;
                noteScript.Speed = this.scrollSpeed;
                Notes.Add(obj);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
