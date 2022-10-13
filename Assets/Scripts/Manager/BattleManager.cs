using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManager : MonoBehaviour
{
    UIManager uIManager;
    [SerializeField] InputManager inputManager;
    public BuildingSO selectTowerSO;
    public Tower selectTower;
    public List<BuildingSO> towerBackpack;

    [Header("Player Actions")]
    [SerializeField] PlayerAction playerAction;
    [SerializeField] TowerSelected select;
    [SerializeField] SelectTowerInWord selectTowerInWord;
    [SerializeField] SellAction sellAction;



    [SerializeField] LayerMask checkLayer;
    [Header("Action Channel")]
    [SerializeField] SelectActionChannel selectActionChannel;
    [SerializeField] ButtonActionChannel buttonActionChannel;
    [SerializeField] EnemyBattleChannel enemyBattleChannel;

    [Header("Resources")]
    public int health;
    public int gold;

 

    [Header("Tower List")]
    public List<Tower> placedTowerList = new List<Tower>();

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
        inputManager=FindObjectOfType<InputManager>();
    }



    private void OnDisable()
    {
        EventManager.current.onEnemyReachGoal -= TakeDamage;
        buttonActionChannel.OnUpgradeButtonClicked -= UpgradeTower;
    }

    private void Start()
    {
        uIManager.GetTowerSIcon(towerBackpack);
        buttonActionChannel.OnUpgradeButtonClicked += UpgradeTower;
        EventManager.current.onEnemyReachGoal += TakeDamage;
        enemyBattleChannel.onEnemyDied += GetGold;
        uIManager.UpdateHp(health);
        uIManager.UpdateGold(gold);

    }

    

    public void SelectTower(int index)
    {
        if (playerAction != null&&playerAction!=select)
        {
            playerAction.Exit(this, inputManager);
        }
        if (index < towerBackpack.Count)
        {
            selectTowerSO = towerBackpack[index];

            if (gold<selectTowerSO.cost[0])
            {
#if UNITY_EDITOR
                Debug.Log("Insufficient gold ");
#endif
                return;
            }
            if (selectTower!=null)
            {
                Destroy(selectTower.gameObject);
            }
            selectTower = Instantiate(selectTowerSO.prefab, GetMousePosition(), Quaternion.identity).GetComponent<Tower>();
            playerAction = select;
            playerAction.Enter(this);
            //进入Select状态
        }
    }


    private void Update()
    {
        if (playerAction != null)
        {
            playerAction.Logic(this, inputManager, GetMousePosition());
        }

        inputManager.TickInput();
    }
    private void LateUpdate()
    {
        inputManager.confirmTap_input = false;
        inputManager.cancel_Input = false;
        inputManager.rotateL_Input = false;
        inputManager.rotateR_Input = false;
    }



    public void ResetAction()
    {
        playerAction = null;
    }

    Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void ResetTowerSelect()
    {
        selectTowerSO = null;
        selectTower = null;
    }

    public void AddTower(Tower tower)
    {
        if (!placedTowerList.Contains(tower))
        {
            placedTowerList.Add(tower);
        }
    }

    public void RemoveTower(Tower tower)
    {
        if (!placedTowerList.Contains(tower))
        {
            placedTowerList.Remove(tower);
        }
    }

    public void ActionBack()
    {
        if (playerAction != null)
        {
            playerAction.Back(this);
        }
    }

    public void HandlePlayerSelectAction()
    {
        if (playerAction==null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, checkLayer))
            {
                if (hit.collider.CompareTag("Tower"))
                {
                    Tower tower = hit.collider.GetComponent<Tower>();
                    if (tower != null && tower.isplaced)
                    {
                        if (playerAction != null)
                        {
                            playerAction.Exit(this, inputManager);
                        }
                        selectTower = tower;
                        playerAction = selectTowerInWord;
                        playerAction.Enter(this);
                    }
                }
            }
        }
        else if (playerAction == select)
        {
            selectActionChannel.Place(selectTower);
            if (selectTower.isplaced)
            {
                AddTower(selectTower);
                CostGold(selectTower.towerSO.cost[selectTower.currentLevel]);
                playerAction.Exit(this, inputManager);
            }
        }
        else if (playerAction == selectTowerInWord)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, checkLayer))
            {
                if (hit.collider.CompareTag("Tower"))
                {
                    Tower tower = hit.collider.GetComponent<Tower>();
                    if (tower!=selectTower)
                    {
                        selectTower = tower;
                    }
                }
                else if (hit.collider.CompareTag("Land"))
                {
                    playerAction.Exit(this,inputManager);
                }
            }
        }             
    }

    /// <summary>
    /// 进入出售行为
    /// </summary>
    public void EnterSellAction()
    {
        if (playerAction!=sellAction)
        {
            playerAction = sellAction;
            playerAction.Enter(this);
        }
 
    }

    public void ExitSellAction()
    {
        if (playerAction==sellAction)
        {
            //reset
            playerAction.Exit(this, inputManager);
        }
    }

    /// <summary>
    /// 获取金钱的接口
    /// </summary>
    public void GetGold(int amount)
    {
        gold+=amount;
        uIManager.UpdateGold(gold);
    }

    private void CostGold(int amount)
    {
        gold-=amount;
        uIManager.UpdateGold(gold);
    }
     

    public void UpgradeTower()
    {
        if (playerAction == selectTowerInWord)
        {
            if (selectTower.currentLevel<selectTower.towerSO.cost.Count-1)
            {
                if (gold >= selectTower.towerSO.cost[selectTower.currentLevel+1])
                {
                    CostGold(selectTower.towerSO.cost[selectTower.currentLevel+1]);
                    selectTower.Upgrade();
                    uIManager.UpdateUnitInfo(selectTower);
                    selectTowerInWord.CheckTowerUpgradeAvailble();
                }
            }
            //Play fail audio
        }
        
    }


    /// <summary>
    /// 获取当前的
    /// </summary>
    /// <returns></returns>
    public PlayerAction GetCurrentPlayerAction()
    {
        return playerAction;
    }


    public void UpdateUnitInfo(Tower tower)
    {
        uIManager.UpdateUnitInfo(tower);
    }


    public void TakeDamage(int damage)
    {
        health-=damage;
        if (health<=0)
        {
            health = 0;
        }
        uIManager.UpdateHp(health);
    }

    public int CalScore()
    {
        int result = 0;
        if (health==20)
        {
            result = 3;
            GameManager.instance.GetScore(3);
        }
        else if (health>=12&&health<20)
        {
            result = 2;
            GameManager.instance.GetScore(2);
        }
        else if (health<12)
        {
            result = 1;
            GameManager.instance.GetScore(1);
        }
        return result;
    }
}
 