using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour {

    public List<Item> ItemInWorkbench = new List<Item>();
    public bool IsEnterCollider;
    public bool IsEmpty;
    public Inventory Inventory;
    public int SlotsInWorkbench;

    void Start ()
    {
        for (int i = 0; i < SlotsInWorkbench; i++)
        {
            ItemInWorkbench.Add(null);
        }
        IsEnterCollider = false;
        IsEmpty = true;
        Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
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
        for (int i = 0; i < ItemInWorkbench.Count; i++)
        {
            if (ItemInWorkbench[i] == null)
            {
                ItemInWorkbench[i] = item;
                print(ItemInWorkbench[i].ItemName);
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
        ItemInWorkbench[index] = null;
        bool flag = false;
        for (int i = 0; i < ItemInWorkbench.Count; i++)
        {
            if (ItemInWorkbench != null)
            {
                flag = true;
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
            int IsBread = SlotsInWorkbench + 1;
            int IsMeat = SlotsInWorkbench + 1;
            for (int i = 0; i < ItemInWorkbench.Count; i++)
            {
                if (ItemInWorkbench[i] != null)
                {
                    if (ItemInWorkbench[i].ItemName == Item.Name.Bread)
                    {
                        IsBread = i;
                        print("Find Bread");
                        break;
                    }
                }
            }
            for (int i = 0; i < ItemInWorkbench.Count; i++)
            {
                if (ItemInWorkbench[i] != null)
                {
                    if (ItemInWorkbench[i].ItemName == Item.Name.Meat)
                    {
                        IsMeat = i;
                        print("Find Meat");
                        break;
                    }
                }
            }

            if (IsMeat !=SlotsInWorkbench + 1 && IsBread != SlotsInWorkbench + 1)
            {
                print("Lol");
                Inventory.AddItem(Item.Name.Sandwich, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
                DeleteItem(IsMeat);
                DeleteItem(IsBread);
            }
            else
            {
                print("Lol");
                Inventory.AddItem(Item.Name.Ubisoft, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
                DeleteItem(IsMeat);
                DeleteItem(IsBread);
            }

        }
    }

    public bool IsPlace()
    {
        print("start call isplace");
        for (int i = 0; i < ItemInWorkbench.Count; i++)
        {
            if (ItemInWorkbench[i] == null)
            {
                print("Est` mesto");
                return true;
            }
        }
        return false;
    }
}
