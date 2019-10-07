using UnityEngine;
using UnityEngine.EventSystems;

// Скрипт описывающий мусорку

public class Garbage : MonoBehaviour
{
    private Inventory _inventory;
    private bool _itemInside;

    void Start()
    {
        _itemInside = false;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        if (_itemInside)
        {
            CheckMouseUp();
        }
    }

    void OnMouseEnter()
    {
        _itemInside = true;
        _inventory.ItemTriggered = true;
    }

    void OnMouseExit()
    {
        _itemInside = false;
        _inventory.ItemTriggered = false;
    }

    void CheckMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && _inventory.ItemIsDragged())
        {
            _inventory.DeleteDraggedItem();
        }
    }

}
