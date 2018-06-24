using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public Item Generate(Item.Name name, Item.StateOfIncision stateOfIncision, Item.StateOfPreparing stateOfPreparing, bool isBreaded)
    {
        return new Item(name.ToString("F"), stateOfIncision, stateOfPreparing, isBreaded);
    }
}
