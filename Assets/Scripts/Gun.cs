using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using Valve.VR;
using UnityEngine;

namespace SBR
{
    public class Gun : MonoBehaviour
    {
        public Hand HoldingHand;
        private Interactable interactable;
        private GameManager gm;
        public SteamVR_Action_Boolean Trigger;
        public SteamVR_Input_Sources inputSource;
        private float timer = 0f;
        private float timerTarget = 0.2f;
        public bool shootEnabled = false;
        [SerializeField]
        private Transform shootingPoint;
        [SerializeField]
        private GameObject bullet;
        void Start()
        {
            interactable = GetComponent<Interactable>();
            gm = GameManager.Inst;
            //StartCoroutine(AttachOnReady(HoldingHand, interactable, gameObject));
            //Trigger.AddOnChangeListener(OnTriggerStateChange, inputSource);
        }
        void Update()
        {
            timer += Time.deltaTime;
            if(timer >= timerTarget) 
            {
                timer = timerTarget;
                if(shootEnabled && gm.remainingBallCount > 0){
                    Instantiate(bullet,shootingPoint.position,shootingPoint.rotation);
                    gm.remainingBallCount--;
                    timer = 0f;
                }
            }
        }
        private static IEnumerator AttachOnReady(Hand hand, Interactable interactable, GameObject gameObject)
        {
            Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags
            & (~Hand.AttachmentFlags.SnapOnAttach)
            & (~Hand.AttachmentFlags.DetachOthers);
            //
            if(!(hand.isActiveAndEnabled && hand.isPoseValid )) { yield return null;}
            yield return new WaitForSeconds(0.1f);
            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, GrabTypes.Scripted, attachmentFlags);
        }
        private void OnTriggerStateChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, System.Boolean newState)
        {
            shootEnabled = newState;
        }
    }
}