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
    public PlayerController player;
    public TMP_Text moneyText;
    private Arrow arrow;


    public bool isFirstOven = default;
    public bool isFirstStorage = default;
    public bool isFirstPos = default;
    public bool isFirstMoneyPile = default;
    public bool isFirstTempArea = default;
    private void Awake()
    {
        Init();

    }

    void Init()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        ResourceManager.Init();
        player = GFunc.GetRootObj("Player").GetComponent<PlayerController>();
        arrow = GFunc.GetRootObj("Arrow").GetComponent<Arrow>();
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
