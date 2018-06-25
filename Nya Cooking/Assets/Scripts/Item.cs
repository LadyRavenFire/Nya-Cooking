using System;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Name ItemName; // название
    public Texture2D ItemIcon; // иконка
    public string TexturePath;
    public StateOfPreparing stateOfPreparing; // состояние приготовленности
    public StateOfIncision stateOfIncision; // состояние предварителности??? 
    public bool Breading; // запанированно (???)

    public enum Name
    {
        Meat, // Мясо
        Bread // Хлеб
    }
    public enum StateOfPreparing
    {
        Raw, // сырое (стартовое)
        fried, // пожаренное
        Burnt, // пережаренное
        Cooked, // сваренное
        Baked, // запеченное
        Stew // тушеное
    }

    public enum StateOfIncision
    {
        Whole, //целое
        Cutted, //порезанное
        Grated, //тертое
        Beaten, //отбитое
        Forcemeat //фарш
    }

    public Item(Name name, StateOfIncision incision, StateOfPreparing preparing, bool breading)
    {
        ItemName = name;
        TexturePath = "ItemIcons/" + this.ToString();
        ItemIcon = Resources.Load<Texture2D>(TexturePath); //загружаем иконку по названию предмета
        stateOfPreparing = preparing;
        stateOfIncision = incision;       
        Breading = breading;
    }

    public Item()
    {

    }

    public void UpdateTexture()
    {
        TexturePath = "ItemIcons/" + this.ToString();
        ItemIcon = Resources.Load<Texture2D>(TexturePath); //загружаем иконку по названию предмета
    }

    public override string ToString()
    {
        if (stateOfPreparing == StateOfPreparing.Raw)
            return ItemName.ToString("F");

        return ItemName + "_" + stateOfPreparing.ToString("F");
    }
}
