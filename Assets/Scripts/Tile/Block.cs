using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Vector2Int position;
    public Vector2Int Position { get { return position; }}

    [SerializeField]
    private Vector3 center;
    public Vector3 Center { get { return center; } }

    public bool isPlaceable;//是否可以放置建筑
    public PlaceableType PlaceableType;
    public Tower towerOnTile;
    [SerializeField] SpriteRenderer promptPicture;
    [SerializeField] PromptSO promptSO;

    public List<Enemy> enemyList;

    private void Start()
    {
        LevelManager.Instance.RecordTiles(this);
    }

    public void Init(Vector3 position,bool isPlaceable,PlaceableType PlaceableType,string gName,PromptSO promptSO)
    {
        transform.position = position;
        center = position;
        this.isPlaceable = isPlaceable;
        this.PlaceableType = PlaceableType;
        this.position = Vector2Int.CeilToInt(position);
        gameObject.name = gName;
        this.promptSO = promptSO;
    }

    /// <summary>
    /// 高光网格的提示样式
    /// </summary>
    public void HighlightBlock_TowerPosition(Tower tower)
    {
        if (tower.towerSO.PlaceableType == PlaceableType)
        {
            promptPicture.sprite = promptSO.PlaceablePrompt;
        }
        else
        {
            promptPicture.sprite = promptSO.UnplaceableSprite;
        }

    }
    /// <summary>
    /// 重置网格的提示样式
    /// </summary>
    public void ResetBlockPromption()
    {
        promptPicture.sprite = null;
    }

    public bool SetTower(Tower tower)
    {
        if (towerOnTile==null)
        {
            if (tower.towerSO.PlaceableType == PlaceableType)
            {
                towerOnTile = tower;
                return true;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"This Block Can't this Type of Building,Building Type{tower.towerSO.PlaceableType},Block Type{PlaceableType}");
#endif
                return false;
            }
        }
        else
        {

#if UNITY_EDITOR
            Debug.LogError($"This Block already placed tower Position:{Position}");
#endif
            return false;
        }
    }

    public void HighlightBlock_Range()
    {
        promptPicture.sprite = promptSO.RangePrompt;
    }

    public Enemy GetEnemy()
    {
        if (enemyList.Count>0)
        {
            return enemyList[0];
        }
        return null;
    }

    public List<Enemy> GetEnemies(int count)
    {
        List<Enemy> result=new List<Enemy>();
        if (enemyList!=null)
        {
            if (enemyList.Count >= count)
            {
                result= enemyList.GetRange(0, count);
            }
            else
            {
                result = enemyList.GetRange(0, enemyList.Count);
            }
        }
        return result;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
    }
}
