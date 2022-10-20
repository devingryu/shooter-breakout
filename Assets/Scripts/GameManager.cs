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
        public int remainingBallCount {get;set;} = 1;
        private int returnedBallCount = 0;
        public int ReturnedBallCount {
            get => returnedBallCount;
            set {
                returnedBallCount = value;
                if(value <= 0)
                    Round++;
            }
        }
        public int MaxBallCount {
            get => maxBallCount;
            set {
                maxBallCount = value;
            }
        }

        private int round = 1;
        public int Round
        {
            get => round;
            set
            {
                if(value > round) {
                    round = value;
                    remainingBallCount = MaxBallCount;
                    returnedBallCount = MaxBallCount;
                    spawner.NextRound();
                } else // Unexpected on normal play
                    round = value;
            }
        }
        private void Awake() 
        {
            minimap = GetComponent<MinimapManager>();
        }
    }
}