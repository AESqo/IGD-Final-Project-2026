using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Text;

public enum GameState{
        Start,
        UP_Panel_Get
    }
public class GameManager : MonoBehaviour
{
    private TMP_Text resourceText;
    private StringBuilder tempText = new StringBuilder();
    public GameObject UpgradePanel;
    public GameObject UP_Button;
    public static GameState currState;
    void Start()
    {
        GameObject tempObj = GameObject.Find("ResourceText");
        resourceText = tempObj.GetComponent<TMP_Text>();
        UpgradePanel.SetActive(false);
        currState = GameState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        GameStateUpdate();
        tempText.Clear();
        foreach (KeyValuePair<resourceType, double> dics in ResourceManager.Instance.rManage) {
            tempText.Append(dics.Key.ToString()).Append(": ").Append(dics.Value).Append(" ");
        }
        resourceText.text = tempText.ToString();
    }

    public void GameStateUpdate()
    {
        if(currState == GameState.UP_Panel_Get) {UP_Button.SetActive(true);}
    }
}

