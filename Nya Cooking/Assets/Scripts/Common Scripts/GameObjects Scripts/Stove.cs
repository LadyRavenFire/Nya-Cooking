using System.Collections.Generic;
using UnityEngine;

// Скрипт описывающий печку

public class Stove : MonoBehaviour
{
    private Item _item;
    private Inventory _inventory;
    private bool _isCooking;
    private float _timer;
    private float _upgrade = 1f;

    private Dictionary<Item.Name,Dictionary<Item.StateOfPreparing, Dictionary<Item.StateOfIncision, float>>> _productTimers = new Dictionary<Item.Name, Dictionary<Item.StateOfPreparing, Dictionary<Item.StateOfIncision, float>>>
    {
        {
            Item.Name.Meat, new Dictionary<Item.StateOfPreparing, Dictionary<Item.StateOfIncision, float>>
            {
                {
                    Item.StateOfPreparing.Raw, new Dictionary<Item.StateOfIncision, float>
                    {
                        {Item.StateOfIncision.Whole, 5},
                        {Item.StateOfIncision.Cutted, 3}
                    }                    
                },

                {
                    Item.StateOfPreparing.Fried, new Dictionary<Item.StateOfIncision, float>
                    {
                        {Item.StateOfIncision.Whole, 10},
                        {Item.StateOfIncision.Cutted, 2}
                    }
                },

                {
                    Item.StateOfPreparing.Burnt, new Dictionary<Item.StateOfIncision, float>
                    {
                        {Item.StateOfIncision.Whole, 666},
                        {Item.StateOfIncision.Cutted, 666}
                    }
                }
            }
        }
    };

    void Start()
    {
        _item = null;
        _isCooking = false;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        print(_upgrade);
    }

    void Update()
    {
        if (!IsEmpty() && _isCooking == false)
        {
            if (_item.ItemName == Item.Name.Meat)
            {
                _timer = _productTimers[_item.ItemName][_item.stateOfPreparing][_item.stateOfIncision];
            }
            _isCooking = true;
        }       
    }

    public void Upgrade(float level)
    {
        _upgrade = level;
    }

    void FixedUpdate()
    {
        if (_isCooking && !IsEmpty())
        {
            PreparingTimer();
        }
    }

    void DeleteItem()
    {
        _item = null;
        _isCooking = false;
    }

    public void AddItem(Item item)
    {
        _item = item;
    }

    void OnMouseEnter()
    {
        _inventory.IsInOther();
    }

    void OnMouseExit()
    {
        _inventory.IsNotInOther();
    }

    void OnMouseOver()
    {
        if (IsEmpty() && Input.GetMouseButtonUp(0) && _inventory.IsDragged())
        {
            AddItem(_inventory.GiveDraggedItem());
            _inventory.DeleteDraggedItem();
        }

        if (Input.GetMouseButtonUp(0) && _inventory.IsDragged() && !IsEmpty())
        {
            _inventory.ReturnInInventory();
        }

        if (Input.GetMouseButtonDown(0) && _isCooking)
        {
            _inventory.AddItem(_item);
            DeleteItem();
        }
    }

    void Prepare()
    {
        if (_item.ItemName == Item.Name.Meat)
        {
            if (_item.stateOfPreparing == Item.StateOfPreparing.Raw)
            {
                print("Fried");

                _item.stateOfPreparing = Item.StateOfPreparing.Fried;
                _item.UpdateTexture();

                _isCooking = false;
                return;
            }

            else if (_item.stateOfPreparing == Item.StateOfPreparing.Fried)
            {
                print("Burnt");

                _item.stateOfPreparing = Item.StateOfPreparing.Burnt;
                _item.UpdateTexture();

                _isCooking = false;
                return;
            }
        }
    }

    void PreparingTimer()
    {
        if (_isCooking)
        {
            if (_timer > 0)
            {
                _timer-= (Time.deltaTime * _upgrade);
            }

            if (_timer <= 0)
            {
                Prepare();
            }
        }
    }

   private bool IsEmpty()
    {
        if (_item == null)
        {
            return true;
        }

        return false;
    }
}
