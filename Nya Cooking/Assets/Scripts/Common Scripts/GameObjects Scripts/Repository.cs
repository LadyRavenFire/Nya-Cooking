using UnityEngine;

// Скрипт описывающий коробки с едой

public class Repository : MonoBehaviour
{

    [SerializeField] private int _slotsCount = 100; // Максимальное количество предметов в коробке ///TODO определить максимальный объем
    [SerializeField] private Item.Name _storedItemType = Item.Name.Meat; ///TODO решить нужно ли сделать поле readonly

    public bool IsEmpty { get { return _itemsCount == 0; } }
    public int ItemsCount { get { return _itemsCount; } }

    private int _itemsCount;

    private Inventory _inventory;

    public Item.Name StoredItemType { get { return _storedItemType; } }
    
    void Start ()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();  
	}

    public void AddtoRepository(int quantity)
    {
        _itemsCount += quantity;
    }

    void ExtractItem()
    {
        if (IsEmpty) return; //здесь сделать вывод сообщения о том, что коробка пуста
        _inventory.AddItem(_storedItemType, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw);
        _itemsCount -= 1;    
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !IsEmpty)
        {
            ExtractItem();
        }
    }
}
