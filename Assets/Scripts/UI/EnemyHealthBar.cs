using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider hpBar;

    private void Awake()
    {
        hpBar = GetComponent<Slider>();
    }

    public void Init(float maxHp)
    {
        gameObject.SetActive(false);
        hpBar.maxValue = maxHp;
        hpBar.value = maxHp;       
    }

    public void SetCurrentHealth(float currentHP)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        hpBar.value = currentHP;
    }

}
