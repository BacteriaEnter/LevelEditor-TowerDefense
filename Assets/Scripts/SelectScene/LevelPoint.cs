using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> stars;
    public bool isLocked;
    public GameObject flag;
    public int levelIndex;


    void LightStar(int startCount)
    {
        for (int i = 0; i < startCount; i++)
        {
            stars[i].SetActive(true);
        }
    }

    public void InitPoint(bool isLocked,int starsCount)
    {
        this.isLocked = isLocked;
        LightStar(starsCount);
        if (isLocked)
        {
            flag.SetActive(false);
        }
    }



    public void ToBattle()
    {
        if (!isLocked)
        {
            GameManager.instance.selectLevel = levelIndex;
            GameManager.instance.LoadBattleScene();
        }

    }
}
