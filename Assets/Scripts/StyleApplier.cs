using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProtoTD
{
    public class StyleApplier : MonoBehaviour
    {
        [SerializeField] private Style m_Style;
        
        [ContextMenu("UpdateStyle")]
        public void UpdateStyle()
        {
            var panelImageComp = GetComponent<Image>();
            panelImageComp.color = m_Style.Background;
            
            TextMeshProUGUI[] textComponents = GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var textComponent in textComponents)
            {
                textComponent.color = m_Style.OnBackGround;
                if (textComponent.CompareTag("Title"))
                {
                    textComponent.fontSize = m_Style.TitleTextSize;
                }
                else
                {
                    if (textComponent.transform.parent.CompareTag("Button"))
                    {
                        textComponent.color = m_Style.OnPrimary;
                        var buttonImage = textComponent.GetComponentInParent<Image>();
                        buttonImage.color = m_Style.Primary;
                    }
                    else
                    {
                        textComponent.color = m_Style.OnBackGround;    
                    }

                    textComponent.fontSize = m_Style.BodyTextSize;
                }
                textComponent.font = m_Style.FontStyle;
            }
        }
        
    }
}
