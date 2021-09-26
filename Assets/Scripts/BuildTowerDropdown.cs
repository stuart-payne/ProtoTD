using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProtoTD
{
    public class BuildTowerDropdown : BuildDropdown, ISelectHandler
    {
        [SerializeField] string m_LabelName;
        [SerializeField] private TMP_Dropdown.OptionData title;

        protected override void Start()
        {
            base.Start();
            m_Dropdown.onValueChanged.AddListener(ResetLabel);
            ResetLabel();
        }

        public void OnSelect(BaseEventData eventData)
        {
            m_Dropdown.options.Remove(title);
        }

        void ResetLabel(int i = 0)
        {
            m_Dropdown.options.Add(title);
            // This sets the value that appears on the dropdown to the value we just added which effectively acts as an label.
            m_Dropdown.SetValueWithoutNotify(m_Dropdown.options.Count - 1);
        }
    }
}
