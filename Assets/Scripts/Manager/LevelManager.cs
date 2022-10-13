using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private Block selectBlock;
    [SerializeField] Tilemap landLayer;
    [SerializeField] TileBase landTile;
    [SerializeField] TileBase[] tileArray;
    Dictionary<Vector2Int,Block> dic_Tile= new Dictionary<Vector2Int, Block>();
    Dictionary<Vector2Int,Block> route_Dic = new Dictionary<Vector2Int, Block>();

    [SerializeField] SelectActionChannel selectActionChannel;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        selectActionChannel.OnSelected += PromptBlockByPosition;
        selectActionChannel.OnTowerPlaced += PlaceTower;
    }

    private void OnDisable()
    {
        selectActionChannel.OnSelected -= PromptBlockByPosition;
        selectActionChannel.OnTowerPlaced -= PlaceTower;
    }


    void PaintSingleTile(Tilemap tilemap, TileBase tile,Vector3Int position)
    {
        tilemap.SetTile(position,tile);
    }


    public void RecordTiles(Block tile)
    {
        if (!dic_Tile.ContainsKey(tile.Position))
        {
            dic_Tile.Add(tile.Position, tile);
        }
    }
    /// <summary>
    /// 高亮防御塔所在方格
    /// </summary>
    /// <param name="mousePos"></param>
    /// <param name="towerRange"></param>
    void PromptBlockByPosition(Tower tower,Vector2Int mousePos,List<Vector2Int> towerRange)
    {
        var tilePos= mousePos;
        if (dic_Tile.TryGetValue(tilePos, out Block tmpBlock))
        {
            if (tmpBlock!=selectBlock)
            {
                if (selectBlock!=null)
                {
                    selectBlock.ResetBlockPromption();
                }
                tmpBlock.HighlightBlock_TowerPosition(tower);
                selectBlock=tmpBlock;
                PrompRangePosition(towerRange);
            }         
        }
        else
        {
            if (selectBlock!=null)
            {
                selectBlock.ResetBlockPromption();
                selectBlock=null;
            }
        }

  
    }
    /// <summary>
    /// 放置防御塔
    /// </summary>
    /// <param name="tower"></param>
    void PlaceTower(Tower tower)
    {
        if (selectBlock == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Location is not available!");
#endif            
            return;
        }
        if (selectBlock.SetTower(tower))
        {
            tower.transform.position = new Vector3(selectBlock.Center.x, selectBlock.Center.y, 0);
            EventManager.current.TowerPlacedTriggerEffect();
            tower.isplaced = true;
            ResetRangePromption(tower.rangeInWorld);
            ResetSelectBlockPromption();
            selectBlock =null;
        }
    }
    /// <summary>
    /// 重置攻击范围提示
    /// </summary>
    /// <param name="towerRange"></param>
    public void ResetRangePromption(List<Vector2Int> towerRange)
    {
        foreach (var item in towerRange)
        {
            if (dic_Tile.TryGetValue(item, out Block hitBlock))
            {
                if (hitBlock != null)
                {
                    //Reset样式
                    hitBlock.ResetBlockPromption();
                }
            }
        }
    }
    /// <summary>
    /// 高亮攻击范围
    /// </summary>
    /// <param name="towerRange"></param>
    public void PrompRangePosition(List<Vector2Int> towerRange)
    {
        foreach (var item in towerRange)
        {
            if (dic_Tile.TryGetValue(item, out Block hitBlock))
            {
                if (hitBlock != null)
                {

                    hitBlock.HighlightBlock_Range();
                }
            }
        }
    }

    public void ResetSelectBlockPromption()
    {
        if (selectBlock!=null)
        {
            selectBlock.ResetBlockPromption();
        }
    }

    public Block GetBlockByPosition(Vector2Int pos)
    {
        dic_Tile.TryGetValue(pos, out Block result);
        return result;
    }

}

