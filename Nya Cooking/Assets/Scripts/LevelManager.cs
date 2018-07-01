using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _money = 0;
    private Text _textComponent;

    void Start()
    {
        //А тут ищем по тегу
        _textComponent = GameObject.FindGameObjectWithTag("MoneyText").GetComponent<Text>();
    }


    void Update()
    {
        //просто увеличиваем деньги каждый кадр и выводим их на верх
        _money++;
        UpdateMoney();
    }

    void AddMoney(int money)
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
    }

}
