using UnityEngine;

public class Pan : Appliance
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
        if (_item.ItemName == Item.Name.Meat)
        {
            if (_item.stateOfPreparing == Item.StateOfPreparing.Raw)
            {
                _item.stateOfPreparing = Item.StateOfPreparing.Cooked;
                _item.UpdateTexture();

                _isCooking = false;
                return;
            }

            else if (_item.stateOfPreparing == Item.StateOfPreparing.Cooked)
            {
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