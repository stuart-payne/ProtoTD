using System;
using System.Collections;
using System.Collections.Generic;
using ProtoTD;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameoverPanel : MonoBehaviour
{
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private TextMeshProUGUI m_ScoreText;

    public void GiveData(int score, UnityAction restartCb)
    {
        m_RestartButton.onClick.AddListener(restartCb);
        m_ScoreText.text = score.ToString();
    }
}
