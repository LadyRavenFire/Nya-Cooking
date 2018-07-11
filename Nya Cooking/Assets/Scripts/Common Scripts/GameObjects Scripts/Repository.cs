using UnityEngine;

// Скрипт описывающий коробки с едой

public class Repository : MonoBehaviour
{

    [SerializeField] private int _slotsCount = 100; // Максимальное количество предметов в коробке ///TODO определить максимальный объем
    [SerializeField] private Item.Name _storedItemType = Item.Name.Meat; ///TODO решить нужно ли сделать поле readonly

    private Item[] _items;   
    private bool _isEmpty; 

    private Inventory _inventory;
    private ItemDataBase _db;



    void Start ()
    {
        _items = new Item[_slotsCount];
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _db = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        _isEmpty = true;
        for (var i = 0; i < _slotsCount; i++)
        {
            _items[i] = null;
        }        
        //AddtoRepository(10,_storedItemType); //test    
	}

    public void AddtoRepository(int quantity, Item.Name type)
    {        
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                if (quantity > 0)
                {
                    _items[i] = _db.Generate(_storedItemType, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
                    if (_isEmpty)
                    {
                        _isEmpty = false;
                    }
                    quantity--;
                }            
            }
        }
    }

    public int CountOfItemsInRepository()
    {
        var count = 0;
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] != null)
            {
                count++;
            }
        }

        return count;
    }

    void AddFromRepository()
    {
        if (!_isEmpty)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    _inventory.AddItem(_items[i]);
                    _items[i] = null;
                    IsRepositoryEmpty();
                    break;
                }
            }
        }      
    }

    void IsRepositoryEmpty()
    {
        _isEmpty = true;
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] != null)
            {
                _isEmpty = false;
                break;
            }
        }        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !_isEmpty)
        {
            AddFromRepository();
        }
    }
}
