using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public Item Generate(Item.Name itemName, Item.StateOfIncision stateOfIncision, Item.StateOfPreparing stateOfPreparing, bool isBreaded)
    {
        return new Item(itemName, stateOfIncision, stateOfPreparing, isBreaded);
    }
}
