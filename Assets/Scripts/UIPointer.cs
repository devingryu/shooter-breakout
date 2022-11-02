using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

namespace SBR
{
    public class UIPointer : MonoBehaviour
    {
        private int counter = 0;
        public int Counter {
            get => counter;
            set {
                counter = value;
                laserPointer.thickness = value<=0?0f:0.002f; 
            }
        }
        private bool active;
        private SteamVR_LaserPointer laserPointer;
        private void Awake() 
        {
            laserPointer = GetComponent<SteamVR_LaserPointer>();
            laserPointer.PointerIn += PointerInside;
            laserPointer.PointerOut += PointerOutside;
            laserPointer.PointerClick += PointerClick;
            Counter = 0;
        }
        public void PointerClick(object sender, PointerEventArgs e)
        {
            UIObject c;
            if ((c = e.target.GetComponent<UIObject>()) != null)
            {
                c.OnClick();
            } 
        }

        public void PointerInside(object sender, PointerEventArgs e)
        {
            UIObject c;
            if ((c = e.target.GetComponent<UIObject>()) != null)
            {
                Counter++;
                if(c.Name == "Button")
                    ((UIButton) c).IsHovering = true;
            }
        }

        public void PointerOutside(object sender, PointerEventArgs e)
        {
            UIObject c;
            if ((c = e.target.GetComponent<UIObject>()) != null)
            {
                Counter--;
                if(c.Name == "Button")
                    ((UIButton) c).IsHovering = false;
            }
        }
    }
}