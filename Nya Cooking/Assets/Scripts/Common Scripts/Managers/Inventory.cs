using System.Collections.Generic;
using UnityEngine;

// Этот скрипт описывает работу инвентаря

// TODO улучшить работу перетаскивания предмета (предмет должен находиться центром прямо под пальцем)

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int SlotsX = 8 , SlotsY = 1; // количество слотов инвентаря в длинну и высоту
    private float slotSize;
    public GUISkin Skin; // скин инвентаря (ака текстурка)

    private Item[] _items;
    private Item _draggedItem;

    public bool ItemTriggered;
    
    private bool _onPause;

    void Start()
    {
        _onPause = false;
        _items = new Item[SlotsX*SlotsY];
        for (int i = 0; i < (SlotsX*SlotsY); i++) 
        {
            _items[i] = null;
        }
        
    }

    void OnGUI()
    {
        if (!_onPause) DrawInventorySlots();
    }

    void DrawInventorySlots()
    {
        GUI.skin = Skin;
        Event e = Event.current;
        slotSize = Screen.width * 0.8f / SlotsX - 10;

        for (int x = 0; x < SlotsX; x++)
        {
            var slotRect = new Rect(Screen.width * 0.1f + x * (slotSize + 10) + 5, Screen.height - slotSize, slotSize, slotSize);

            //Проверяем нажатие на слот инвентаря
            if (slotRect.Contains(e.mousePosition) && e.type == EventType.MouseDown)
            {
                //выбираем предмет при нажатии на него
                _draggedItem = _items[x];
                _items[x] = null;
            }
            if (_draggedItem != null && e.type == EventType.MouseUp && !ItemTriggered) ReturnDraggedItemInInventory();

            //отрисовка слота
            GUI.DrawTexture(slotRect, Resources.Load<Texture>("UI/cell"), ScaleMode.StretchToFill);

            //отрисовка перетаскиваемого предмета
            if (_draggedItem != null)
                GUI.DrawTexture(new Rect(e.mousePosition.x - slotSize / 2, e.mousePosition.y - slotSize / 2, slotSize, slotSize),
                    _draggedItem.ItemIcon.texture, ScaleMode.StretchToFill);

            //отрисовка предмета в слоте
            if (_items[x] != null)
                    GUI.DrawTexture(slotRect, _items[x].ItemIcon.texture, ScaleMode.StretchToFill);
        }
    }

    

    public void AddItem(Item.Name name, Item.StateOfIncision stateOfIncision, Item.StateOfPreparing stateOfPreparing)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = new Item(name, stateOfIncision, stateOfPreparing);
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < SlotsX; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                break;
            }
        }
        
    }

    public bool ItemIsDragged()
    {
        return _draggedItem != null;
    }

    public void DeleteDraggedItem()
    {
        _draggedItem = null;
        //_draggedItem = null;    
    }

    public Item GetDraggedItem()
    {
        return _draggedItem;
    }

    public void ReturnDraggedItemInInventory()
    {
        AddItem(_draggedItem);
        _draggedItem = null;
    }

    public void OffInventory()
    {
        _onPause = true;
    }

    public void OnInventory()
    {
        _onPause = false;
    }

    public int ReturnSlots()
    {
        return SlotsX;
    }

    public Item ReturnItem(int i)
    {
        return _items[i];
    }

    public void Clear()
    {
        for (int i = 0; i < SlotsX; i++)
        {
            _items[i] = null;
        }
    }

    public void DeleteItem(int i)
    {
        _items[i] = null;
    }
}
