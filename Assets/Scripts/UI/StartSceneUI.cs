using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] GameObject saveMenu;
    
    public void NewGame()
    {
        OpenSaveMenu();
    }

    public void OpenSaveMenu()
    {
        saveMenu.SetActive(true);
    }
    
    public void CloseLoadMenu()
    {
        saveMenu.SetActive(false);
    }

    public void TapLoadGame()
    {

    }

    public void LoadGame()
    { 
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
