using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pan : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Item _item;
    private Inventory _inventory;
    private bool _isCooking;
    private float _timer;
    private float _upgrade = 1f;
    private Image _sprite;

    private bool _checkInFlag;

    private Dictionary<Item.Name, Dictionary<Item.StateOfPreparing, Dictionary<Item.StateOfIncision, float>>> _productTimers = new Dictionary<Item.Name, Dictionary<Item.StateOfPreparing, Dictionary<Item.StateOfIncision, float>>>
    {
        {
            Item.Name.Meat, new Dictionary<Item.StateOfPreparing, Dictionary<Item.StateOfIncision, float>>
            {
                {
                    Item.StateOfPreparing.Raw, new Dictionary<Item.StateOfIncision, float>
                    {
                        {Item.StateOfIncision.Whole, 666},
                        {Item.StateOfIncision.Cutted, 5},
                        {Item.StateOfIncision.Forcemeat, 666 },
                        {Item.StateOfIncision.Grated, 666}
                    }
                },

                {
                    Item.StateOfPreparing.Fried, new Dictionary<Item.StateOfIncision, float>
                    {
                        {Item.StateOfIncision.Whole, 666},
                        {Item.StateOfIncision.Cutted, 666},
                        {Item.StateOfIncision.Forcemeat, 666},
                        {Item.StateOfIncision.Grated, 666}
                    }
                },

                {
                    Item.StateOfPreparing.Burnt, new Dictionary<Item.StateOfIncision, float>
                    {
                        {Item.StateOfIncision.Whole, 666},
                        {Item.StateOfIncision.Cutted, 666},
                        {Item.StateOfIncision.Forcemeat, 666},
                        {Item.StateOfIncision.Grated, 666}
                    }
                },

                {
                    Item.StateOfPreparing.Cooked, new Dictionary<Item.StateOfIncision, float>
                    {
                        {Item.StateOfIncision.Whole, 666},
                        {Item.StateOfIncision.Cutted, 666},
                        {Item.StateOfIncision.Forcemeat, 666},
                        {Item.StateOfIncision.Grated, 666}
                    }
                }
            }
        }
    };

    void Start()
    {
        _checkInFlag = false;
        _item = null;
        _isCooking = false;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        //print(_upgrade);
        _sprite = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (_checkInFlag)
        {
            CheckMouseUp();
        }

        if (!IsEmpty() && _isCooking == false)
        {
            if (_item.ItemName == Item.Name.Meat)
            {
                _timer = _productTimers[_item.ItemName][_item.stateOfPreparing][_item.stateOfIncision];
                _sprite.color = Color.yellow; // delete            
            }
            _isCooking = true;
        }
    }

    public void Upgrade(float level)
    {
        _upgrade = level;
    }

    void FixedUpdate()
    {
        if (_isCooking && !IsEmpty())
        {
            PreparingTimer();
        }
    }

    public void DeleteItem()
    {
        _item = null;
        _isCooking = false;

       // _sprite.sprite = Resources.Load<Sprite>("Kitchenware/CuttingBoard_empty");
        _sprite.color = Color.white;

    }

    public void AddItem(Item item)
    {
        _item = item;
    }

    void Prepare()
    {
        if (_item.ItemName == Item.Name.Meat)
        {
            if (_item.stateOfIncision == Item.StateOfIncision.Cutted && _item.stateOfPreparing == Item.StateOfPreparing.Raw)
            {
                //print("ForceMeated");

                _item.stateOfPreparing = Item.StateOfPreparing.Cooked;
                _item.UpdateTexture();

                //_sprite.sprite = Resources.Load<Sprite>("Kitchenware/CuttingBoard_with_Meat_Raw_Cutted"); //need to delete later
                _sprite.color = Color.white;
                _isCooking = false;
                return;
            }
        }
    }

    void PreparingTimer()
    {
        if (_isCooking)
        {
            if (_timer > 0)
            {
                _timer -= (Time.deltaTime + Mathf.Log(_upgrade));
            }

            if (_timer <= 0)
            {
                Prepare();
            }
        }
    }

    private bool IsEmpty()
    {
        if (_item == null)
        {
            return true;
        }

        return false;
    }

    public Item ReturnItem()
    {
        return _item;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _checkInFlag = true;
        _inventory.IsInOther();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _checkInFlag = false;
        _inventory.IsNotInOther();
    }


    void CheckMouseUp()
    {
        if (IsEmpty() && Input.GetMouseButtonUp(0) && _inventory.IsDragged())
        {
            AddItem(_inventory.GiveDraggedItem());
            _inventory.DeleteDraggedItem();
        }

        if (Input.GetMouseButtonUp(0) && _inventory.IsDragged() && !IsEmpty())
        {
            _inventory.ReturnInInventory();
        }

        if (Input.GetMouseButtonDown(0) && _isCooking)
        {
            _inventory.AddItem(_item);
            DeleteItem();
        }
    }
}
