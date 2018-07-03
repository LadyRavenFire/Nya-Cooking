﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndlessGameVariables : MonoBehaviour {

    private int _money;
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
        print(PlayerPrefs.GetInt("EndlessGameMoney"));
        _money = PlayerPrefs.GetInt("EndlessGameMoney");
    }

    public void SaveToData()
    {
        PlayerPrefs.SetInt("EndlessGameMoney", _money);
    }

    void GoToMainMenu()
    {
        SaveToData();
        print("Money save");
        print(PlayerPrefs.GetInt("EndlessGameMoney"));
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateMoney()
    {
        _textComponent.text = "Money: " + _money.ToString();
    }

}
