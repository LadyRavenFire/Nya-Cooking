using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public List<Item> ItemInStove = new List<Item>();
    public bool IsEnterCollider;
    public bool IsEmpty;
    private bool _isCooking;
    public Inventory Inventory;
    private float _timer;
    private bool _timerFlag;

    void Start()
    {
        print("Stove created");

        ItemInStove.Add(null);
        IsEnterCollider = false;
        IsEmpty = true;
        _isCooking = false;
        Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _timer = 0;
        _timerFlag = false;
    }

    void Update()
    {
        if (!IsEmpty && _isCooking == false)
        {
            if (ItemInStove[0].ItemName == Item.Name.Meat && _timerFlag == false)
            {
                _timer = 5;
                _timerFlag = true;
            }

            _isCooking = true;            
            print("EDA V NYTRI!!!");
        }
        PreparingTimer();
    }

    void DeleteItem(int index)
    {
        ItemInStove[index] = null;

        IsEmpty = true;
        _isCooking = false;
    }

    public void AddItem(Item item)
    {
        ItemInStove[0] = item;
    }

    void OnMouseEnter()
    {
        IsEnterCollider = true;
       // print("In PECHKA!!! YEAAACH!!!");
    }

    void OnMouseExit()
    {
        IsEnterCollider = false;
       // print("Out of PECHKA!!! NOOOOO!!!");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && _isCooking)
        {
            Inventory.AddItem(ItemInStove[0]);
            print("Vz9l item iz pechki!");
            DeleteItem(0);
            //_isCooking = false;
        }
    }

    void Prepare()
    {
        if (ItemInStove[0].ItemName == Item.Name.Meat)
        {
            ItemInStove[0].stateOfPreparing = Item.StateOfPreparing.Fried;
            ItemInStove[0].UpdateTexture();
            // ItemInStove[0] = _database.Items[1];           
            print("Eda prigotovilas`");
        }
        _timerFlag = false;
        _timer = 0;
    }

    void PreparingTimer()
    {
        if (_isCooking && _timerFlag)
        {
            if (_timer > 0)
            {
                _timer-= Time.deltaTime;
            }

            if (_timer < 0)
            {
                Prepare();
            }
        }
    }
}
