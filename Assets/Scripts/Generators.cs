using UnityEngine;

public abstract class Generator
{
    public resourceType type;
    public resourceType typecost;
    public int add;
    public int boost;
    public int cost;
    public float timer;
    public int timesbought;

    public Generator(resourceType type, resourceType typecost,
                        int add, int boost, int cost, float timer, int timesbought)
    {
        this.type = type;
        this.typecost = typecost;
        this.add = add;
        this.boost = boost;
        this.cost = cost;
        this.timer = timer;
        this.timesbought = timesbought;
    }

    public abstract void Produce(ref double resourcetotal);

    public bool TryPurchase(out string message)
    {
        double balance = ResourceManager.Instance.rManage[this.typecost];
        if (balance >= this.cost)
        {
            ResourceManager.Instance.rManage[this.typecost] -= this.cost;
            this.add += this.boost;
            this.cost = (int)(this.cost * 1.5f);
            this.timesbought++;
            message = "Purchase successful!";
            return true;
        }

        message = "Not enough " + this.typecost.ToString();
        return false;
    }


    public System.Collections.IEnumerator Tick()
    {
        while (true)
    
        {
            yield return new WaitForSeconds(this.timer);
            if (!ResourceManager.Instance.rManage.ContainsKey(this.type))
                continue;
           
            double val = ResourceManager.Instance.rManage[this.type];
            Produce(ref val);
            ResourceManager.Instance.rManage[this.type] = val;
        }
    }
}
    public class Lumberjackgen : Generator
{
    public Lumberjackgen(int add, int boost, int cost, float timer)
    : base(resourceType.Logs, resourceType.Logs, add, boost, cost, timer, 0) { }

    public override void Produce(ref double resourcetotal)
    {
        float mult = 1f + (ResourceManager.Instance.lumberMult.GetComponent<LumberMult>().LumMult.tier / 2f);
        resourcetotal += (int)(this.add * mult);
    }
}

public class sawmillgen : Generator
{
    public sawmillgen(int add, int boost, int cost, float timer)
        : base(resourceType.Planks, resourceType.Logs, add, boost, cost, timer, 0) { }
    public override void Produce(ref double resourcetotal)
    {
        if ((!ResourceManager.Instance.rManage.ContainsKey(resourceType.Planks)) || !ResourceManager.Instance.rManage.ContainsKey(resourceType.Logs))
            return;
           
        int logsneeded = this.boost * this.timesbought;
        if (ResourceManager.Instance.rManage[resourceType.Logs] >= logsneeded)
        {
            resourcetotal += this.add;
            ResourceManager.Instance.rManage[resourceType.Logs] -= this.add;
        }
    }
}

        