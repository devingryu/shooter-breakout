using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class UIBall : MonoBehaviour
    {
        private Transform pair;
        private float scaleFactor;
        private Vector3[] gridBound;
        private SpriteRenderer ren;
        private void Awake() 
        {
            ren = GetComponent<SpriteRenderer>();
        }
        public void Init(Transform pair)
        {
            GameManager m = GameManager.Inst;
            gridBound = (Vector3[])m.grid.gridBound.Clone();
            gridBound[0] -= (m.grid.brickBound/2); gridBound[1] += (m.grid.brickBound/2);

            Vector3 whtg = m.grid.gridBound[1]-m.grid.gridBound[0]+m.grid.brickBound;
            Vector3 whtm = m.minimap.gridBound[1]-m.minimap.gridBound[0]+m.minimap.brickBound;
            scaleFactor = whtm[0] / whtg[0];

            this.pair = pair;
        }

        // Update is called once per frame
        void Update()
        {
            try
            {
                if (pair != null)
                {
                    for(int i=0;i<3;i++)
                        if(!(gridBound[0][i] < pair.position[i] && pair.position[i] < gridBound[1][i])) {
                            ren.enabled = false;
                            return;
                        }

                    ren.enabled = true;
                    gameObject.transform.localPosition = new(pair.position.x * scaleFactor,pair.position.z *scaleFactor, 0f);
                }
            }
            catch { }
        }
    }
}