using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Text;

public class passiveUpgrade
{
    public string name;
    public string costName;
    public int add;
    public int cost;
    public float timer;
    public passiveUpgrade() { name = "Not assigned"; }
    public passiveUpgrade(string name, string costName, int add, int cost, float timer) { this.name = name; this.costName = costName; this.add = add; this.cost = cost; this.timer = timer; }

    public void upgrade()
    {
        if (GameManager.Instance.resourceManager[this.costName] >= this.cost) {
            GameManager.Instance.resourceManager[this.costName] -= this.cost;
            this.add += 1;
            this.cost = (int)(this.cost * 1.5f);
        }
    }

    public System.Collections.IEnumerator tick()
    {
        while(true) {
            yield return new WaitForSeconds(this.timer);
            GameManager.Instance.resourceManager[this.name] += this.add;
        }
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Dictionary<string, double> resourceManager = new Dictionary<string, double>();
    private TMP_Text resourceText;
    private StringBuilder tempText = new StringBuilder();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null) {Instance = this;}
        else {Destroy(gameObject);}
    }

    void Start()
    {
        GameObject tempObj = GameObject.Find("ResourceText");
        resourceText = tempObj.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tempText.Clear();
        foreach (KeyValuePair<string, double> dics in resourceManager) {
            tempText.Append(dics.Key).Append(": ").Append(dics.Value).Append(" ");
        }
        resourceText.text = tempText.ToString();
    }
}
