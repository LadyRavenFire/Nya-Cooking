using System.Collections.Generic;
using UnityEngine;

// Скрипт описывающий печку

public class Stove : MonoBehaviour
{
    private readonly List<Item> _items = new List<Item>();

    public bool IsEnterCollider;
    public bool IsEmpty;

    private bool _isCooking;

    private float _timer;
    private bool _timerFlag;

    private Inventory _inventory;

    private Dictionary<Item.Name, Dictionary<Item.StateOfIncision, float>> _productTimers = new Dictionary<Item.Name, Dictionary<Item.StateOfIncision, float>>
    {
        {Item.Name.Bread,
            new Dictionary<Item.StateOfIncision, float>
            {
                { Item.StateOfIncision.Whole, 5 },
                { Item.StateOfIncision.Cutted, 3}
            }},
        {Item.Name.Meat,
            new Dictionary<Item.StateOfIncision, float>
            {
                { Item.StateOfIncision.Whole, 5 },
                { Item.StateOfIncision.Cutted, 3 }
            }}
    };

    void Start()
    {
        _items.Add(null);
        IsEnterCollider = false;
        IsEmpty = true;
        _isCooking = false;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _timer = 0;
        _timerFlag = false;
    }

    void Update()
    {
        if (!IsEmpty && _isCooking == false)
        {
            if (_items[0].ItemName == Item.Name.Meat && _timerFlag == false)
            {
                _timer = _productTimers[_items[0].ItemName][_items[0].stateOfIncision];
                _timerFlag = true;
            }
            _isCooking = true;            
            //print("EDA V NYTRI!!!");
        }
        
    }

    void FixedUpdate()
    {
        PreparingTimer();
    }

    void DeleteItem(int index)
    {
        _items[index] = null;
        IsEmpty = true;
        _isCooking = false;
        _timerFlag = false;
    }

    public void AddItem(Item item)
    {
        _items[0] = item;
        IsEmpty = false;
    }

    void OnMouseEnter()
    {
        IsEnterCollider = true;
    }

    void OnMouseExit()
    {
        IsEnterCollider = false;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && _isCooking)
        {
            _inventory.AddItem(_items[0]);
            DeleteItem(0);
        }
    }

    void Prepare()
    {
        if (_items[0].ItemName == Item.Name.Meat)
        {
            _items[0].stateOfPreparing = Item.StateOfPreparing.Fried;
            _items[0].UpdateTexture();        
        }
        _timerFlag = false;
        _timer = 0;
    }

    void PreparingTimer()
    {
        if (_isCooking && _timerFlag)
        {
            if (_timer > 0)
            {
                _timer-= Time.deltaTime;
            }

            if (_timer <= 0)
            {
                Prepare();
            }
        }
    }
}
