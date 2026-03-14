using UnityEngine;

public class UpgradePanelExit : MonoBehaviour
{
    GameObject daddy;
    void Start()
    {
        daddy = transform.parent.gameObject;
    }
    public void exit_panel()
    {
        daddy.SetActive(false);
    }
}
