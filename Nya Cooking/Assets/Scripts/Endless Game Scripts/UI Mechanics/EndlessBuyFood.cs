using UnityEngine;
using UnityEngine.UI;

// скрипт описывающий покупку еды в бесконечной игре
// TODO изменить процесс закупки еды, если это действительно требуется

public class EndlessBuyFood : MonoBehaviour
{
    private GameObject _buyMenuPanel;

    void Start ()
	{
		_buyMenuPanel = GameObject.Find("BuyFoodPanel");
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

    public static void BuyProduct(Item.Name product)
    {
        var endlessGameVariables = GameObject.Find("LevelManager").GetComponent<EndlessGameVariables>();
        var repositories = GameObject.FindGameObjectsWithTag("Repository");

        var catalog = FindObjectOfType<Catalog>();
        var price = catalog.GetPriceOf(product);


        if (endlessGameVariables.ReturnMoney() >= price)
        {
            catalog.GetRepository(product).AddtoRepository(1);
            endlessGameVariables.AddMoney(-price);
        }
    }

    public void ExitBuyMenu()
    {
        _buyMenuPanel.SetActive(false);
    }
}
