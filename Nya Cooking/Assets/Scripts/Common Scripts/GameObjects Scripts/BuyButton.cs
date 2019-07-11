using UnityEngine;
using UnityEngine.UI;

class BuyButton: MonoBehaviour
{
    [SerializeField]
    private Item.Name _buyItemType = Item.Name.Meat;

    public Item.Name BuyItemType { get { return _buyItemType; } }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener( () => EndlessBuyFood.BuyProduct(_buyItemType) );
        //подписываем цену согласно каталогу
        GetComponentInChildren<Text>().text = new Catalog().GetPriceOf(_buyItemType).ToString();
    }
}
