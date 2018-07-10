using UnityEngine;
using UnityEngine.UI;

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
	    
        _buyBread.onClick.AddListener(BuyBread);
	    _buyMeat.onClick.AddListener(BuyMeat);
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

    void BuyBread()
    {
        var endlessGameVariables = GameObject.Find("LevelManager").GetComponent<EndlessGameVariables>();
        if (endlessGameVariables.ReturnMoney() > 0)
        {
            var _repositoryWithBread = GameObject.Find("BoxWithBread").GetComponent<Repository>();
            _repositoryWithBread.AddtoRepository(1, Item.Name.Bread);
            endlessGameVariables.AddMoney(-40);
        }     
    }

    void BuyMeat()
    {
        var endlessGameVariables = GameObject.Find("LevelManager").GetComponent<EndlessGameVariables>();
        if (endlessGameVariables.ReturnMoney() > 0)
        {
            var _repositoryWithBread = GameObject.Find("BoxWithMeat").GetComponent<Repository>();
            _repositoryWithBread.AddtoRepository(1, Item.Name.Meat);
            endlessGameVariables.AddMoney(-40);
        }
    }

    void ExitBuyMenu()
    {
        _buyMenuPanel.SetActive(false);
        //Time.timeScale = 0f;
    }

}
