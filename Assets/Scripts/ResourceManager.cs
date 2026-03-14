using UnityEngine;
using System.Collections.Generic;
public enum resourceType{
        Logs,
        Planks
    }
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    public Dictionary<resourceType, double> rManage = new Dictionary<resourceType, double>();
    public GameObject lumberMult;
    void Awake()
    {
        if (Instance == null) {Instance = this;}
        else {Destroy(gameObject);}
    }

    public void add_resource(resourceType type, resourceType costType, int amount, int needed)
    {
        switch(type)
        {
            case resourceType.Logs:
                amount = (int) (amount * (1f + (lumberMult.GetComponent<LumberMult>().LumMult.tier / 2f)));
                rManage[resourceType.Logs] += (int)amount;
                break;
            case resourceType.Planks:
                if (rManage[resourceType.Logs] >= needed)
                {
                    rManage[resourceType.Planks] += amount;
                    rManage[resourceType.Logs] -= amount;
                }
                break;
        }
    }
}
public class passiveUpgrade
{
    public resourceType type;
    public resourceType costType;
    public int add;
    public int boost;
    public int cost;
    public float timer;
    public int timesBought;
    public passiveUpgrade(){}
    public passiveUpgrade(resourceType type, resourceType costType, int add, int boost, int cost, float timer, int timesBought) { this.type = type; this.costType = costType; this.add = add; this.boost = boost; this.cost = cost; this.timer = timer; this.timesBought = timesBought;}

    public void upgrade()
    {
        if (ResourceManager.Instance.rManage[this.costType] >= this.cost) {
            ResourceManager.Instance.rManage[this.costType] -= this.cost;
            this.add += this.boost;
            this.cost = (int)(this.cost * 1.5f);
            this.timesBought++;
        }
    }

    public System.Collections.IEnumerator tick()
    {
        while(true) {
            yield return new WaitForSeconds(this.timer);
            ResourceManager.Instance.add_resource(this.type, this.costType, this.add, this.boost * this.timesBought);
        }
    }
}

public class multUpgrade
{
    public string name;
    public resourceType costType;
    public int tier;
    public int tierMax;
    public int cost;
    public multUpgrade(){name = "ERROR";}
    public multUpgrade(string name, resourceType costType, int tier, int tierMax, int cost){this.name = name; this.costType = costType; this.tier = tier; this.tierMax = tierMax; this.cost = cost;}

    public void Upgrade()
    {
        if(this.tier < this.tierMax && ResourceManager.Instance.rManage[this.costType] >= this.cost)
        {
            ResourceManager.Instance.rManage[this.costType] -= this.cost;
            this.cost = (int)(this.cost * 1.5f);
            this.tier += 1;
        }
    }
}