using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    private ItemDataBase _database;

    void Start()
    {
        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        inventory.Add(_database.Items[0]);
        inventory.Add(_database.Items[0]);
        inventory.Add(_database.Items[0]);
    }

    void OnGUI()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            GUI.Label(new Rect(10,i * 40,200,50), inventory[i].ItemIcon);
        }
    }
}
