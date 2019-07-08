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
        _inventory.IsInOther();
    }

    void OnMouseExit()
    {
        _itemInside = false;
        _inventory.IsNotInOther();
    }

    void CheckMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && _inventory.IsDragged())
        {
            _inventory.DeleteDraggedItem();
        }
    }

}
