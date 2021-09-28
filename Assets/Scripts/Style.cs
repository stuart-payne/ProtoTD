using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ProtoTD
{
    [CreateAssetMenu()]
    public class Style : ScriptableObject
    {
        public Color Background;
        public Color OnBackGround;
        public Color Primary;
        public Color OnPrimary;
        public int TitleTextSize;
        public int BodyTextSize;
        public TMP_FontAsset FontStyle;
    }
}
