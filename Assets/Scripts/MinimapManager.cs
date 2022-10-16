using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class MinimapManager : MonoBehaviour
    {
        [SerializeField]
        private Minimap[] minimaps;
        public void Init()
        {
            foreach(var m in minimaps)
                m.Init();
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