using UnityEngine;
using TMPro;

public class SawmillButton : MonoBehaviour
{
    private passiveUpgrade sawmill;
    private TMP_Text sawText;
    private bool first_click = false;
    void Awake()
    {
        GameObject tempObj = GameObject.Find("SawText");
        sawText = tempObj.GetComponent<TMP_Text>();
    }

    public void button_click() {
        if(!first_click)
        {
            sawmill = new passiveUpgrade(resourceType.Planks, resourceType.Logs, 0, 4, 100, 3f, 0);
            first_click = true;
            GameManager.currState = GameState.UP_Panel_Get;
            StartCoroutine(sawmill.tick());
        }
        sawmill.upgrade();
        sawText.text = "Sawmill\n" + sawmill.cost + " Logs";
    }
}
