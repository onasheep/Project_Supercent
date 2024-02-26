using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyPos : MonoBehaviour
{
    public GameObject tempArea = default;
    public GameObject sittingArea = default;
    private int cost = 30;

    private bool isEnter = default;
    private bool affordable = default;
    private bool isGiving = default;
    private TMP_Text costText = default;
    
    // Start is called before the first frame update
    void Start()
    {
        costText = gameObject.FindChildComponent<TMP_Text>("MoneyNeedText");
    }

    // Update is called once per frame
    void Update()
    {
        affordable = GameManager.Instance.money >= cost ? true : false;
        costText.text = cost.ToString();

        if (isEnter == true)
        {
            if(isGiving == false)
            {
                StartCoroutine(GetMoney());
            }
        }
    }

    IEnumerator GetMoney()
    {
        isGiving = true;
        SoundManager.Instance.OnPlayClip(RDefine.SFX_MONEY);
        while (cost > 0 && affordable)
        {
            GameObject money = Instantiate(ResourceManager.objects["Money"], GameManager.Instance.player.transform.position, Quaternion.identity);
            money.GetComponent<Money>().SimulateProjectile(this.transform.position);
            cost--;
            GameManager.Instance.money--;
            yield return null;
        }
        tempArea.SetActive(false);
        SoundManager.Instance.OnPlayClip(RDefine.SFX_SUCCESS);
        Instantiate(ResourceManager.vfx["VFX_AppearSignStand"], this.transform.position + Vector3.up,Quaternion.identity);
        sittingArea.SetActive(true);
        TweenScale();
        isGiving = false;
    }    


    private void TweenScale()
    {
        float t = 0f;
        if(t < 0.5f)
        {
            t += Time.deltaTime;
            sittingArea.transform.localScale += new Vector3(t, t, t);
        }
        else
        {
            t -= Time.deltaTime;
            sittingArea.transform.localScale -= new Vector3(t, t, t);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isEnter = false;
        }
    }
}
