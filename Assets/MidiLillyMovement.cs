using Minis;
using System;
using System.Collections;
using UnityEngine;
namespace LillypadGame
{

    public class MidiLillyMovement : MonoBehaviour
    {

        #region setup
        // Used this https://stackoverflow.com/questions/8465239/how-to-remove-a-method-from-an-action-delegate-in-c-sharp
        // declare on a class fields level
        Action<MidiNoteControl, float> NoteOnAction;
        Action<MidiNoteControl> NoteOffAction;

        public Animator CharAnimator;
        private bool isAnimateGrounded = true;

        public bool debug = false;

        private void SubscibeNoteFuncs()
        {
            MidiController.NoteOnActions += NoteOnAction;
            MidiController.NoteOffActions += NoteOffAction;
        }

        private void UnsubscribeNoteFuncs()
        {
            MidiController.NoteOnActions -= NoteOnAction;
            MidiController.NoteOffActions -= NoteOffAction;
        }

        #endregion


        // Start is called before the first frame update
        void Start()
        {
            if (CharAnimator == null)
            {
                CharAnimator = GetComponent<Animator>();
            }

            NoteOnAction = (MidiNoteControl note, float velocity) =>
            {
                OnNotePressed(note, velocity);
            };
            NoteOffAction = (MidiNoteControl note) =>
            {
                OnNoteReleased(note);
            };

            SubscibeNoteFuncs();
            if (debug)
            {
                Debug.LogWarning("midi lily start");
            }

        }

        private void OnDestroy()
        {
            if (debug)
            {
                Debug.LogWarning("midi lily destoryed");
            }
            
            UnsubscribeNoteFuncs();
        }

        // Update is called once per frame
        void Update()
        {
            if (debug)
            {
                //Debug.LogWarning("midi lily update");
            }
            
            CharAnimator.SetBool("Grounded", isAnimateGrounded);
        }


        public float jumpDurration = 100f;
        public float jumpHeight = 5f;
        private IEnumerator JumpToTarget()
        {
            if (debug)
            {
                Debug.LogWarning("midi lily start Jump!");
            }
            
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = GetJumpPosition();

            //float distance = (startPosition - targetPosition).magnitude;
            //float movementSpeed = 100;
            //float duration = distance / movementSpeed;

            for (float f = 0; f < 1; f += Time.deltaTime / jumpDurration)
            {
                transform.position = Vector3.Lerp(
                    startPosition,
                    targetPosition,
                    f)
                    + new Vector3(0f, Mathf.Sin(f * Mathf.PI) * jumpHeight, 0f)
                    ;
                
                yield return null;
            }
            isAnimateGrounded = true;
        }
        Vector3 nextTransform;
        private Vector3 GetJumpPosition()
        {
            return nextTransform;
            //throw new NotImplementedException();
        }

        int notesDown = 0;
        int currentScaleIndex = 0;
        private void OnNotePressed(MidiNoteControl note, float velocity)
        {
            //print(note);


            var lillyPad = LillyPadGameManager.Instance.LilyPads[currentScaleIndex];

            var targetNote = LillyPadGameManager.Instance.currentScale[currentScaleIndex];

            if (note.shortDisplayName == targetNote)
            {
                if (debug)
                {
                    Debug.LogWarning($"midi lily Hit correct note: {note.shortDisplayName}");
                }
                nextTransform = lillyPad.transform.position;
                StartJump();
                currentScaleIndex++;
            }
            else 
            {
                if (debug)
                {
                    Debug.LogWarning($"midi lily Hit WRONG note: {note.shortDisplayName} --- correct note is {targetNote}");
                }
            }
            //var lillyPadLogic = lillyPad.GetComponent<LillyPadLogic>();

            if (notesDown == 0)
            {
                isAnimateGrounded = false;
            }
            notesDown++;

        }

        private void StartJump()
        {

            Debug.Log("Active? " + gameObject.activeInHierarchy);

            StartCoroutine(JumpToTarget());
        }

        private void OnNoteReleased(MidiNoteControl note)
        {
            //print(note + " off");

            notesDown = Math.Max(0, notesDown - 1);
            if (notesDown == 0)
            {
                // Silence from the player
            }

        }


    }
}