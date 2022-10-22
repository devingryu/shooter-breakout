using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

namespace SBR
{
    public class ControllerHandler : MonoBehaviour
    {
        private GameManager gm;
        private int gunState = -1; // -1: Unattached, 0: LEFT, 1: RIGHT
        private Gun attachedGun = null;
        [SerializeField]
        private Hand[] hands; // LEFT, RIGHT
        [SerializeField]
        private SteamVR_Action_Boolean trigger;
        private SteamVR_Input_Sources[] inputSources 
            = {SteamVR_Input_Sources.LeftHand, SteamVR_Input_Sources.RightHand};
        // Start is called before the first frame update
        void Start()
        {
            gm = GameManager.Inst;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void OnGunAttachStateChanged()
        {
            for(int i=0;i<2;i++)
                trigger.RemoveAllListeners(inputSources[i]);
            for(int i=0;i<2;i++)
            {
                foreach(var obj in hands[i].AttachedObjects)
                {
                    if (obj.attachedObject.tag == "Gun") 
                    {
                        gunState = i;
                        attachedGun = obj.attachedObject.GetComponent<Gun>();
                        OnUpdateBulletCount(gm.RemainingBallCount,gm.MaxBallCount);
                        trigger.AddOnChangeListener(OnTriggerStateChange,inputSources[i]);
                        return;
                    }
                }
            }
            gunState = -1;
            attachedGun = null;
        }
        private void OnTriggerStateChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, System.Boolean newState)
        {
            if(attachedGun != null) 
            {
                attachedGun.shootEnabled = newState;
            }
        }
        public void OnUpdateBulletCount(int rm, int max)
        {
            if(attachedGun != null)
            {
                attachedGun.UpdateBulletIndicator(rm,max);
            }
        }
    }
}