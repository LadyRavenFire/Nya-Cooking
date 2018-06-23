using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// comment at 8 lesson - drop outside
// next 9 lesson
public class Inventory : MonoBehaviour
{
    public int SlotsX, SlotsY;
    public GUISkin Skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> Slots = new List<Item>();
    private ItemDataBase _database;
    public Stove stove;

    private bool _draggingItem;
    private Item _draggedItem;
    private int _prevIndex;


    void Start()
    {
        for (int i = 0; i < (SlotsX*SlotsY); i++)
        {
            Slots.Add(new Item());
            inventory.Add(new Item());
        }
        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        stove = GameObject.FindGameObjectWithTag("Stove").GetComponent<Stove>();
        AddItem(0);
        AddItem(0);
        //RemoveItem(0);
    }

    void OnGUI()
    {
        GUI.skin = Skin;
        DrawInventory();
        if (_draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), _draggedItem.ItemIcon);
        }
    }

    void DrawInventory()
    {
        Event e = Event.current;
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
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.button == 0 && e.type == EventType.MouseDrag && !_draggingItem)
                        {
                            _draggingItem = true;
                            _prevIndex = index;
                            _draggedItem = Slots[index];
                            inventory[index] = new Item();
                        }

                        if (e.type == EventType.MouseUp && _draggingItem)
                        {
                            inventory[_prevIndex] = inventory[index];
                            inventory[index] = _draggedItem;
                            _draggingItem = false;
                            _draggedItem = null;
                        }
                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseUp && _draggingItem)
                        {
                            inventory[index] = _draggedItem;
                            _draggingItem = false;
                            _draggedItem = null;
                        }
                    }
                }

                index++;
            }
        }

        if (e.type == EventType.mouseUp && _draggingItem && stove.IsEnterCollider)
        {
            stove.AddItem(_draggedItem);
            _draggingItem = false;
            _draggedItem = null;

        }
        if (e.type == EventType.mouseUp && _draggingItem)
        {
            inventory[_prevIndex] = _draggedItem;
            _draggingItem = false;
            _draggedItem = null;
        }
    }

    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemId == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemName == null)
            {
                for (int j = 0; j < _database.Items.Count; j++)
                {
                    if (_database.Items[j].ItemId == id)
                    {
                        inventory[i] = _database.Items[j];
                    }
                }
                break;
            }
        }
    }

    public void AddItemFromOther(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemName == null)
            {
                for (int j = 0; j < _database.Items.Count; j++)
                {
                    inventory[i] = item;
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
            if (inventory[id].ItemId == id)
            {
                result = true;
                break;
            }
        }
        return result;
    }
}
