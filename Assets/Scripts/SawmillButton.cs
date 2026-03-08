using UnityEngine;
using TMPro;

public class SawmillButton : MonoBehaviour
{
    public passiveUpgrade sawmill;
    private TMP_Text sawText;
    void Start()
    {
        GameObject tempObj = GameObject.Find("SawText");
        sawText = tempObj.GetComponent<TMP_Text>();
        GameManager.Instance.resourceManager.Add("Planks", 0);
        sawmill = new passiveUpgrade("Planks", "Logs", 0, 100, 1f);
        StartCoroutine(sawmill.tick());
    }

    public void button_click() {
        sawmill.upgrade();
        sawText.text = "Sawmill\n" + sawmill.cost + " Logs";
    }
}
