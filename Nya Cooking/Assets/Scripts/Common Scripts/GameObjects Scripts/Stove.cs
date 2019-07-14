using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Скрипт описывающий печку

public class Stove : Appliance
{

    void Update()
    {
        if (_itemIsInside)
        {
            CheckMouseUp();
        }

        if (!IsEmpty && !_isCooking)
        {
            _timer = 4;                     
            _isCooking = true;
            PlaceItem();
        }
    }

    void FixedUpdate()
    {
        if (_isCooking && !IsEmpty)
        {
            PreparingTimer();
        }
    }

    void Prepare()
    {
        if (_item.stateOfPreparing == Item.StateOfPreparing.Raw)
        {
            _item.stateOfPreparing = Item.StateOfPreparing.Fried;
            _item.UpdateTexture();

            _isCooking = false;
            return;
        }

        else if (_item.stateOfPreparing == Item.StateOfPreparing.Fried)
        {
            _item.stateOfPreparing = Item.StateOfPreparing.Burnt;
            _item.UpdateTexture();

            _isCooking = false;
            return;
        }
    }

    void PreparingTimer()
    {
        if (_isCooking)
        {
            if (_timer > 0)
            {
                _timer -= (Time.deltaTime + Mathf.Log(_upgrade));
            }

            if (_timer <= 0)
            {
                Prepare();
            }
        }
    }

    void CheckMouseUp()
    {
        if (IsEmpty && Input.GetMouseButtonUp(0) && _inventory.IsDragged())
        {
            AddItem(_inventory.GiveDraggedItem());
            _inventory.DeleteDraggedItem();
        }

        if (Input.GetMouseButtonUp(0) && _inventory.IsDragged() && !IsEmpty)
        {
            _inventory.ReturnInInventory();
        }

        if (Input.GetMouseButtonDown(0) && _isCooking)
        {
            _inventory.AddItem(_item);
            DeleteItem();
        }
    }
}
