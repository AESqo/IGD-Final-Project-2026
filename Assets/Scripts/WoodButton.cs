using UnityEngine;

public class WoodButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.resourceManager.Add("Logs", 0);
    }
    public void add_wood() {
        GameManager.Instance.resourceManager["Logs"] += 1;
    }
}
