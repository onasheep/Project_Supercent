using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPile : MonoBehaviour
{
    private List<Money> moneys = default;

    private float xOffset = 0.75f;
    private float zOffset = 0.55f;
    private float yOffset = 0.2f;

    private int xIndexer = default;
    private int zIndexer = default;
    private int yIndexer = default;

    private int moneyNum = default;

    private int moneyCount = 0;
    private void Awake()
    {
        Init();
    }
    void Init()
    {
        xIndexer = 0;
        zIndexer = 0;
        yIndexer = 0;
        moneys = new List<Money>();
        moneyCount = moneys.Count;
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
        float max = moneys.Count + addNum;
        for (int i = moneyCount; i < max; i++)
        {
            Money money = Instantiate(ResourceManager.objects["Money"], transform).GetComponent<Money>();
            moneys.Add(money);
            money.transform.position = this.transform.position + new Vector3(xOffset * xIndexer, yOffset * yIndexer, -zOffset * zIndexer);

            xIndexer++;
            moneyNum++;

            if (moneyNum % 9 == 0)
            {
                yIndexer++;
                zIndexer = 0;
                xIndexer = 0;
            }
            else if (moneyNum % 3 == 0)
            {
                xIndexer = 0;
                zIndexer++;
            }
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
            SoundManager.Instance.OnPlayClip(RDefine.SFX_GET);
            yield return null;
        }
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(GiveMoney(other.gameObject.transform.position));
        }
    }


}
