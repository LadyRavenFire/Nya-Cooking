using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour {
  
    private readonly List<Item> _itemInWorkbench = new List<Item>();
    public bool IsEnterCollider;
    public bool IsEmpty;
    private Inventory _inventory;
    [SerializeField]
    private int SlotsInWorkbench = 5;

    void Start ()
    {
        for (int i = 0; i < SlotsInWorkbench; i++)
        {
            _itemInWorkbench.Add(null);
        }
        IsEnterCollider = false;
        IsEmpty = true;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void OnMouseEnter()
    {
        IsEnterCollider = true;
    }

    void OnMouseExit()
    {
        IsEnterCollider = false;
    }

    public void AddItem(Item item)
    {
        print("Start call add");
        for (int i = 0; i < _itemInWorkbench.Count; i++)
        {
            if (_itemInWorkbench[i] == null)
            {
                _itemInWorkbench[i] = item;
                print(_itemInWorkbench[i].ItemName);
                if (IsEmpty)
                {
                    IsEmpty = false;
                    print("V pechky sto to polozili");
                }
                break;
            }
        }
    }

    void DeleteItem(int index)
    {
        _itemInWorkbench[index] = null;
        bool flag = false;
        for (int i = 0; i < _itemInWorkbench.Count; i++)
        {
            if (_itemInWorkbench != null)
            {
                flag = true;
                print(i);
            }
        }

        if (!flag)
        {
            IsEmpty = true;
            print("Pechka pysta, milord");
        }

        if (flag)
        {
            print("V pechke stoto ostalos` moi gospodin");            
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewProduct();
        }
    }

    void CreateNewProduct()
    {
        if (!IsEmpty)
        {
            int isBread = SlotsInWorkbench + 1;
            int isMeat = SlotsInWorkbench + 1;
            for (int i = 0; i < _itemInWorkbench.Count; i++)
            {
                if (_itemInWorkbench[i] != null)
                {
                    if (_itemInWorkbench[i].ItemName == Item.Name.Bread)
                    {
                        isBread = i;
                        print("Find Bread");
                        break;
                    }
                }
            }
            for (int i = 0; i < _itemInWorkbench.Count; i++)
            {
                if (_itemInWorkbench[i] != null)
                {
                    if (_itemInWorkbench[i].ItemName == Item.Name.Meat)
                    {
                        isMeat = i;
                        print("Find Meat");
                        break;
                    }
                }
            }

            if (isMeat !=SlotsInWorkbench + 1 && isBread != SlotsInWorkbench + 1)
            {
                print("byter");
                _inventory.AddItem(Item.Name.Sandwich, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
                for (int i = 0; i < _itemInWorkbench.Count; i++)
                {
                    DeleteItem(i);
                }
            }
            else
            {
                print("vsyakofign9");
                _inventory.AddItem(Item.Name.Ubisoft, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
                for (int i = 0; i < _itemInWorkbench.Count; i++)
                {
                    DeleteItem(i);
                }
            }
        }
    }

    public bool IsPlace()
    {
        print("start call isplace");
        for (int i = 0; i < _itemInWorkbench.Count; i++)
        {
            if (_itemInWorkbench[i] == null)
            {
                print("Est` mesto");
                return true;
            }
        }
        return false;
    }
}
