using System.Collections.Generic;
using UnityEngine;

// Скрипт содержит в себе все рецепты

public class Recipes : MonoBehaviour
{

    private ItemDataBase _db;
    public List<Receipe> RecipesList;

    void Start()
    {
        _db = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();

        Receipe potatoWithMeat = new Receipe()
        {
            Ingridients = new List<Item>()
            {
                new Item(Item.Name.Meat,
                        Item.StateOfIncision.Cutted,
                        Item.StateOfPreparing.Fried),
                new Item(Item.Name.Potato,
                        Item.StateOfIncision.Cutted,
                        Item.StateOfPreparing.Fried)
            },
            Result = _db.Generate(Item.Name.MeatWithPotato, Item.StateOfIncision.Cutted, Item.StateOfPreparing.Fried)

        };

        Receipe sandwich = new Receipe()
        {
            Ingridients = new List<Item>()
            {
                new Item(Item.Name.Meat,
                        Item.StateOfIncision.Forcemeat,
                        Item.StateOfPreparing.Fried),
                new Item(Item.Name.Bread,
                        Item.StateOfIncision.Cutted,
                        Item.StateOfPreparing.Raw),
                new Item(Item.Name.Bread,
                        Item.StateOfIncision.Cutted,
                        Item.StateOfPreparing.Raw)
            },
            Result = _db.Generate(Item.Name.Sandwich, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw)

        };
        Receipe recipe2 = new Receipe()
        {
            Ingridients = new List<Item>()
            {
                new Item(Item.Name.Meat,
                    Item.StateOfIncision.Whole,
                    Item.StateOfPreparing.Raw),
                new Item(Item.Name.Meat,
                    Item.StateOfIncision.Whole,
                    Item.StateOfPreparing.Raw)
            },
            Result = _db.Generate(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Fried)
        };
        RecipesList = new List<Receipe>() { potatoWithMeat, sandwich, recipe2};
    }

    public class Receipe
    {
        public List<Item> Ingridients { get; set; }
        public Item Result { get; set; }
    }
}
