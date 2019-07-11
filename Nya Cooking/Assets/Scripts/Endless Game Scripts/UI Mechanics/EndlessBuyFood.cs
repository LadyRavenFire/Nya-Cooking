using UnityEngine;
using UnityEngine.UI;

// скрипт описывающий покупку еды в бесконечной игре
// TODO изменить процесс закупки еды, если это действительно требуется

public class EndlessBuyFood : MonoBehaviour
{
    private GameObject _buyMenuPanel;

    // Use this for initialization
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

        //поиск коробки с подходящим продуктом
        for (int i = 0; i < repositories.Length; i++)
        {
            var box = repositories[i].GetComponent<Repository>();
            if (box.StoredItemType != product) continue;
            else
            {
                var price = new Catalog().GetPriceOf(product); //поиск цены в каталоге
                if (endlessGameVariables.ReturnMoney() < price) break;

                box.AddtoRepository(1, product);
                endlessGameVariables.AddMoney(-price);
                break;
            }
        };
    }

    public void ExitBuyMenu()
    {
        _buyMenuPanel.SetActive(false);
    }
}
