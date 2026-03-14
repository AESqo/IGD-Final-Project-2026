using UnityEngine;

public class WoodButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }
    public void add_wood() {
        ResourceManager.Instance.rManage[resourceType.Logs] += 1;
    }
}
