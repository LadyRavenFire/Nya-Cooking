using UnityEngine;
using UnityEngine.EventSystems;

// Скрипт описывающий мусорку

public class Garbage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Inventory _inventory;
    private bool _checkInFlag;
    void Start()
    {
        _checkInFlag = false;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        if (_checkInFlag)
        {
            CheckMouseUp();
        }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _checkInFlag = true;
        _inventory.IsInOther();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _checkInFlag = false;
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
