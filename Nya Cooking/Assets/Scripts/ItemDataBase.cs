using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> Items = new List<Item>(); // list of items in database  

    void Start() //голосом дроздова: а здесь находится база данных в её естественной среде обитания, в ней хранятся все виды предметов, которые можно добавить на сцену
    { 
        Items.Add(new Item("Meat", 0, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false));
        Items.Add(new Item("Meat_fried", 1, Item.StateOfIncision.Whole, Item.StateOfPreparing.fried, false)); //ПЕРЕДЕЛАТЬ
    }

    public Item Generate(Item.Name name, Item.StateOfIncision state1, Item.StateOfPreparing state2, bool isBreaded)
    {
        return new Item(name.ToString("F"), 0, state1, state2, isBreaded);
    }

    public Item Generate(int id)
    {
        if (id == 0)
            return Generate(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);

        return Generate(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
    }
}
