using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using UnityEngine;

namespace SBR
{
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager() { }
        public BrickSpawner spawner;
        public Grid grid;
        [HideInInspector]
        public MinimapManager minimap;
        private int maxBallCount = 1;
        public int MaxBallCount {
            get => maxBallCount;
            set {
                maxBallCount = value;
            }
        }

        private int round = 0;
        public int Round
        {
            get => round;
            set
            {
                round = value;
            }
        }
        private void Awake() 
        {
            minimap = GetComponent<MinimapManager>();
        }
    }
}