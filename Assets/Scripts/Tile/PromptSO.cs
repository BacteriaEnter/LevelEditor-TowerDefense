using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Block/PromptSO",fileName ="PromptSO")]
public class PromptSO : ScriptableObject
{
    [SerializeField] private Sprite placeablePrompt;
    public Sprite PlaceablePrompt => placeablePrompt;

    [SerializeField] private Sprite unplaceablePrompt;
    public Sprite UnplaceableSprite => unplaceablePrompt;

    [SerializeField] private Sprite rangePrompt;
    public Sprite RangePrompt => rangePrompt;

    [SerializeField] private List<Sprite> subWaypointPrompt;
    public List<Sprite> SubWaypointPrompt => subWaypointPrompt;
}
