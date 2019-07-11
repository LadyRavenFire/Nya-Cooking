using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Catalog: MonoBehaviour
{
    Dictionary<Item.Name, int> itemsCost = new Dictionary<Item.Name, int>()
    {
        {Item.Name.Bread, 40 },
        {Item.Name.Meat, 50 },
        {Item.Name.Potato, 30 }
    };

    Dictionary<Item.Name, float> timeToPrepare = new Dictionary<Item.Name, float>()
    {
        {Item.Name.Bread, 2f },
        {Item.Name.Meat, 5f },
        {Item.Name.Potato, 4f }
    };

    public int GetPriceOf(Item.Name productType)
    {
        if (!itemsCost.ContainsKey(productType))
            throw new System.Exception("Failed to find the price of " + productType.ToString("F"));

        return itemsCost[productType];
    }
}
