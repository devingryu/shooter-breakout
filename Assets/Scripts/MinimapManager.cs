using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class MinimapManager : MonoBehaviour
    {
        [SerializeField]
        private Minimap[] minimaps;
        private int currentY = 0;
        private int maxY = 1;
        public int CurrentY {
            get => currentY;
            set {
                if(!(0 <= value && value < maxY)) return;
                currentY = value;
                foreach(var m in minimaps)
                    m.CurrentY = value;
            }
        }
        public void Init(int maxY)
        {
            foreach(var m in minimaps)
                m.Init();
            CurrentY = 0;
            this.maxY = maxY;
        }
        public void NewBall(Transform t)
        {
            foreach(var m in minimaps)
                m.newBall().Init(t);
        }
        public void OnBrickUpdate(XYZ pos)
        {
            foreach(var m in minimaps)
                m.OnBrickUpdate(pos);
        }
    }
}