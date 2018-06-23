using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public List<Item> ItemInStove = new List<Item>();
    public bool IsEnterCollider;
    public bool IsEmpty;
    private bool _isCooking;
    public Inventory inventory;
    private ItemDataBase _database;

    void Start()
    {
        ItemInStove.Add(new Item());
        IsEnterCollider = false;
        IsEmpty = true;
        _isCooking = false;
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
    }

    void Update()
    {
        if (ItemInStove[0].ItemName != null && _isCooking == false)
        {
            ItemInStove[0].stateOfPreparing = Item.StateOfPreparing.Fried;
            if (ItemInStove[0].ItemId == 0) // костыль, переделать 
            {
                ItemInStove[0] = _database.Items[1]; // вот тут жопка сама, если просто поменять текстуру предмета - то она поменяется у всех предметов данного вида!
            }
            _isCooking = true;
            print("EDA V NYTRI!!!");
        }
    }

    void DeleteItem(int id)
    {
        for (int i = 0; i < ItemInStove.Count; i++)
        {
            if (ItemInStove[i].ItemId == id)
            {
                ItemInStove[i] = new Item();
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        ItemInStove[0] = item;
    }

    void OnMouseEnter()
    {
        IsEnterCollider = true;
       // print("In PECHKA!!! YEAAACH!!!");
    }

    void OnMouseExit()
    {
        IsEnterCollider = false;
       // print("Out of PECHKA!!! NOOOOO!!!");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && _isCooking)
        {
            inventory.AddItemFromOther(ItemInStove[0]);
            print("Vz9l item iz pechki!");
            DeleteItem(1);
        }
    }
}
