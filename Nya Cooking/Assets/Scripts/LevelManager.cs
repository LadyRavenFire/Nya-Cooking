using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _money = 0;
    [SerializeField] private Text _textComponent;


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

    void UpdateMoney()
    {
        _textComponent.text = "Money: " + _money.ToString();
    }

}
