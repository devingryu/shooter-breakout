using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using UnityEngine;

namespace SBR
{
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager() { }
        [HideInInspector]
        public BrickSpawner spawner;
        [HideInInspector]
        public Grid grid;
        public ControllerHandler ch;
        [HideInInspector]
        public MinimapManager minimap;
        private int maxBallCount = 1;
        private int remainingBallCount = 1;
        public int RemainingBallCount {
            get => remainingBallCount;
            set {
                remainingBallCount = value;
                ch.OnUpdateBulletCount(remainingBallCount,maxBallCount);
            }
        }
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
                ch.OnUpdateBulletCount(remainingBallCount,maxBallCount);
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
                    ch.OnUpdateBulletCount(remainingBallCount,maxBallCount);
                    spawner.NextRound();
                } else // Unexpected on normal play
                    round = value;
            }
        }
        private void Awake() 
        {
            spawner = GetComponent<BrickSpawner>();
            grid = GetComponent<Grid>();
            minimap = GetComponent<MinimapManager>();
            ch = GetComponent<ControllerHandler>();
        }
    }
}