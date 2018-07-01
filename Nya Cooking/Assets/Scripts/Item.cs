using UnityEngine;

[System.Serializable]
public class Item
{
    public Name ItemName; // название
    public Texture2D ItemIcon; // иконка
    public string TexturePath;
    public StateOfPreparing stateOfPreparing; // состояние приготовленности
    public StateOfIncision stateOfIncision; // состояние предварителности??? 
    public bool IsBreaded; // запанированно (???)

    public enum Name
    {
        Meat, // Мясо
        Bread, // Хлеб
        Sandwich, // Бутер
        Ubisoft
    }
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

    public Item(Name name, StateOfIncision incision, StateOfPreparing preparing, bool isBreaded)
    {
        ItemName = name;
        stateOfPreparing = preparing;
        stateOfIncision = incision;       
        IsBreaded = isBreaded;

        TexturePath = "ItemIcons/" + this.ToString();
        ItemIcon = Resources.Load<Texture2D>(TexturePath); //загружаем иконку по названию предмета
    }

    public Item()
    {

    }

    public void UpdateTexture()
    {
        TexturePath = "ItemIcons/" + this.ToString();
        ItemIcon = Resources.Load<Texture2D>(TexturePath); //загружаем иконку по названию предмета
    }

    public sealed override string ToString()
    {
        /*if (stateOfPreparing == StateOfPreparing.Raw)
             return ItemName.ToString("F");*/

        return ItemName + "_" + stateOfPreparing.ToString("F") + "_" + stateOfIncision.ToString("F");
    }

    static public bool operator==(Item item1, Item item2)
    {
        bool item1IsNull = object.ReferenceEquals(item1, null);
        bool item2IsNull = object.ReferenceEquals(item2, null);

        if (item1IsNull && item2IsNull) return true;
        if (item1IsNull) return false;
        if (item2IsNull) return false;

        return (item1.ItemName == item2.ItemName
                && item1.stateOfIncision == item2.stateOfIncision
                && item1.stateOfPreparing == item2.stateOfPreparing
                && item1.IsBreaded == item2.IsBreaded);
    } 

    static public bool operator !=(Item item1, Item item2) 
    {
        bool item1IsNull = object.ReferenceEquals(item1, null);
        bool item2IsNull = object.ReferenceEquals(item2, null);

        if (item1IsNull && item2IsNull) return false;
        if (item1IsNull) return true;
        if (item2IsNull) return true;

        return !(item1.ItemName == item2.ItemName
                && item1.stateOfIncision == item2.stateOfIncision
                && item1.stateOfPreparing == item2.stateOfPreparing
                && item1.IsBreaded == item2.IsBreaded);
    }

    public override bool Equals(object item)
    {
        if (!(item is Item)) return false;

        var item2 = item as Item;

        return (ItemName == item2.ItemName
                && stateOfIncision == item2.stateOfIncision
                && stateOfPreparing == item2.stateOfPreparing
                && IsBreaded == item2.IsBreaded);
    }
}
