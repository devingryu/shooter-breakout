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
        
        public GameObject gun;
        public Hand hand;

        private int round = 0;
        public int Round
        {
            get => round;
            set
            {
                round = value;
            }
        }
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}