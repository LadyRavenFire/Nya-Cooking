﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MeatGrinder : Appliance
{
   void Update()
    {
        if (_itemIsInside)
        {
            CheckMouseUp();
        }

        if (!IsEmpty && _isCooking == false)
        {
            _timer = 3;
            _isCooking = true;
            PlaceItem();
        }
    }

    void FixedUpdate()
    {
        if (_isCooking && !IsEmpty)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
            PreparingTimer();
        }
    }

    void Prepare()
    {
        if (_item.stateOfIncision == Item.StateOfIncision.Whole)
        {
            print("ForceMeated");
                

            _item.stateOfIncision = Item.StateOfIncision.Forcemeat;
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
