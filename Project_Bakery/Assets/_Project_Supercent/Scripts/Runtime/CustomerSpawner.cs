using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{

    private int maxCapacity = 5;
    private int curCapacity = 0;
    private bool isMax = default;

    IEnumerator routine = default;
    // Start is called before the first frame update
    void Start()
    {
        isMax = false;
        routine = Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsSpawnable())
        {
            StartCoroutine(routine);
        }
        else
        {
            StopCoroutine(routine);
        }
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            float randDelay = Random.Range(2f, 10f);
            SpawnCustomer();
            yield return new WaitForSeconds(randDelay);
        }
    }

    bool IsSpawnable()
    {
        if(curCapacity < maxCapacity)
        {
            return true;
        }
        return false;
    }
    private void SpawnCustomer()
    {
        GameObject customer = Instantiate(ResourceManager.objects["Customer"], this.transform);
        curCapacity++;
    }
}
