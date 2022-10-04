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
        public SteamVR_Action_Boolean Trigger;
        public SteamVR_Input_Sources inputSource;
        private float timer = 0f;
        private float timerTarget = 0.2f;
        private bool shootEnabled = false;
        [SerializeField]
        private Transform shootingPoint;
        [SerializeField]
        private GameObject bullet;
        /*
        private void HandHoverUpdate(Hand hand)
        {
            interactable = GetComponent<Interactable>();
            GrabTypes startingGrabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(gameObject);

            if(interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
            {
                hand.HoverLock(interactable);
                hand.AttachObject(gameObject,startingGrabType, attachmentFlags);
            } 
            else if (isGrabEnding)
            {
                hand.DetachObject(gameObject);
                hand.HoverUnlock(interactable);
            }
        }
        */
        void Start()
        {
            interactable = GetComponent<Interactable>();
            StartCoroutine(AttachOnReady(HoldingHand, interactable, gameObject));
            Trigger.AddOnChangeListener(OnTriggerStateChange, inputSource);
        }
        void Update()
        {
            timer += Time.deltaTime;
            if(timer >= timerTarget) 
            {
                timer = timerTarget;
                if(shootEnabled){
                    //Debug.Log("Shoot!");
                    Instantiate(bullet,shootingPoint.position,shootingPoint.rotation);
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