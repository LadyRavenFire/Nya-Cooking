using UnityEngine;
using System.Collections;

// Скрипт описывающий мусорку

public class Garbage : MonoBehaviour {

    private Inventory _inventory;
    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
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
        if (Input.GetMouseButtonUp(0) && _inventory.IsDragged())
        {
                _inventory.DeleteDraggedItem();          
        }
    }
    
}
