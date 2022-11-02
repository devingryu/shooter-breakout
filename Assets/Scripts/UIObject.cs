using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class UIObject : MonoBehaviour
    {
        public virtual string Name => "Default";
        public virtual void OnClick() {}
    }
}