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
 
    public void button_click()
    {
        try
        {
            if (!first_click)
            {
                sawmill = new passiveUpgrade(resourceType.Planks, resourceType.Logs, 0, 4, 100, 3f, 0);
                first_click = true;
 
                // Call the public trigger method instead of invoking the event directly.
                // GameManager fires its own event internally from there.
                GameManager.TriggerSawmillUnlocked();
 
                StartCoroutine(sawmill.tick());
            }
 
            sawmill.upgrade();
            sawText.text = "Sawmill\n" + sawmill.cost + " Logs";
        }
        catch (System.Collections.Generic.KeyNotFoundException e)
        {
            Debug.LogWarning("SawmillButton: Resource key missing. " + e.Message);
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogWarning("SawmillButton: Null reference (sawText or sawmill). " + e.Message);
        }
    }
}