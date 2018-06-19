using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int SlotsX, SlotsY;
    public GUISkin Skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> Slots = new List<Item>();
    public bool showInventory; // potencial delete
    private ItemDataBase _database;

    void Start()
    {
        for (int i = 0; i < (SlotsX*SlotsY); i++)
        {
            Slots.Add(new Item());
            inventory.Add(new Item());
        }
        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        //inventory.Add(_database.Items[0]);
        //inventory.Add(_database.Items[0]);
        //inventory.Add(_database.Items[0]);
        inventory[0] = _database.Items[0]; // add item? >_< need normal function later....
        inventory[1] = _database.Items[0];
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))//potencial delete
        {
            showInventory = !showInventory;
        }
    }

    void OnGUI()
    {
        GUI.skin = Skin;
        if (showInventory)
        {
            DrawInventory();
        }

        /*for (int i = 0; i < inventory.Count; i++)
        {
            GUI.Label(new Rect(10,i * 20,200,50), inventory[i].ItemName);
        }*/
    }

    void DrawInventory()
    {
        int index = 0;
        for (int y = 0; y < SlotsY; y++)
        {
            for (int x = 0; x < SlotsX; x++)
            {
                Rect slotRect = new Rect(x * 60, y * 60, 50, 50);
                GUI.Box(slotRect, "", Skin.GetStyle("Slot"));
                Slots[index] = inventory[index];
                if (Slots[index].ItemName != null)
                {
                    GUI.DrawTexture(slotRect, Slots[index].ItemIcon);
                }
                index++;
            }
        }
    }
}
