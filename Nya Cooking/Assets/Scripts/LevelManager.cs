using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int Money = 0;
    [SerializeField] private Text _textComponent;

    void Update()
    {
        Money++;
        UpdateMoney();
    }

    void UpdateMoney()
    {
        _textComponent.text = "Money: " + Money.ToString();
    }
}
