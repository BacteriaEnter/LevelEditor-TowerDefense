using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    BattleManager battleManager;

    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] int currentWave=0;

    public List<SpawnPoint> spawnPoints;

    [SerializeField] List<SavedWaveDetail> savedWaveDetails;
    public ScriptableLevel sLevl;

    public bool lastDied;
    bool stageClear;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        battleManager=GetComponent<BattleManager>();
    }


    private void Start()
    {
        //savedWaveDetails = sLevl.WaveDetails;
        //StartCoroutine(nameof(EnemySpawnCoroutine));
    }

    private void Update()
    {
        if (lastDied==true&& stageClear==false)
        {
            HandleStageClear();
        }    
    }




    public SavedWaveDetail GetWaveDetailFromEnemyManager()
    {
        SavedWaveDetail waveDetail = null;
        if (currentWave< sLevl.WaveDetails.Count)
        {
            waveDetail = sLevl.WaveDetails[currentWave];
            currentWave++;
        }
        return waveDetail;
    }

    void HandleStageClear()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.enemies.Count > 0)
            {
                return;
            }
        }
        lastDied = true;
        stageClear=true;
        //UIµ¯³ö
        var score=battleManager.CalScore();
        FindObjectOfType<UIManager>().EnableScorePanel(score);
#if UNITY_EDITOR
        Debug.Log("Stage Clear");
#endif
    }


}
