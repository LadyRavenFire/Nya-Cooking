using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//tooltip is in the 7 lesson. Maybe delete later....
public class Inventory : MonoBehaviour
{
    public int SlotsX, SlotsY;
    public GUISkin Skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> Slots = new List<Item>();
    //public bool showInventory; // potencial delete
    private ItemDataBase _database;
    //private bool showTooltip;
    //private string tooltip;

    void Start()
    {
        for (int i = 0; i < (SlotsX*SlotsY); i++)
        {
            Slots.Add(new Item());
            inventory.Add(new Item());
        }
        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        AddItem(0);
        AddItem(0);
        RemoveItem(0);
        //print(InventoryContains(0));
    }

    void Update()
    {
       // if (Input.GetButtonDown("Inventory"))//potencial delete
        //{
        //    showInventory = !showInventory;
       // }
    }

    void OnGUI()
    {
       // tooltip = "";
        GUI.skin = Skin;
        //if (showInventory)
        //{
            DrawInventory();
        //}

       // if (showTooltip)
       // {
       //     GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y,50,50), tooltip/*, Skin.GetStyle("Slot")*/);
       // }
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
                   // if (slotRect.Contains(Event.current.mousePosition))
                   //{
                   //     tooltip = CreateTooltip(Slots[index]);
                   //     showTooltip = true;
                   // }
                }

               // if (tooltip == "")
               // {
               //     showTooltip = false;
               // }

                index++;
            }
        }
    }

   // string CreateTooltip(Item item)
   // {
   //     tooltip = item.ItemName;
   //     return tooltip;
   // }

    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemName == null)
            {
                for (int j = 0; j < _database.Items.Count; j++)
                {
                    if (_database.Items[j].ItemID == id)
                    {
                        inventory[i] = _database.Items[j];
                    }
                }
                break;
            }
        }
    }

    bool InventoryContains(int id)
    {
        bool result = false;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[id].ItemID == id)
            {
                result = true;
                break;
            }
        }
        return result;
    }
}
