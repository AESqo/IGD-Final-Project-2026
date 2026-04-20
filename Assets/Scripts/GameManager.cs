using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Text;
using System.IO;
 
public enum GameState{
        Start,
        UP_Panel_Get
    }
 
[System.Serializable]
public class SaveData {
    public double logs;
    public double planks;
    public GameState state;
}
 
public class GameManager : MonoBehaviour
{
    private TMP_Text resourceText;
    private StringBuilder tempText = new StringBuilder();
    public GameObject UpgradePanel;
    public GameObject UP_Button;
    public static GameState currState;
 
    // Delegate defines the method signature listeners must match.
    public delegate void SawmillUnlockedHandler();
    // Only GameManager can fire this event — others must call TriggerSawmillUnlocked().
    private static event SawmillUnlockedHandler OnSawmillUnlocked;
 
    // --- PUBLIC TRIGGER METHOD ---
    // Since only GameManager can invoke its own event, outside classes
    // call this method instead. GameManager then fires the event internally.
    public static void TriggerSawmillUnlocked()
    {
        OnSawmillUnlocked?.Invoke();
    }
 
    void Start()
    {
        GameObject tempObj = GameObject.Find("ResourceText");
        resourceText = tempObj.GetComponent<TMP_Text>();
        UpgradePanel.SetActive(false);
        currState = GameState.Start;
 
        // Subscribe: when the event fires, call HandleSawmillUnlocked.
        OnSawmillUnlocked += HandleSawmillUnlocked;
 
        LoadGame();
    }
 
    void OnDestroy()
    {
        OnSawmillUnlocked -= HandleSawmillUnlocked;
    }
 
    private void HandleSawmillUnlocked()
    {
        currState = GameState.UP_Panel_Get;
        UP_Button.SetActive(true);
    }
 
    void Update()
    {
        if(ResourceManager.Instance == null) return;
 
        tempText.Clear();
        foreach (KeyValuePair<resourceType, double> dics in ResourceManager.Instance.rManage) {
            tempText.Append(dics.Key.ToString()).Append(": ").Append(dics.Value).Append(" ");
        }
        resourceText.text = tempText.ToString();
    }
 
    public void SaveGame()
    {
        SaveData data = new SaveData {
            logs = ResourceManager.Instance.rManage[resourceType.Logs],
            planks = ResourceManager.Instance.rManage[resourceType.Planks],
            state = currState
        };
        string json = JsonUtility.ToJson(data, true);
        string rootFolder = Path.GetDirectoryName(Application.dataPath);
        string fullPath = Path.Combine(rootFolder, "save.json");
        File.WriteAllText(fullPath, json);
    }
 
    public void LoadGame()
    {
        try
        {
            string rootFolder = Path.GetDirectoryName(Application.dataPath);
            string fullPath = Path.Combine(rootFolder, "save.json");
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                ResourceManager.Instance.rManage[resourceType.Logs] = data.logs;
                ResourceManager.Instance.rManage[resourceType.Planks] = data.planks;
                currState = data.state;
 
                if (currState == GameState.UP_Panel_Get)
                    TriggerSawmillUnlocked();
            }
        }
        catch (IOException e)
        {
            Debug.LogWarning("GameManager: Could not read save file. " + e.Message);
        }
        catch (System.ArgumentException e)
        {
            Debug.LogWarning("GameManager: Save file was corrupt or malformed. " + e.Message);
        }
    }
 
    void OnApplicationQuit()
    {
        SaveGame();
        string rootFolder = Path.GetDirectoryName(Application.dataPath);
        string fullPath = Path.Combine(rootFolder, "playtime.txt");
        string playTime = "Session Playtime: " + Time.time + " seconds\n";
        File.AppendAllText(fullPath, playTime);
    }
}
 