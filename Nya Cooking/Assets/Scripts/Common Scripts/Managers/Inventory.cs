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

    // TODO переделать системы драга
    // Вся логика по IsMouseEnter должна лежать внутри ссответствующего объекта: stove, workbench, garbage, mexicans и т.д.
    // Там проверяется, что предмет вошел в колайдер и меняет _isItemDragged и _draggedItem
    // А их значит, нужно сделать публичными

    private bool _isItemDragged;
    private Item _draggedItem;
    private int _prevIndex;

    private ItemDataBase _database;
    private Stove _stove;
    private Workbench _workbench;
    private Garbage _garbage;

    private readonly List<VisitorsBehaviourEndless> _visitorsBehaviourEndless = new List<VisitorsBehaviourEndless>(); // TODO реализовать работу с множеством посетителей!!!

    void Start()
    {
        _slots = new Item[SlotsX*SlotsY];
        for (int i = 0; i < (SlotsX*SlotsY); i++) 
        {
            _slots[i] = null;
        }

        _database = GameObject.FindGameObjectWithTag("ItemDataBase").GetComponent<ItemDataBase>();
        _stove = GameObject.FindGameObjectWithTag("Stove").GetComponent<Stove>();
        _workbench = GameObject.FindGameObjectWithTag("Workbench").GetComponent<Workbench>();
        _garbage = GameObject.FindGameObjectWithTag("Garbage").GetComponent<Garbage>();

        if (GameObject.Find("mexicans") != null)
        {
            for (int i = 0; i < 3; i++)
            {
                _visitorsBehaviourEndless.Add(GameObject.Find("mexican"+i).GetComponent<VisitorsBehaviourEndless>());//
            }            
        }
        

    }

    void OnGUI()
    {
        GUI.skin = Skin;
        DrawInventory();
        if (_isItemDragged)
        {
            var obj = GameObject.Find("Image0");
            var rectransform = obj.GetComponent<RectTransform>();
            GUI.DrawTexture(
                new Rect(Event.current.mousePosition.x, 
                         Event.current.mousePosition.y, 
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

        if (e.type == EventType.MouseUp && _isItemDragged && _stove.IsEnterCollider)
        {
            if (_stove.IsEmpty)
            {
                _stove.AddItem(_draggedItem);
                _isItemDragged = false;
                _draggedItem = null;
            }
        }

        if (e.type == EventType.MouseUp && _isItemDragged && _workbench.IsEnterCollider)
        {
            if (_workbench.IsPlace())
            {
                _workbench.AddItem(_draggedItem);
                _isItemDragged = false;
                _draggedItem = null;
            }
        }

        if (e.type == EventType.MouseUp && _isItemDragged && _garbage.IsEnterCollider)
        {
                _isItemDragged = false;
                _draggedItem = null;
        }

        if (_visitorsBehaviourEndless != null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (e.type == EventType.MouseUp && _isItemDragged && _visitorsBehaviourEndless[i].IsEnterCollider && _visitorsBehaviourEndless[i].IsClientIn)
                {
                    _visitorsBehaviourEndless[i].AddItem(_draggedItem);
                    _isItemDragged = false;
                    _draggedItem = null;
                }
            }
        }
        
        

        if (e.type == EventType.MouseUp && _isItemDragged)
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
}
