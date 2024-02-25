using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPile : MonoBehaviour
{
    [SerializeField]
    private List<Money> moneys = default;

    private float xOffset = 0.75f;
    private float zOffset = 0.55f;
    private float yOffset = 0.2f;

    private int xIndexer = default;
    private int zIndexer = default;
    private int yIndexer = default;

    private int moneyNum = default;

    private int moneyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        xIndexer = 0;
        zIndexer = 0;
        yIndexer = 0;
        moneys = new List<Money>();
        moneyCount = moneys.Count;
        SpawnMoney(30);
    }

    // Update is called once per frame
    void Update()
    {
        GetLastIndex();
    }

    void GetLastIndex()
    {
        moneyCount = moneys.Count;
    }
    public void SpawnMoney(int addNum)
    {
        float max = moneyCount + addNum;
        for (int i = 1; i <= max; i++)
        {
            Money money = Instantiate(ResourceManager.objects["Money"], transform).GetComponent<Money>();
            moneys.Add(money);
            moneyCount++;
            if(xIndexer > 2)
            {
                xIndexer = 0;
                zIndexer++;
            }

            if(moneyNum % 9 == 0)
            {
                yIndexer++;
                xIndexer = 0;
                zIndexer = 0;
            }

            money.transform.position = this.transform.position + new Vector3(xOffset * xIndexer, yOffset * yIndexer, -zOffset * zIndexer);
            xIndexer++;
            moneyNum++;
        }
    }

    IEnumerator GiveMoney(Vector3 playerPos)
    {
        int index = moneyCount - 1;
        while (moneyCount > 0)
        {
            moneys[index].SimulateProjectile(playerPos);
            GameManager.Instance.money++;
            moneys.RemoveAt(index);
            index--;            
            yield return null;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(GiveMoney(other.gameObject.transform.position));
        }
    }


}
