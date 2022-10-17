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
        public int CurrentY {
            get => currentY;
            set {
                currentY = value;
                foreach(var m in minimaps)
                {
                    m.CurrentY = value;
                }
            }
        }
        public void Init()
        {
            foreach(var m in minimaps)
                m.Init();
            CurrentY = 0;
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