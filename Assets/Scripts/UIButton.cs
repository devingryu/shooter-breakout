using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

namespace SBR
{
    public class UIButton: UIObject
    {
        public override string Name => "Button";
        [SerializeField]
        private UnityEvent OnClickFunc;

        [SerializeField]
        private Color defaultColor;
        [SerializeField]
        private Color hoverColor;
        [SerializeField]
        private Color clickColor;
        private bool isHovering;
        private Renderer rd;
        public bool IsHovering {
            get => isHovering;
            set {
                isHovering = value;
                CurrentColor = isHovering?hoverColor:defaultColor;
            }
        }

        private Color currentColor;
        public Color CurrentColor {
            get => currentColor;
            set {
                currentColor = value;
                rd.material.color = currentColor;
            }
        }
        private Material mat;
        private void Awake() 
        {
            rd = GetComponent<Renderer>();
            var mat = new Material(rd.material.shader);
            rd.material = mat;
            CurrentColor = defaultColor;
            
        }
        public override void OnClick()
            => OnClickFunc.Invoke();
        
    }
}