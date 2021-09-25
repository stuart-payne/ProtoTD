using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public WaveSO[] Waves;
    public bool IsWaveInProgress;
    public Button button;

    private SpawnManager m_Spawner;
    private WaveEnumerator m_WaveEnumerator;
    private WaveSO m_CurrentWave;


    private void Start()
    {
        IsWaveInProgress = false;
        m_Spawner = GetComponent<SpawnManager>();
        m_WaveEnumerator = new WaveEnumerator(Waves);
    }

    private void Update()
    {
        if (IsWaveInProgress)
            button.interactable = false;
        else
            button.interactable = true;
    }

    public void StartWave()
    {
        if(m_WaveEnumerator.MoveNext())
        {
            m_CurrentWave = m_WaveEnumerator.Current;
            IsWaveInProgress = true;
            StartCoroutine(ExecuteWave(m_CurrentWave));

        } else
        {
            Debug.Log("HIt end of Ienumerator");
            // trigger level complete
        }
    }

    IEnumerator ExecuteWave(WaveSO wave)
    {
        foreach(var enemyString in wave.SpawnStrings)
        {
            yield return StartCoroutine(ExecuteEnemyString(enemyString));
        }
        IsWaveInProgress = false;
    }

    IEnumerator ExecuteEnemyString(EnemySpawnString enemySpawnString)
    {
        yield return new WaitForSeconds(enemySpawnString.StartDelay);
        for(var i = 0; i < enemySpawnString.numberOfEnemies; i++)
        {
            m_Spawner.SpawnEnemy(enemySpawnString.EnemyPrefab, m_WaveEnumerator.CurrentIndex);
            yield return new WaitForSeconds(enemySpawnString.TimeBetweenSpawns);
        }
    }
}

public class WaveEnumerator: IEnumerator<WaveSO>
{
    private WaveSO[] m_Waves;
    private WaveSO m_CurrentWave;
    private int m_CurrentIndex;

    public WaveEnumerator(WaveSO[] waves)
    {
        m_Waves = waves;
        m_CurrentIndex = -1;
    }

    public bool MoveNext()
    {
        if(++m_CurrentIndex >= m_Waves.Length)
        {
            return false;
        } else
        {
            m_CurrentWave = m_Waves[m_CurrentIndex];
        }
        return true;
    }

    public void Reset() => m_CurrentIndex = -1;

    void IDisposable.Dispose() { }

    public WaveSO Current
    {
        get { return m_CurrentWave; }
    }


    public int CurrentIndex
    {
        get { return m_CurrentIndex; }
    }


    object IEnumerator.Current
    {
        get { return Current; }
    }
}
