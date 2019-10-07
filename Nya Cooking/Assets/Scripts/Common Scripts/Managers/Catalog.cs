using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Catalog: MonoBehaviour
{
    Dictionary<Item.Name, int> _itemsCost = new Dictionary<Item.Name, int>()
    {
        {Item.Name.Bread, 40 },
        {Item.Name.Meat, 50 },
        {Item.Name.Potato, 30 },
        {Item.Name.Tomato, 20 }
    };

    Dictionary<Item.Name, float> _timeToPrepare = new Dictionary<Item.Name, float>()
    {
        {Item.Name.Bread, 2f },
        {Item.Name.Meat, 5f },
        {Item.Name.Potato, 4f },
        {Item.Name.Tomato, 2f }
    };

    Dictionary<Item.Name, Repository> _repositories = new Dictionary<Item.Name, Repository>();

    Dictionary<Item, int> _ordersCost = new Dictionary<Item, int>();


    private Item[] _orders;

    public Item[] Orders {  get { return _orders; } }

    private void Start()
    {
        FillRepositoriesDictionary();
        CreateOrdersArray();
        //FillOrdersCostDictionary();
    }

    public int GetPriceOf(Item.Name productType)
    {
        if (!_itemsCost.ContainsKey(productType))
            throw new System.Exception("Failed to find the price of " + productType.ToString("F")); // потом убрать

        return _itemsCost[productType];
    }

    public float GetPrepareTimeOf(Item.Name productType)
    {
        if (!_timeToPrepare.ContainsKey(productType))
            throw new System.Exception("Failed to find the prepare time of " + productType.ToString("F")); // потом убрать

        return _timeToPrepare[productType];
    }

    public Repository GetRepository(Item.Name productType)
    {
        if (!_repositories.ContainsKey(productType))
            throw new System.Exception("Failed to find repository of type " + productType.ToString("F")); // потом убрать

        return _repositories[productType];
    }

    private void FillRepositoriesDictionary()
    {
        var repositories = GameObject.FindGameObjectsWithTag("Repository");
        foreach (var repository in repositories)
        {
            var current = repository.GetComponent<Repository>();
            _repositories[current.StoredItemType] = current;
        }
    }

    private void CreateOrdersArray()
    {
        _orders = new Item[]
        {
            new Item(Item.Name.Sandwich, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw),
            new Item(Item.Name.MeatWithPotato, Item.StateOfIncision.Cutted, Item.StateOfPreparing.Fried),
            new Item(Item.Name.Tomato, Item.StateOfIncision.Whole, Item.StateOfPreparing.Fried),
            new Item(Item.Name.Tomato, Item.StateOfIncision.Forcemeat, Item.StateOfPreparing.Cooked)
        };
    }

}
