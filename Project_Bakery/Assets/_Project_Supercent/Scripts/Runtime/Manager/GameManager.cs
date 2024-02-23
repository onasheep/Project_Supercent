using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public int money = 5;
    private PlayerController player;
    public TMP_Text moneyText;
    private void Awake()
    {
        ResourceManager.Init();
        player = FindObjectOfType<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(player == null);
        //Debug.Log(ResourceManager.objects[RDefine.OBJECT_CROASSANT].name);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoneyText();
    }

    void UpdateMoneyText()
    {
        moneyText.text = money.ToString();
    }
}
