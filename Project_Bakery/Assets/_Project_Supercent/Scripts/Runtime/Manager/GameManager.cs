using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance = default;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }


    public int money = 5;
    private PlayerController player;
    private BreadStorage storage;

    public TMP_Text moneyText;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        ResourceManager.Init();
        player = GFunc.GetRootObj("Player").GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
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
