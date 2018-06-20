using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public List<Item> ItemInStove = new List<Item>();
    public bool IsEnterCollider;
    public bool IsEmpty;

    void Start()
    {
        ItemInStove.Add(new Item());
        IsEnterCollider = false;
        IsEmpty = true;
    }

    void Update()
    {
        if (ItemInStove[0].ItemName != null)
        {
            print("EDA V NYTRI!!!");
        }
    }

    public void AddItem(Item item)
    {
        ItemInStove[0] = item;
    }

    void OnMouseEnter()
    {
        IsEnterCollider = true;
        print("In PECHKA!!! YEAAACH!!!");
    }

    void OnMouseExit()
    {
        IsEnterCollider = false;
        print("Out of PECHKA!!! NOOOOO!!!");
    }
}
