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
        [HideInInspector]
        public ControllerHandler ch;
        [HideInInspector]
        public MinimapManager minimap;
        private DataHandler dataHandler;
        public Transform BulletParent;
        
        private int maxBallCount = 1;
        private int remainingBallCount = 1;
        private float saveTimer;
        private float saveDelay = 1f;
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
        private void Start()
        {
            dataHandler = DataHandler.Inst;
            Debug.Log(dataHandler.filePath);
        }
        private void Update() 
        {
            saveTimer += Time.deltaTime;
            if(saveTimer > saveDelay && grid.en)
            {
                saveTimer = 0f;
                dataHandler.Save(SerializeProgress());
            }
        }
        private SaveStructure SerializeProgress()
        {
            var bricks = grid.SerializeBricks();
            List<BulletInfo> bullets = new List<BulletInfo>();
            foreach(var b in BulletParent.GetComponentsInChildren<Bullet>())
                bullets.Add(new(new v3(b.transform.position), new v3(b.Direction)));
            
            return new SaveStructure(bricks, bullets.ToArray(), round, maxBallCount, remainingBallCount, returnedBallCount);
        }
    }
}