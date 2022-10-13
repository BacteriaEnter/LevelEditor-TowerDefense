using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneUI : MonoBehaviour
{
    public void LoadStartScene()
    {
        GameManager.instance.LoadStartScene();
    }
}
