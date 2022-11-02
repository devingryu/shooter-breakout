using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace SBR
{
    public class Minimap : MonoBehaviour
    {
        [SerializeField]
        private bool isHandHeld;
        [SerializeField]
        private Transform ballParent;
        [SerializeField]
        private TextMeshPro floorText;
        [SerializeField]
        private float gridCenterX = 0f;
        [SerializeField]
        private float gridStartZ = 0f;
        // 기준 벽돌 좌표, 위치
        private XYZ StdBrickCoord;
        private Vector3 StdBrickPos;
        private int currentY = 0;
        public int CurrentY
        {
            get => currentY;
            set
            {
                currentY = value;
                floorText.text = $"{value + 1}F";
                UpdateCurrentY();
            }
        }
        public UIBrick[,] bricks;
        private int[] gridCount;
        [SerializeField]
        private GameObject uiBrickPrefab;
        [SerializeField]
        private GameObject uiBackgroundPrefab;
        [SerializeField]
        private GameObject uiBallPrefab;
        private Grid grid;
        public readonly Vector3 brickBound = new(0.2f, 0f, 0.125f);
        public Vector3[] gridBound;

        public void Init()
        {
            grid = GameManager.Inst.grid;
            gridCount = (int[])grid.gridCount.Clone();
            StdBrickCoord = new XYZ(((gridCount[0] % 2 == 0) ? (gridCount[0] - 2) : (gridCount[0] - 1)) / 2, 0, 0);
            StdBrickPos = new Vector3((gridCount[0] % 2 == 0) ? gridCenterX - (brickBound.x / 2) : gridCenterX, 0, gridStartZ);
            gridBound = new Vector3[2] { getPosFromCoord(new XYZ(0, 0, 0)), getPosFromCoord(new XYZ(gridCount[0] - 1, 0, gridCount[2] - 1)) };

            bricks = new UIBrick[gridCount[0], gridCount[2]];
            for (int i = 0; i < gridCount[0]; i++)
                for (int k = 0; k < gridCount[2]; k++)
                {
                    var newUIBrick = Instantiate(uiBrickPrefab, Vector3.zero, transform.rotation, transform);
                    newUIBrick.SetActive(false);
                    bricks[i, k] = newUIBrick.GetComponent<UIBrick>();
                    bricks[i, k].Init(getPosFromCoord(new(i, 0, k)));
                }

            var grdbndsum = (gridBound[0] + gridBound[1]) / 2f;
            var grdbndmns = gridBound[1] - gridBound[0] + brickBound;

            var bg = isHandHeld ?
                transform.parent.GetChild(0).transform :
                Instantiate(uiBackgroundPrefab, Vector3.zero, transform.rotation, transform).transform;
            float multiplier = isHandHeld ? transform.localScale.x : 1f;
            bg.localPosition = new Vector3(gridCenterX + grdbndsum.x, gridStartZ + grdbndsum.z, 0f) * multiplier;
            bg.localScale = new Vector3(grdbndmns.x * 2f, grdbndmns.z * 2f, 0.05f / multiplier) * multiplier;


            ((RectTransform)floorText.transform).anchoredPosition3D = new Vector3(grdbndsum[0], gridBound[1][2] + brickBound[2], 0f);
        }
        public Vector3 getPosFromCoord(XYZ coord)
        {
            Vector3 pos = new Vector3();
            for (int i = 0; i < 3; i++)
                pos[i] = StdBrickPos[i] + ((coord[i] - StdBrickCoord[i]) * brickBound[i]);
            return pos;
        }
        public void OnBrickUpdate(XYZ pos)
        {
            if (currentY != pos.Y) return;
            var gridbrick = grid.bricks[pos.X, pos.Y, pos.Z];
            if (gridbrick != null)
            {
                bricks[pos.X, pos.Z].IsBrick = gridbrick.isBrick;
                bricks[pos.X, pos.Z].Health = gridbrick.Health;
            }
            else
                bricks[pos.X, pos.Z].Health = 0;
        }
        public void updateBricks()
        {
            for (int i = 0; i < gridCount[0]; i++)
                for (int k = 0; k < gridCount[2]; k++)
                    OnBrickUpdate(new(i, currentY, k));
        }
        public UIBall newBall()
            => Instantiate(uiBallPrefab, Vector3.zero, transform.rotation, ballParent).GetComponent<UIBall>();
        private void UpdateCurrentY()
        {
            var children = ballParent.GetComponentsInChildren<UIBall>();
            foreach (UIBall child in children)
            {
                child.currentY = currentY;
            }
            updateBricks();
        }
    }
}