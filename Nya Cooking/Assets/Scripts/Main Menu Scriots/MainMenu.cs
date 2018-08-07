using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Скрипт описывающий работу кнопок и загрузок цен в главном меню 

public class MainMenu : MonoBehaviour
{
    //все панели меню
    private GameObject _mainMenuBasic;
    private GameObject _campaignMenuBasic;
    private GameObject _endlessGameMenuBasic;
    private GameObject _settingsBasic;

    //кнопки основного меню
    private Button _campaignButton;
    private Button _endlessGameButton;
    private Button _settingsButton; // пока не активно
    private Button _quitButton;

    //кнопки меню компании
    private Button _campaignNewGameButton;
    private Button _campaignContinueButton;
    private Button _campaignBackButton;

    //кнопки бесконечной игры
    private Button _endlessNewGameButton;
    private Button _endlessContinueButton;
    private Button _endlessBackButton;

    //кнопки настроек
    private Button _settingsBackButton;

    // Use this for initialization
    void Start () {

        //ищем панели меню
		_mainMenuBasic = GameObject.Find("MainMenuBasic");
        _campaignMenuBasic = GameObject.Find("CampaignMenuBasic");
        _endlessGameMenuBasic = GameObject.Find("EndlessGameMenuBasic");
        _settingsBasic = GameObject.Find("SettingsBasic");

        //ищем кнопки основного меню
        _campaignButton = GameObject.Find("CampaignButton").GetComponent<Button>();
        _endlessGameButton = GameObject.Find("EndlessGameButton").GetComponent<Button>();
        _settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        _quitButton = GameObject.Find("QuitButton").GetComponent<Button>();

        //ищем кнопки компании
        _campaignNewGameButton = GameObject.Find("NewGameCampaignButton").GetComponent<Button>();
        _campaignContinueButton = GameObject.Find("ContinueCampaignButton").GetComponent<Button>();
        _campaignBackButton = GameObject.Find("BackCampaignButton").GetComponent<Button>();

        //ищем кнопки бесконечной игры
        _endlessNewGameButton = GameObject.Find("NewGameEndlessButton").GetComponent<Button>();
        _endlessContinueButton = GameObject.Find("ContinueEndlessButton").GetComponent<Button>();
        _endlessBackButton = GameObject.Find("BackEndlessButton").GetComponent<Button>();

        //ищем кнопки настроек
        _settingsBackButton = GameObject.Find("BackSettingsButton").GetComponent<Button>();

        //=======================================
        //       Добавляем Listener кнопкам
        //=======================================

        //            Основного меню

        _campaignButton.onClick.AddListener(() =>
        {
            _mainMenuBasic.SetActive(false);
            _campaignMenuBasic.SetActive(true);
        });
        _endlessGameButton.onClick.AddListener(() =>
        {
            _mainMenuBasic.SetActive(false);
            _endlessGameMenuBasic.SetActive(true);
        });
        _settingsButton.onClick.AddListener(() =>
        {
            _mainMenuBasic.SetActive(false);
            _settingsBasic.SetActive(true);
        });
        _quitButton.onClick.AddListener(Application.Quit);

        // Кнопкам компании
        _campaignNewGameButton.onClick.AddListener(() =>
        {

        });

        _campaignContinueButton.onClick.AddListener(() =>
        {

        });

        _campaignBackButton.onClick.AddListener(() =>
        {
            _campaignMenuBasic.SetActive(false);
            _mainMenuBasic.SetActive(true);
        });

        // Кнопкам бесконечной игры

        _endlessNewGameButton.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("EndlessGameMoney", 0);
            PlayerPrefs.SetInt("EndlessMeatInBox", 5);
            PlayerPrefs.SetInt("EndlessBreadInBox", 5);
            PlayerPrefs.SetFloat("EndlessStoveUpgrade", 1f);
            SceneManager.LoadScene("Test level");
        });
        _endlessContinueButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Test level");
        });
        _endlessBackButton.onClick.AddListener(() =>
        {
            _endlessGameMenuBasic.SetActive(false);
            _mainMenuBasic.SetActive(true);
        });

        // Кнопкам настроек игры

        _settingsBackButton.onClick.AddListener(() =>
        {
            _settingsBasic.SetActive(false);
            _mainMenuBasic.SetActive(true);
        });

        // отключаем ненужные на старте панели

        _campaignMenuBasic.SetActive(false);
        _endlessGameMenuBasic.SetActive(false);
        _settingsBasic.SetActive(false);
	}
}
