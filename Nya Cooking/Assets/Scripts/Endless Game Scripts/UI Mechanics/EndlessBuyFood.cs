﻿using UnityEngine;
using UnityEngine.UI;

// скрипт описывающий покупку еды в бесконечной игре
// TODO изменить процесс закупки еды, если это действительно требуется

public class EndlessBuyFood : MonoBehaviour
{
    private GameObject _buyMenuPanel;
    private Button _exitBuyMenu;
    private Button _buyMeat;
    private Button _buyBread;


	// Use this for initialization
	void Start () {
		_buyMenuPanel = GameObject.Find("BuyFoodPanel");
	    _exitBuyMenu = GameObject.Find("ExitBuyButton").GetComponent<Button>();
	    _buyBread = GameObject.Find("BreadBuyButton").GetComponent<Button>();
	    _buyMeat = GameObject.Find("MeatBuyButton").GetComponent<Button>();
	    
        _buyBread.onClick.AddListener(() => BuyProduct(Item.Name.Bread));
	    _buyMeat.onClick.AddListener(() => BuyProduct(Item.Name.Meat));
        _exitBuyMenu.onClick.AddListener(ExitBuyMenu);

        _buyMenuPanel.SetActive(false);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _buyMenuPanel.SetActive(true);
            //Time.timeScale = 1f;
        }
    }

    void BuyProduct(Item.Name product)
    {
        var endlessGameVariables = GameObject.Find("LevelManager").GetComponent<EndlessGameVariables>();
        if (endlessGameVariables.ReturnMoney() > 0)
        {
            switch (product)
            {
                case Item.Name.Bread:
                    var repositoryWithBread = GameObject.Find("BoxWithBread").GetComponent<Repository>();
                    repositoryWithBread.AddtoRepository(1, product);
                    endlessGameVariables.AddMoney(-40);
                    break;
                case Item.Name.Meat:
                    var repositoryWithMeat = GameObject.Find("BoxWithMeat").GetComponent<Repository>();
                    repositoryWithMeat.AddtoRepository(1, Item.Name.Meat);
                    endlessGameVariables.AddMoney(-50);
                    break;
            }
        }
    }

    void ExitBuyMenu()
    {
        _buyMenuPanel.SetActive(false);
        //Time.timeScale = 0f;
    }

}
