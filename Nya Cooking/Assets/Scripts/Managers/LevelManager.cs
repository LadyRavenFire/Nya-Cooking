using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    //[SerializeField] private int _money = 0;
    private Text _textComponent;
    private Button _pauseMenuButton;
    private GameObject _pauseComponent;
    private Button _resumeGameButton;
    private Button _quitGameButton;

    void Start()
    {
        //А тут ищем по тегу
        _textComponent = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<Text>();
        _pauseMenuButton = GameObject.FindGameObjectWithTag("PauseMenuButton").GetComponent<Button>();
        _pauseComponent = GameObject.FindGameObjectWithTag("PauseMenu");
        _resumeGameButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        _quitGameButton = GameObject.Find("QuitButton").GetComponent<Button>();

        _pauseMenuButton.onClick.AddListener(PauseGame);
        _resumeGameButton.onClick.AddListener(ResumeGame);
        _quitGameButton.onClick.AddListener(QuitGame);

        _pauseComponent.SetActive(false);
    }


    void Update()
    {
        //просто увеличиваем деньги каждый кадр и выводим их на верх
        //_money++;
        //UpdateMoney();
    }

    void QuitGame()
    {
        //перед выходом нужно сохранить все данные (наверное...)
        //SaveMoneyToData();
        Application.Quit();
    }

    void ResumeGame()
    {
        _pauseComponent.SetActive(false);
        Time.timeScale = 1f;
    }
    void PauseGame()
    {
        //timescale паузит игру, тоесть паузит все кроме того,что делается в update и gui
        Time.timeScale = 0f;
        _pauseComponent.SetActive(true);
    }

   /* void AddMoney(int money)
    {
        _money += money;
    }

    void RemoveMoney(int money)
    {
        _money -= money;
    }
    //Сохранение денег
    public void SaveMoneyToData()
    {
        PlayerPrefs.SetInt("Money", _money);
    }
    //Загрузка денег
    void LoadMoneyFromData()
    {
        PlayerPrefs.GetInt("Money", _money);
    }

    void UpdateMoney()
    {
        _textComponent.text = "Money: " + _money.ToString();
    }*/

}
