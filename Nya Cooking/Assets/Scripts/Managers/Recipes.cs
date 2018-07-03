using System.Collections.Generic;
using UnityEngine;

public class Recipes : MonoBehaviour
{

    private ItemDataBase _db;
    public List<Receipe> Receipes;

    void Start()
    {
        _db = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        
        Receipe recipe1 = new Receipe()
        {
            Ingridients = new List<Item>()
            {
                new Item
                {
                    ItemName = Item.Name.Meat,
                    stateOfPreparing = Item.StateOfPreparing.Raw,
                    stateOfIncision = Item.StateOfIncision.Whole,
                    IsBreaded = false
                },
                new Item
                {
                    ItemName = Item.Name.Bread,
                    stateOfPreparing = Item.StateOfPreparing.Raw,
                    stateOfIncision = Item.StateOfIncision.Whole,
                    IsBreaded = false
                }
            },
            Result = _db.Generate(Item.Name.Sandwich, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false)

        };
        Receipe recipe2 = new Receipe()
        {
            Ingridients = new List<Item>()
            {
                new Item
                {
                    ItemName = Item.Name.Meat,
                    stateOfPreparing = Item.StateOfPreparing.Raw,
                    stateOfIncision = Item.StateOfIncision.Whole,
                    IsBreaded = false
                },
                new Item
                {
                    ItemName = Item.Name.Meat,
                    stateOfPreparing = Item.StateOfPreparing.Raw,
                    stateOfIncision = Item.StateOfIncision.Whole,
                    IsBreaded = false
                }
            },
            Result = _db.Generate(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Fried, false)
        };
        Receipes = new List<Receipe>() { recipe1, recipe2};
    }

    public class Receipe
    {
        public List<Item> Ingridients { get; set; }
        public Item Result { get; set; }
    }
}
