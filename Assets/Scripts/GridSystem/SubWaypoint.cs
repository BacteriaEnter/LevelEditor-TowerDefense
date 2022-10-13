using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWaypoint : MonoBehaviour
{
    [SerializeField] private int index;
    public int Index { get { return index; } }
    [SerializeField] SpriteRenderer spriteRenderer;
    public bool isTransferPoint;//ÖÐ×ªµã
    public int transferDestination;

    [SerializeField] private PromptSO promptSO;
    public void Init(int index,bool isTransferPoint,int transferDestination)
    {
        this.index = index;
        this.isTransferPoint= isTransferPoint;
        this.transferDestination = transferDestination;
        InitSpriteByIndex();
    }

    void InitSpriteByIndex()
    {
        spriteRenderer.sprite = promptSO.SubWaypointPrompt[index - 1];
    }
}
