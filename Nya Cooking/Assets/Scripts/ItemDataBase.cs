using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> Items = new List<Item>(); // list of items in database  

    void Start()
    {
        Items.Add(new Item("Meat", 0, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false));
        Items.Add(new Item("Meat_fried", 1, Item.StateOfIncision.Whole, Item.StateOfPreparing.Fried, false)); //ПЕРЕДЕЛАТЬ
    }
}
