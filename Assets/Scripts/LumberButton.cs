using UnityEngine;
using System.Collections;
using TMPro;

public class LumberButton : MonoBehaviour
{
    public passiveUpgrade lumberjack;
    private TMP_Text lumberText;
    private bool first_click = false;
    void Start()
    {
        GameObject tempObj = GameObject.Find("LumberText");
        lumberText = tempObj.GetComponent<TMP_Text>();
        lumberjack = new passiveUpgrade(resourceType.Logs, resourceType.Logs, 0, 1, 10, 1f, 0);
    }

    public void button_click() {
        lumberjack.upgrade();
        if (!first_click)
        {
            first_click = true;
            StartCoroutine(lumberjack.tick());
            ResourceManager.Instance.StartGenerators();
        }
        lumberText.text = "Lumberjack\n" + lumberjack.cost + " Logs";
    }
}
