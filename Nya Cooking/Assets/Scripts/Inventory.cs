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
    private readonly List<Item> _slots = new List<Item>(); 
    private ItemDataBase _database;
    private Stove _stove;
    private Workbench _workbench;

    private bool _draggingItem;
    private Item _draggedItem;
    private int _prevIndex;

    void Start()
    {
        for (int i = 0; i < (SlotsX*SlotsY); i++) 
        {
            _slots.Add(null);
        }

        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        _stove = GameObject.FindGameObjectWithTag("Stove").GetComponent<Stove>();
        _workbench = GameObject.FindGameObjectWithTag("Workbench").GetComponent<Workbench>();

        AddItem(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
        AddItem(Item.Name.Meat, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
        AddItem(Item.Name.Bread, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
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
                var temp = _slots[index];
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
                            _slots[_prevIndex] = _slots[index];
                            _slots[index] = _draggedItem;
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
                            _slots[index] = _draggedItem;
                            _draggingItem = false;
                            _draggedItem = null;
                        }
                    }
                }

                index++;
            }
        }

        if (e.type == EventType.mouseUp && _draggingItem && _stove.IsEnterCollider)
        {
            if (_stove.IsEmpty)
            {
                print("Adding item to stove");
                _stove.AddItem(_draggedItem);
                _draggingItem = false;
                _draggedItem = null;
            }
        }

        if (e.type == EventType.mouseUp && _draggingItem && _workbench.IsEnterCollider)
        {
            if (_workbench.IsPlace())
            {
                print("Adding item to workbench");
                _workbench.AddItem(_draggedItem);
                _draggingItem = false;
                _draggedItem = null;
            }
        }

        if (e.type == EventType.mouseUp && _draggingItem)
        {
            _slots[_prevIndex] = _draggedItem;
            _draggingItem = false;
            _draggedItem = null;
        }
    }

    void RemoveItem(int index)
    {
        _slots[index] = null;
    }

    public void AddItem(Item.Name name, Item.StateOfIncision stateOfIncision, Item.StateOfPreparing stateOfPreparing, bool isBreaded)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i] == null)
            {
                _slots[i] = _database.Generate(name, stateOfIncision, stateOfPreparing, isBreaded);
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i] == null)
            {
                _slots[i] = item;
                break;
            }
        }
    }
}
