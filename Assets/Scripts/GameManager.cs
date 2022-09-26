using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager() { }
        public BrickSpawner spawner = new BrickSpawner();
        public Grid grid = new Grid();

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