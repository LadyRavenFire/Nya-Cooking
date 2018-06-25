using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// comment at 8 lesson - drop outside
// next 9 lesson
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int SlotsX =5 , SlotsY = 1; // количество слотов инвентаря в длинну и высоту
    public GUISkin Skin; // скин инвентаря (ака текстурка)
    private List<Item> Slots = new List<Item>(); 
    private ItemDataBase _database;
    public Stove Stove;

    private bool _draggingItem;
    private Item _draggedItem;
    private int _prevIndex;

    void Start()
    {
        for (int i = 0; i < (SlotsX*SlotsY); i++) 
        {
            Slots.Add(null);
        }

        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>(); // тут и строкой ниже ищем по тегу база данных и печка и добавляем объекты в таблицу
        Stove = GameObject.FindGameObjectWithTag("Stove").GetComponent<Stove>(); // тут кст могут быть ошибки, если печек будет много, нужно подумать как улучшить
        AddItem(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
        AddItem(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
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
                if (temp != null)
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

        if (e.type == EventType.mouseUp && _draggingItem && Stove.IsEnterCollider)
        {
            print("Adding item to stove");
            
            Stove.AddItem(_draggedItem);
            Stove.IsEmpty = false;
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
        Slots[index] = null;
    }

    public void AddItem(Item.Name name, Item.StateOfIncision stateOfIncision, Item.StateOfPreparing stateOfPreparing, bool isBreaded)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i] == null)
            {
                Slots[i] = _database.Generate(name, stateOfIncision, stateOfPreparing, isBreaded);
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i] == null)
            {
                Slots[i] = item;
                break;
            }
        }
    }
}
