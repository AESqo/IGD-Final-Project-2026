using UnityEngine;

public class UP_Button : MonoBehaviour
{
    public GameObject UpgradePanel;
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void open_up()
    {
        UpgradePanel.SetActive(true);
    }
}
