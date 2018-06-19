using UnityEngine;

[System.Serializable]
public class Item
{
    public string ItemName; // название
    public int ItemID; // id
    public Texture2D ItemIcon; // иконка
    public StateOfPreparing stateOfPreparing; // состояние приготовленности
    public StateOfIncision stateOfIncision; // состояние предварителности??? 
    public bool Breading; // запанированно (???)

    public enum StateOfPreparing
    {
        Raw, // сырое (стартовое)
        Fried, // пожаренное
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

    public Item(string name, int id, StateOfIncision incision, StateOfPreparing preparing, bool breading)
    {
        ItemName = name;
        ItemID = id;
        ItemIcon = Resources.Load<Texture2D>("ItemIcons/" + name); //загружаем иконку по названию предмета
        stateOfPreparing = preparing;
        stateOfIncision = incision;       
        Breading = breading;

    }

    public Item()
    {

    }
}
