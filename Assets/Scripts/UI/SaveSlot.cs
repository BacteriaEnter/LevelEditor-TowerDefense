using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] bool saveStatus;

    [SerializeField] int starCount;
    [SerializeField] TextMeshProUGUI starText;
    [SerializeField] TextMeshProUGUI saveStatusText;
    [SerializeField] int saveIndex;
    [SerializeField] GameObject scoreParent;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.currentSaveSlot = saveIndex;
        if (!saveStatus)
        {
            saveStatus = true;
            if (saveIndex == 1)
            {
                GameManager.instance.save1 = saveStatus;
            }
            else if (saveIndex == 2)
            {
                GameManager.instance.save2 = saveStatus;
            }
            else
            {
                GameManager.instance.save3 = saveStatus;
            }
            scoreParent.gameObject.SetActive(true);
            saveStatusText.gameObject.SetActive(false);
            starText.text ="0";
            //GameManager.instance.GetStar(saveIndex).ToString()
            GameManager.instance.ChangeSaveInfo(saveIndex);
            GameManager.instance.SaveFileStatus();
        }
        GameManager.instance.Load(saveIndex);
        GameManager.instance.LoadSelectScene();
    }

    private void Awake()
    {
        if (saveIndex == 1)
        {
            saveStatus = GameManager.instance.save1;
        }
        else if (saveIndex == 2)
        {
            saveStatus = GameManager.instance.save2;
        }
        else
        {
            saveStatus = GameManager.instance.save3;
        }
        if (saveStatus)
        {
            scoreParent.gameObject.SetActive(true);
            saveStatusText.gameObject.SetActive(false);
            starText.text = GameManager.instance.GetStar(saveIndex).ToString();
        }
        else
        {
            scoreParent.gameObject.SetActive(false);
            saveStatusText.gameObject.SetActive(true);
        }
    }
}
