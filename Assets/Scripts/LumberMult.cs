using UnityEngine;
using TMPro;

public class LumberMult : MonoBehaviour
{
    public multUpgrade LumMult;
    private TMP_Text LumMultText;
    void Awake()
    {
        GameObject tempObj = GameObject.Find("LumberMultText");
        LumMultText = tempObj.GetComponent<TMP_Text>();
        LumMult = new multUpgrade("Lumber Multiplier", resourceType.Planks, 0, 10, 200);
    }

    public void Upgrade()
    {
        LumMult.Upgrade();
        LumMultText.text = "Lumber Multiplier\n" + LumMult.cost + " Planks";
    }
}
