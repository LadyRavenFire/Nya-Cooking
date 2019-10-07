using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestRoom : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _guestsSprites;

    private Visitor[] _guests;
    private int _guestsCount;
    private float _timer;
    private Catalog _catalog;
    private Inventory _inventory;
    private EndlessGameVariables _variables;

    void Start()
    {
        _guests = new Visitor[_guestsSprites.Length];
        _catalog = FindObjectOfType<Catalog>();
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _variables = GameObject.Find("LevelManager").GetComponent<EndlessGameVariables>();
    }

    void Update()
    {
        CheckGuestTimer();
    }

    void CheckGuestTimer()
    {
        if (_guestsCount != _guestsSprites.Length) return;
        if (_timer > 0) _timer -= (Time.deltaTime + Mathf.Log(1f));
        else CreateGuest();
    }

    void CreateGuest()
    {
        _guestsCount++;
        for (int i = 0; i < _guests.Length; i++)
        {
            if (_guests[i] != null) continue;

            var order = _catalog.Orders[RandomCreate.Random.Next(0, _catalog.Orders.Length - 1)];
            _guests[i] = new Visitor(Visitor.Name.Emily, Visitor.Expression.Normal, order);
            _guestsSprites[i] = _guests[i].VisitorSprite; 
        }
    }

    void RecieveOrder(Visitor guest, Item product)
    {
        guest.RecieveOrder(product);
        _inventory.DeleteDraggedItem();
        if (guest.Order == product) _variables.AddMoney(_catalog.GetPriceOf(product.ItemName) * 2);
    }
}
