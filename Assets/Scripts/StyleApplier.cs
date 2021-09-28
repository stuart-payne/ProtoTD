using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

namespace ProtoTD
{
    [ExecuteInEditMode]
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
                var parentButton = textComponent.transform.parent.GetComponent<Button>();
                if(parentButton != null)
                    StyleButton(parentButton, textComponent);
                if(textComponent.CompareTag("Title"))
                    StyleTitle(textComponent);
                StyleBodyText(textComponent);
            }
        }

        private void StyleButton(Button button, TextMeshProUGUI buttonText)
        {
            var buttonSerObj = new SerializedObject(button);
            var buttonTextSerObj = new SerializedObject(buttonText);
            var image = button.GetComponent<Image>();
            var imageSerObj = new SerializedObject(image);
            imageSerObj.FindProperty("m_Color").colorValue = m_Style.Primary;
            buttonTextSerObj.FindProperty("m_Color").colorValue = m_Style.OnPrimary;
            buttonTextSerObj.FindProperty("m_fontSize").floatValue = m_Style.BodyTextSize;
            SerializedProperty fontSerProp = buttonTextSerObj.FindProperty("m_fontAsset");
            fontSerProp.objectReferenceValue = m_Style.FontStyle;
            imageSerObj.ApplyModifiedProperties();
            buttonSerObj.ApplyModifiedProperties();
            buttonTextSerObj.ApplyModifiedProperties();
        }

        private void StyleTitle(TextMeshProUGUI titleText)
        {
            var textSerObj = new SerializedObject(titleText);
            textSerObj.FindProperty("m_Color").colorValue = m_Style.OnBackGround;
            textSerObj.FindProperty("m_fontSize").floatValue = m_Style.TitleTextSize;
            textSerObj.FindProperty("m_fontAsset").objectReferenceValue = m_Style.FontStyle;
            textSerObj.ApplyModifiedProperties();
        }

        private void StyleBodyText(TextMeshProUGUI bodyText)
        {
            var textSerObj = new SerializedObject(bodyText);
            textSerObj.FindProperty("m_Color").colorValue = m_Style.OnBackGround;
            textSerObj.FindProperty("m_fontSize").floatValue = m_Style.BodyTextSize;
            textSerObj.FindProperty("m_fontAsset").objectReferenceValue = m_Style.FontStyle;
            textSerObj.ApplyModifiedProperties();
        }
        
    }
}
