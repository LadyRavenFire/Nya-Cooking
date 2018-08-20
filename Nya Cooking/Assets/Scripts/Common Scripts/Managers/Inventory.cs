using System.Collections.Generic;
using UnityEngine;

// Этот скрипт описывает работу инвентаря

// TODO улучшить работу перетаскивания предмета (предмет должен находиться центром прямо под пальцем)

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int SlotsX = 5 , SlotsY = 1; // количество слотов инвентаря в длинну и высоту
    private Item[] _slots;

    public GUISkin Skin; // скин инвентаря (ака текстурка)
    private bool _isItemInOtherGameobject;

    private bool _pauseInventory;

    private bool _isItemDragged;
    private Item _draggedItem;
    private int _prevIndex;

    private ItemDataBase _database;

    void Start()
    {
        _pauseInventory = false;
        _isItemInOtherGameobject = false;
        _slots = new Item[SlotsX*SlotsY];
        for (int i = 0; i < (SlotsX*SlotsY); i++) 
        {
            _slots[i] = null;
        }

        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        
    }

    void OnGUI()
    {
        GUI.skin = Skin;
        if (!_pauseInventory)
        {
            DrawInventory();
        }        
        if (_isItemDragged)
        {
            var obj = GameObject.Find("Image0");
            var rectransform = obj.GetComponent<RectTransform>();
            GUI.DrawTexture(
                new Rect(Event.current.mousePosition.x - rectransform.rect.width/3, 
                         Event.current.mousePosition.y - rectransform.rect.height/3, 
                         rectransform.rect.width - rectransform.rect.width / 10, 
                         rectransform.rect.height - rectransform.rect.height / 10), 
                _draggedItem.ItemIcon);
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
                //пытаюсь оптимизировать под разные разешения, что бы инвентарь подстраивался и всегда был по середине и не мелким и не большим. Пока идет так себе.
                //Rect slotRect = new Rect(((Screen.width / 5) + x * 80), ((Screen.height - (Screen.height / 8)) + (y * 80)), 70, 70); // функция отрисовки ячеек инвентаря               
                //GUI.Box(slotRect, "", Skin.GetStyle("Slot")); // функция отрисовки ячеек инвентаря
                var obj = GameObject.Find("Image" + x);
                var rectransform = obj.GetComponent<RectTransform>();
                Rect obj2 = new Rect(obj.transform.position.x + rectransform.rect.width/20,
                                     Screen.height - obj.transform.position.y + rectransform.rect.height/20,
                                     rectransform.rect.width - rectransform.rect.width/10,
                                     rectransform.rect.height - rectransform.rect.height/10);
                var temp = _slots[index];
                if (temp != null)
                {
                    GUI.DrawTexture(obj2, temp.ItemIcon); // функция отрисовки предметов в инвентаре
                    if (obj2.Contains(e.mousePosition))
                    {
                        if (e.button == 0 && e.type == EventType.MouseDrag && !_isItemDragged) 
                        {
                            _isItemDragged = true;
                            _prevIndex = index;
                            _draggedItem = temp;
                            RemoveItem(index);
                        }

                        if (e.type == EventType.MouseUp && _isItemDragged)
                        {
                            _slots[_prevIndex] = _slots[index];
                            _slots[index] = _draggedItem;
                            _isItemDragged = false;
                            _draggedItem = null;
                        }
                    }
                }
                else
                {
                    if (obj2.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseUp && _isItemDragged)
                        {
                            _slots[index] = _draggedItem;
                            _isItemDragged = false;
                            _draggedItem = null;
                        }
                    }
                }
                index++;
            }
        }

        if (e.type == EventType.MouseUp && _isItemDragged && !_isItemInOtherGameobject)
        {
            _slots[_prevIndex] = _draggedItem;
            _isItemDragged = false;
            _draggedItem = null;
        }
    }

    void RemoveItem(int index)
    {
        _slots[index] = null;
    }

    public void AddItem(Item.Name name, Item.StateOfIncision stateOfIncision, Item.StateOfPreparing stateOfPreparing, bool isBreaded)
    {
        for (int i = 0; i < _slots.Length; i++)
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
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i] == null)
            {
                _slots[i] = item;
                break;
            }
        }
    }

    public bool IsDragged()
    {
        return _isItemDragged;
    }

    public void DeleteDraggedItem()
    {
        _draggedItem = null;
        _isItemDragged = false;        
    }

    public void IsInOther()
    {
        _isItemInOtherGameobject = true;
    }

    public void IsNotInOther()
    {
        _isItemInOtherGameobject = false;
    }

    public Item GiveDraggedItem()
    {
        return _draggedItem;
    }

    public void ReturnInInventory()
    {
        _slots[_prevIndex] = _draggedItem;
        _isItemDragged = false;
        _draggedItem = null;
    }

    public void OffInventory()
    {
        _pauseInventory = true;
    }

    public void OnInventory()
    {
        _pauseInventory = false;
    }

    public int ReturnSlots()
    {
        return SlotsX;
    }

    public Item ReturnItem(int i)
    {
        return _slots[i];
    }

    public Item ReturnDraggedItem()
    {
        return _draggedItem;
    }

    public void Clear()
    {
        for (int i = 0; i < SlotsX; i++)
        {
            _slots[i] = null;
        }
    }

    public void DeleteItem(int i)
    {
        _slots[i] = null;
    }
}
