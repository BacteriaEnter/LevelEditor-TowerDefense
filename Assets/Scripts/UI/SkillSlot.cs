using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] Sprite skillIcon;
    [SerializeField] Sprite defaultIcon;
    [SerializeField] Image skillImage;

    public void InitSlot(bool outOfRange, Sprite icon)
    {
        if (outOfRange)
        {
            skillImage.sprite = defaultIcon;
        }
        else
        {
            skillImage.sprite = icon;
        }
    }

    public void ResetSlot()
    {
        skillImage.sprite = defaultIcon;
    }
}
