using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SBR
{
    public class GameOverMenu : MonoBehaviour
    {
        public GameObject inner;
        public GameObject background;
        [SerializeField]
        private TextMeshProUGUI text;

        public void Init(Vector3 scale)
        {
            background.transform.localScale = scale;
            var min = Mathf.Min(scale.x,scale.y);
            inner.transform.localScale = Vector3.one * min;
        }
        public void SetScore((int,int) score)
            => text.text = $"현재기록: {score.Item2}\n최고기록: {score.Item1}";
    }
}