using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SBR
{
    public class RoundIndicator : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        private int currentRound;
        private int maxScore;
        public int CurrentRound {
            get => currentRound;
            set {
                currentRound = value;
                UpdateText();
            }
        }
        public int MaxScore {
            get => maxScore;
            set {
                maxScore = value;
                UpdateText();
            }
        }
        private void UpdateText()
            => text.text = $"라운드 {currentRound}\n최고기록 {maxScore}";

        
    }
}