using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompanyLevel1Manager : MonoBehaviour {

    private int _money;
    private Text _textComponent;
    private Button _menuButton;

    private Repository _repositoryWithMeat;
    private Repository _repositoryWithBread;

    private Button _recipes;
    private Button _exitRecipes;
    private GameObject _recipesScrollView;

    void Start()
    {
        _textComponent = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<Text>();
        _menuButton = GameObject.FindGameObjectWithTag("MenuButton").GetComponent<Button>();

        //_inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        _repositoryWithBread = GameObject.Find("BoxWithBread").GetComponent<Repository>();
        _repositoryWithMeat = GameObject.Find("BoxWithMeat").GetComponent<Repository>();

        _menuButton.onClick.AddListener(GoToMainMenu);

        _recipes = GameObject.Find("RecipesButton").GetComponent<Button>();
        _recipesScrollView = GameObject.Find("RecipesUI");
        _exitRecipes = GameObject.Find("ExitRecipesButton").GetComponent<Button>();

        _recipes.onClick.AddListener(OpenRecipes);
        _exitRecipes.onClick.AddListener(CloseRecipes);

        _recipesScrollView.SetActive(false);

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
        _money = PlayerPrefs.GetInt("CompanyGameMoney");
        //print(_repositoryWithBread);
        _repositoryWithBread.AddtoRepository(PlayerPrefs.GetInt("CompanyBreadInBox"));
        //print("SMTH");
        _repositoryWithMeat.AddtoRepository(PlayerPrefs.GetInt("CompanyMeatInBox"));
    }

    public void SaveToData()
    {
        PlayerPrefs.SetInt("CompanyGameMoney", _money);
        PlayerPrefs.SetInt("CompanyBreadInBox", _repositoryWithMeat.ItemsCount);
        PlayerPrefs.SetInt("CompanyMeatInBox", _repositoryWithBread.ItemsCount);
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

    void OpenRecipes()
    {
        _recipesScrollView.SetActive(true);
        Time.timeScale = 0f;
    }

    void CloseRecipes()
    {
        _recipesScrollView.SetActive(false);
        Time.timeScale = 1f;
    }
}
