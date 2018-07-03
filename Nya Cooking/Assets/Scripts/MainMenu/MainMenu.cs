using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


        //Добавляем Listner кнопкам
        //Основного меню
        _campaignButton.onClick.AddListener(MainToCampaign);
        _endlessGameButton.onClick.AddListener(MainToEndless);
        _settingsButton.onClick.AddListener(MainToSettings);
        _quitButton.onClick.AddListener(QuitGame);

        //Кнопкам компании
        _campaignBackButton.onClick.AddListener(CampaignToMain);

        //Кнопкам бесконечной игры
        _endlessNewGameButton.onClick.AddListener(EndlessNewGame);
        _endlessContinueButton.onClick.AddListener(EndlessContinue);
        _endlessBackButton.onClick.AddListener(EndlessToMain);

        //Кнопкам настроек игры
        _settingsBackButton.onClick.AddListener(SettingsToMain);

        //отключаем ненужные на старте панели
        _campaignMenuBasic.SetActive(false);
        _endlessGameMenuBasic.SetActive(false);
        _settingsBasic.SetActive(false);
	}

    void MainToCampaign()
    {
        _mainMenuBasic.SetActive(false);
        _campaignMenuBasic.SetActive(true);
    }

    void MainToEndless()
    {
        _mainMenuBasic.SetActive(false);
        _endlessGameMenuBasic.SetActive(true);
    }

    void MainToSettings()
    {
        _mainMenuBasic.SetActive(false);
        _settingsBasic.SetActive(true);
    }

    void CampaignToMain()
    {
        _campaignMenuBasic.SetActive(false);
        _mainMenuBasic.SetActive(true);
    }

    void EndlessToMain()
    {
        _endlessGameMenuBasic.SetActive(false);
        _mainMenuBasic.SetActive(true);
    }

    void SettingsToMain()
    {
        _settingsBasic.SetActive(false);
        _mainMenuBasic.SetActive(true);
    }

    void EndlessNewGame()
    {
        PlayerPrefs.SetInt("EndlessGameMoney", 0);
        SceneManager.LoadScene("Test level");
    }

    void EndlessContinue()
    {
        SceneManager.LoadScene("Test level");
    }

    void QuitGame()
    {
        Application.Quit();
    }

}
