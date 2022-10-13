using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int sceneIndex = 0;
    [SerializeField] InputManager inputManager;
    [SerializeField] string battleSceneName = "BattleScene";
    [SerializeField] string selectSceneName = "SelectScene";
    [SerializeField] string startSceneName = "StartScene";
    public bool save1;
    public bool save2;
    public bool save3;
    public int currentSaveSlot;
    public int selectLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this);

            inputManager = GetComponent<InputManager>();
        }

    }

    private void Start()
    {
        LoadAllSaveStatus();

    }


    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadBattleScene()
    {
        sceneIndex = 2;
        SceneManager.LoadSceneAsync(battleSceneName);
    }

    public void LoadSelectScene()
    {
        sceneIndex = 1;
        SceneManager.LoadScene(selectSceneName);
    }

    public void LoadStartScene()
    {
        sceneIndex = 0;
        SceneManager.LoadScene(startSceneName);
    }

    IEnumerator AsyncLoad(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        float loadingProgress = 0;
        loadingProgress = async.progress * 100;
        if (loadingProgress >= 90)
        {

            async.allowSceneActivation = true;
            yield return null;
        }
        StopCoroutine(nameof(AsyncLoad));

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == battleSceneName)
        {
            inputManager.LoadComponent();
            FindObjectOfType<EnemyManager>().sLevl = Resources.Load<ScriptableLevel>($"Levels/Level{selectLevel}");
            FindObjectOfType<TilemapManager>().LoadLevel(selectLevel);
        }
        else if (scene.name == selectSceneName)
        {
            Debug.Log("Load Select Scene");
            var levelPointManager = FindObjectOfType<LevelPointManager>();
            levelPointManager.InitLevelStatus(level1, level1lock, level1_Star);
            levelPointManager.InitLevelStatus(level2, level2lock, level2_Star);
        }
        //Load Map;
    }

    #region Save Data
    [SerializeField] int starCount = 0;
    [SerializeField] int beatlevel = 0;

    [SerializeField] int level1 = 1;
    [SerializeField] bool level1lock = false;
    [SerializeField] int level1_Star = 0;
    [SerializeField] int level2 = 2;
    [SerializeField] bool level2lock = false;
    [SerializeField] int level2_Star = 0;

    [System.Serializable]
    class SaveData
    {
        public int starCount;
        public int beatlevel;

        public int level1;
        public bool level1lock;
        public int level1_Star;
        public int level2;
        public bool level2lock;
        public int level2_Star;


    }

    [System.Serializable]
    class SaveStatus
    {
        public bool save1;
        public bool save2;
        public bool save3;
    }

    SaveStatus SavingStatus()
    {
        var saveStatus = new SaveStatus();
        saveStatus.save1 = save1;
        saveStatus.save2 = save2;
        saveStatus.save3 = save3;
        return saveStatus;
    }

    const string SAVE_DATA_FILE_Name = "SaveStatus.sav";
    const string PLYAER_DATA_FILE_Name = "Save";

    SaveData SavingData()
    {
        var saveData = new SaveData();
        saveData.starCount = starCount;
        saveData.beatlevel = beatlevel;
        saveData.level1 = level1;
        saveData.level1lock = level1lock;
        saveData.level1_Star = level1_Star;
        saveData.level2 = level2;
        saveData.level2lock = level2lock;
        saveData.level2_Star = level2_Star;
        return saveData;
    }



    void SaveByJson(string savefile)
    {
        SaveSystem.SaveByJson(savefile, SavingData());
    }

    public void Save()
    {
        SaveByJson(PLYAER_DATA_FILE_Name + currentSaveSlot + ".sav");
    }

    public void SaveFileStatus()
    {
        SaveSystem.SaveByJson(SAVE_DATA_FILE_Name, SavingStatus());
    }

    public void LoadFileStatus()
    {
        var saveData = SaveSystem.LoadFromJson<SaveStatus>(SAVE_DATA_FILE_Name);
        LoadStatus(saveData);
    }

    public void LoadFromJson(string saveFile)
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(saveFile);
        LoadData(saveData);
    }

    void LoadData(SaveData saveData)
    {
        starCount = saveData.starCount;
        beatlevel = saveData.beatlevel;

        level1 = saveData.level1;
        level1lock = saveData.level1lock;
        level1_Star = saveData.level1_Star;
        level2 = saveData.level2;
        level2lock = saveData.level2lock;
        level2_Star = saveData.level2_Star;
    }


    void LoadStatus(SaveStatus saveStatus)
    {
        save1 = saveStatus.save1;
        save2 = saveStatus.save2;
        save3 = saveStatus.save3;
    }

    int GetStarByJson(SaveData saveDta)
    {
        return saveDta.starCount;
    }

    public void Load(int index)
    {
        LoadFromJson(PLYAER_DATA_FILE_Name + index + ".sav");
    }
    [UnityEditor.MenuItem("Developer/Delete Player Data Save File")]
    public static void DeletePlayerDataSaveFile()
    {
        SaveSystem.DeleteSaveFile(PLYAER_DATA_FILE_Name);
    }

    #endregion

    public void ChangeSaveInfo(int index)
    {
        if (index == 1)
        {
            save1 = true;
        }
        else if (index == 2)
        {
            save2 = true;
        }
        else if (index == 3)
        {
            save3 = true;
        }
        starCount = 0;
        beatlevel = 0;
        level1 = 1;
        level1lock = false;
        level1_Star = 0;
        level2 = 2;
        level2lock = true;
        level2_Star = 0;
        currentSaveSlot = index;
        Save();
    }

    public int GetStar(int index)
    {
        string saveFile = PLYAER_DATA_FILE_Name + index + ".sav";
        var saveData = SaveSystem.LoadFromJson<SaveData>(saveFile);
        return GetStarByJson(saveData);
    }

    public void LoadAllSaveStatus()
    {
        LoadFileStatus();
    }

    public void GetScore(int count)
    {
        Debug.Log(selectLevel);
        if (selectLevel == 1)
        {
            level1_Star = count;
            if (count > 0)
            {
                level2lock = false;
            }
        }
        else if (selectLevel == 2)
        {
            level2_Star = count;
        }
        starCount = level1_Star + level2_Star;
        Save();
    }

    public void SaveStarts(int count)
    {
        if (selectLevel == 1)
        {
            Debug.Log("11");
            level1_Star = count;
        }
        else if (selectLevel == 2)
        {
            Debug.Log("22");
            level2_Star = count;
        }
        Save();
    }
}
