using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using Valve.VR;
using UnityEngine;
using TMPro;

namespace SBR
{
    public class Gun : MonoBehaviour
    {
        private Interactable interactable;
        private GameManager gm;
        private Transform bulletParent;
        public SteamVR_Action_Boolean Trigger;
        public SteamVR_Input_Sources inputSource;
        private float timer = 0f;
        private float timerTarget = 0.2f;
        public bool shootEnabled = false;
        [SerializeField]
        private Transform shootingPoint;
        [SerializeField]
        private GameObject bullet;
        [SerializeField]
        private TextMeshPro bulletIndicator;
        void Start()
        {
            interactable = GetComponent<Interactable>();
            gm = GameManager.Inst;
            bulletParent = gm.BulletParent;
            //StartCoroutine(AttachOnReady(HoldingHand, interactable, gameObject));
            //Trigger.AddOnChangeListener(OnTriggerStateChange, inputSource);
        }
        void Update()
        {
            timer += Time.deltaTime;
            if(timer >= timerTarget) 
            {
                timer = timerTarget;
                if(shootEnabled && gm.RemainingBallCount > 0){
                    Instantiate(bullet,shootingPoint.position,shootingPoint.rotation,bulletParent);
                    gm.RemainingBallCount--;
                    timer = 0f;
                }
            }
        }
        private void OnTriggerStateChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, System.Boolean newState)
        {
            shootEnabled = newState;
        }
        public void UpdateBulletIndicator(int rm, int max)
        {
            bulletIndicator.text = $"{rm}/{max}";
        }
    }
}