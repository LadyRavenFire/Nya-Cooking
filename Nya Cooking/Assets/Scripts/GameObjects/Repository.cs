using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repository : MonoBehaviour
{
    [SerializeField]
    private int _slotsCount = 100;
    private Item[] _items;
    
    private bool _isEmpty;

    private Inventory _inventory;
    private ItemDataBase _db;

    [SerializeField] private Item.Name _storedItemType = Item.Name.Meat;

    void Start ()
    {
        _items = new Item[_slotsCount];
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _db = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        _isEmpty = true;
        for (int i = 0; i < _slotsCount; i++)
        {
            _items[i] = null;
        }
        
        AddtoRepository(10,_storedItemType); //test    
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

    void AddFromRepository()
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
