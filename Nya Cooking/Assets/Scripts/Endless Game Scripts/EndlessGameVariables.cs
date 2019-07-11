using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Система Save Load в бесконечной игре

public class EndlessGameVariables : MonoBehaviour {

    private int _money;
    private Text _textComponent;
    private Button _menuButton;

    //need to be changed
    private Repository _repositoryWithMeat;
    private Repository _repositoryWithBread;
    private Repository _repositoryWithPotato;

    //private Inventory _inventory;
    //TODO реализовать сохранение и загрузку предметов в инвентаре

    void Start()
    {
        _textComponent = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<Text>();
        _menuButton = GameObject.FindGameObjectWithTag("MenuButton").GetComponent<Button>();

        //_inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        _repositoryWithBread = GameObject.Find("BoxWithBread").GetComponent<Repository>();
        _repositoryWithMeat = GameObject.Find("BoxWithMeat").GetComponent<Repository>();
        _repositoryWithPotato = GameObject.Find("BoxWithPotato").GetComponent<Repository>();

        _menuButton.onClick.AddListener(GoToMainMenu);

        LoadFromData();
    }

    void Update()
    {
        //_money++;
        UpdateMoney();
    }

    void LoadFromData()
    {
        //print(PlayerPrefs.GetInt("EndlessGameMoney"));
        _money = PlayerPrefs.GetInt("EndlessGameMoney");
        //print(_repositoryWithBread);
        _repositoryWithBread.AddtoRepository(PlayerPrefs.GetInt("EndlessBreadInBox"), Item.Name.Bread);
        //print("SMTH");
        _repositoryWithMeat.AddtoRepository(PlayerPrefs.GetInt("EndlessMeatInBox"), Item.Name.Meat);
        _repositoryWithPotato.AddtoRepository(5, Item.Name.Potato);
    }

    public void SaveToData()
    {
        PlayerPrefs.SetInt("EndlessGameMoney", _money);
        PlayerPrefs.SetInt("EndlessMeatInBox", _repositoryWithMeat.CountOfItemsInRepository());
        PlayerPrefs.SetInt("EndlessBreadInBox", _repositoryWithBread.CountOfItemsInRepository());
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateMoney()
    {
        _textComponent.text = "Money: " + _money.ToString();
    }

    public void AddMoney(int moneyToAdd)
    {
        _money += moneyToAdd;
    }

    public int ReturnMoney()
    {
        return _money;
    }

}
