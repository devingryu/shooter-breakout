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
        private Minimap attachedMinimap;
        private bool isInit = false;
        private Vector3 brickBound;
        public int currentY = 0;
        private void Awake() 
        {
            ren = GetComponent<SpriteRenderer>();
        }
        public void Init(Transform pair)
        {
            GameManager m = GameManager.Inst;
            attachedMinimap = transform.parent.parent.GetComponent<Minimap>();
            currentY = attachedMinimap.CurrentY;
            gridBound = (Vector3[])m.grid.gridBound.Clone();
            brickBound = m.grid.brickBound;
            gridBound[0] -= (m.grid.brickBound/2); gridBound[1] += (m.grid.brickBound/2);
            
            Vector3 whtg = m.grid.gridBound[1]-m.grid.gridBound[0]+m.grid.brickBound;
            Vector3 whtm = attachedMinimap.gridBound[1]-attachedMinimap.gridBound[0]+attachedMinimap.brickBound;
            scaleFactor = whtm[0] / whtg[0];

            this.pair = pair;
            isInit = true;
        }

        // Update is called once per frame
        void Update()
        {
            try
            {
                if(!isInit) return;
                if (pair != null)
                {
                    if(((int)(pair.position[1]/brickBound[1])) != currentY) {
                        ren.enabled = false;
                        return;
                    }
                    for(int i=0;i<3;i++)
                        if(!(gridBound[0][i] < pair.position[i] && pair.position[i] < gridBound[1][i])) {
                            ren.enabled = false;
                            return;
                        }

                    ren.enabled = true;
                    gameObject.transform.localPosition = new(pair.position.x * scaleFactor,pair.position.z *scaleFactor, 0f);
                } 
                else 
                {
                    Destroy(gameObject);
                }
            }
            catch { }
        }
    }
}