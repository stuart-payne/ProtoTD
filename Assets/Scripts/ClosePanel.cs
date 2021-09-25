using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{

    public Button CloseButton;
    // Start is called before the first frame update
    void Start()
    {
        CloseButton.onClick.AddListener(ClosePanelCB);
    }

    void ClosePanelCB() => gameObject.SetActive(false);
}
