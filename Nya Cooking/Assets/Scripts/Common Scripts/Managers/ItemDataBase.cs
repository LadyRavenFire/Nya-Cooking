using UnityEngine;

// Генератор предметов

public class ItemDataBase : MonoBehaviour
{
    public Item Generate(Item.Name itemName, Item.StateOfIncision stateOfIncision, Item.StateOfPreparing stateOfPreparing, bool isBreaded)
    {
        return new Item(itemName, stateOfIncision, stateOfPreparing, isBreaded);
    }
}
