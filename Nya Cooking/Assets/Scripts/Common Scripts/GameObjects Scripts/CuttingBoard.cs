﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuttingBoard : Appliance
{

    void Update()
    {
        if (_itemIsInside)
        {
            CheckMouseUp();
        }

        if (!IsEmpty && _isCooking == false)
        {
            _timer = 2f;         
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
        if (_item.stateOfIncision == Item.StateOfIncision.Whole)
        {
            //print("ForceMeated");

            _item.stateOfIncision = Item.StateOfIncision.Cutted;
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
                _timer -= (Time.deltaTime +  Mathf.Log(_upgrade));
            }

            if (_timer <= 0)
            {
                Prepare();
            }
        }
    }

    void CheckMouseUp()
    {
        if (IsEmpty && Input.GetMouseButtonUp(0) && _inventory.ItemIsDragged())
        {
            AddItem(_inventory.GetDraggedItem());
            _inventory.DeleteDraggedItem();
        }

        if (Input.GetMouseButtonUp(0) && _inventory.ItemIsDragged() && !IsEmpty)
        {
            _inventory.ReturnDraggedItemInInventory();
        }

        if (Input.GetMouseButtonDown(0) && _isCooking)
        {
            _inventory.AddItem(_item);
            DeleteItem();
        }
    }
}
