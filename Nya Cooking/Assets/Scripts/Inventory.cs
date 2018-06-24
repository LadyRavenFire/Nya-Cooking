using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// comment at 8 lesson - drop outside
// next 9 lesson
public class Inventory : MonoBehaviour
{
    private int SlotsX, SlotsY; // количество слотов инвентаря в длинну и высоту
    public GUISkin Skin; // скин инвентаря (ака текстурка)
    private List<Item> Slots = new List<Item>(); 
    private ItemDataBase _database;
    public Stove stove;

    private bool _draggingItem;
    private Item _draggedItem;
    private int _prevIndex;

    void Start()
    {
        for (int i = 0; i < (SlotsX*SlotsY); i++) 
        {
            // Slots.Add(new Item());
            Slots.Add(new Item());
        }

        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>(); // тут и строкой ниже ищем по тегу база данных и печка и добавляем объекты в таблицу
        stove = GameObject.FindGameObjectWithTag("Stove").GetComponent<Stove>(); // тут кст могут быть ошибки, если печек будет много, нужно подумать как улучшить
        AddItem(0);
        AddItem(0);

        print(Slots[0].ItemName);
        print(Slots[0].TexturePath);
        print(Slots[0].ItemIcon == null);

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
                Rect slotRect = new Rect(x * 60, y * 60, 50, 50); // функция отрисовки ячеек инвентаря
                GUI.Box(slotRect, "", Skin.GetStyle("Slot")); // функция отрисовки ячеек инвентаря
                var temp = Slots[index];
                if (temp.ItemName != null)
                {
                    GUI.DrawTexture(slotRect, temp.ItemIcon); // функция отрисовки предметов в инвентаре
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.button == 0 && e.type == EventType.MouseDrag && !_draggingItem) 
                        {
                            _draggingItem = true;
                            _prevIndex = index;
                            _draggedItem = temp;
                            RemoveItem(index);
                        }

                        if (e.type == EventType.MouseUp && _draggingItem)
                        {
                            Slots[_prevIndex] = Slots[index];
                            Slots[index] = _draggedItem;
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
                            Slots[index] = _draggedItem;
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
            print("Adding item to stove");
            
            stove.AddItem(_draggedItem);
            stove.IsEmpty = false;
            _draggingItem = false;
            _draggedItem = null;

        }
        if (e.type == EventType.mouseUp && _draggingItem)
        {
            Slots[_prevIndex] = _draggedItem;
            _draggingItem = false;
            _draggedItem = null;
        }
    }

    void RemoveItem(int index)
    {
        Slots[index] = new Item();

        /*
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].ItemId == id)
            {
                slots[i] = new Item();
                break;
            }
        }
        */
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].ItemName == null)
            {
                Slots[i] = _database.Generate(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
                break;
            }
        }

        /*for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].ItemName == null)
            {
                for (int j = 0; j < _database.Items.Count; j++)
                {
                    if (_database.Items[j].ItemId == id)
                    {
                        slots[i] = _database.Items[j];
                    }
                }
                break;
            }
        }*/
    }

    public void AddItemFromOther(Item item)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].ItemName == null)
            {
                // for (int j = 0; j < _database.Items.Count; j++)
                    Slots[i] = item;
                break;
            }
        }
    }

    /*
    bool InventoryContains(int id)
    {
        bool result = false;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[id].ItemId == id)
            {
                result = true;
                break;
            }
        }
        return result;
    }
    */
}
