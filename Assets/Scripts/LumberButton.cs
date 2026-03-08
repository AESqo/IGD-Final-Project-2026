using UnityEngine;
using System.Collections;
using TMPro;

public class LumberButton : MonoBehaviour
{
    public passiveUpgrade lumberjack;
    private TMP_Text lumberText;
    void Start()
    {
        GameObject tempObj = GameObject.Find("LumberText");
        lumberText = tempObj.GetComponent<TMP_Text>();
        lumberjack = new passiveUpgrade("Logs", "Logs", 0, 10, 1f);
        StartCoroutine(lumberjack.tick());
    }

    public void button_click() {
        lumberjack.upgrade();
        lumberText.text = "Lumberjack\n" + lumberjack.cost + " Logs";
    }
}
