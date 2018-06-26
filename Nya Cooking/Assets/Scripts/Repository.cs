using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repository : MonoBehaviour {

    private readonly List<Item> _itemInRepository = new List<Item>();
    private Inventory _inventory;
    private ItemDataBase _db;
    private bool _isEmpty;
    [SerializeField] private int _slotsInRepository = 100;
    [SerializeField] private string _typeOfThingsOfRepository = "Meat";

    void Start ()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _db = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        _isEmpty = true;
        for (int i = 0; i < _slotsInRepository; i++)
        {
            _itemInRepository.Add(null);
        }
        
        AddtoRepository(2,_typeOfThingsOfRepository); //test    
	}

    public void AddtoRepository(int quantity, string type)
    {        
        for (int i = 0; i < _itemInRepository.Count; i++)
        {
            if (_itemInRepository[i] == null)
            {
                if (quantity > 0)
                {
                    _itemInRepository[i] = SearchForProduct(type);
                    if (_isEmpty)
                    {
                        _isEmpty = false;
                    }
                    quantity--;
                }            
            }
        }
    }

    Item SearchForProduct(string type)
    {
        if (type == "Meat")
        {
            return _db.Generate(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);  
        }

        if (type == "Bread")
        {
            return _db.Generate(Item.Name.Bread, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);          
        }
        return null;
    }

    void AddFromRepository()
    {
        for (int i = 0; i < _itemInRepository.Count; i++)
        {
            if (_itemInRepository[i] != null)
            {
                _inventory.AddItem(_itemInRepository[i]);
                _itemInRepository[i] = null;
                IsRepositoryEmpty();
                break;
            }          
        }
    }

    void IsRepositoryEmpty()
    {
        _isEmpty = true;
        for (int i = 0; i < _itemInRepository.Count; i++)
        {
            if (_itemInRepository[i] != null)
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
