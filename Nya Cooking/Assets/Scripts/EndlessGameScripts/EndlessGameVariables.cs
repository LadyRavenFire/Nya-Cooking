using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndlessGameVariables : MonoBehaviour {

    private int _money = 0;
    private Text _textComponent;
    private Button _menuButton;

    void Start()
    {
        LoadFromData();
        _textComponent = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<Text>();
        _menuButton = GameObject.FindGameObjectWithTag("MenuButton").GetComponent<Button>();

        _menuButton.onClick.AddListener(GoToMainMenu);
    }

    void Update()
    {
        _money++;
        UpdateMoney();
    }

    void LoadFromData()
    {
        PlayerPrefs.GetInt("EndlessGameMoney", _money);
    }

    public void SaveToData()
    {
        PlayerPrefs.SetInt("Money", _money);
    }

    void GoToMainMenu()
    {
        SaveToData();
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateMoney()
    {
        _textComponent.text = "Money: " + _money.ToString();
    }

}
