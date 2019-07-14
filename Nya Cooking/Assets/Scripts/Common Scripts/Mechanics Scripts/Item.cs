using UnityEngine;

// Скрипт описывающий все характеристики предметов

[System.Serializable]
public class Item
{
    public readonly Name ItemName; // название
    public Sprite ItemIcon; // иконка
    public string TexturePath;
    public StateOfPreparing stateOfPreparing; // состояние приготовленности
    public StateOfIncision stateOfIncision; // состояние предварителности???

    public enum Name
    {
        Meat, // Мясо
        Bread, // Хлеб
        Potato, // Картошка
        Sandwich, // Бутер
        MeatWithPotato, //всё что связано с мясом + картошкой
        Ubisoft
    }
    public enum StateOfPreparing
    {
        Raw, // сырое (стартовое)
        Fried, // пожаренное
        Burnt, // пережаренное
        Cooked, // сваренное
        Baked, // запеченное
    }

    public enum StateOfIncision
    {
        Whole, //целое
        Cutted, //порезанное
        Grated, //тертое
        Forcemeat //фарш
    }

    public Item(Name name, StateOfIncision incision, StateOfPreparing preparing)
    {
        ItemName = name;
        stateOfPreparing = preparing;
        stateOfIncision = incision;       

        UpdateTexture();
    }

    public void UpdateTexture()
    {
        TexturePath = "ItemIcons/" + this.ToString();
        var sprite = Resources.Load<Sprite>(TexturePath);
        if (sprite == null)
            ItemIcon = Resources.Load<Sprite>("ItemIcons/NotFound");
        else
            ItemIcon = sprite; //загружаем иконку по названию предмета
    }

    public sealed override string ToString()
    {
        return ItemName + "_" + stateOfPreparing.ToString("F") + "_" + stateOfIncision.ToString("F");
    }

    public static bool operator==(Item item1, Item item2)
    {
        bool item1IsNull = object.ReferenceEquals(item1, null);
        bool item2IsNull = object.ReferenceEquals(item2, null);

        if (item1IsNull && item2IsNull) return true;
        if (item1IsNull) return false;
        if (item2IsNull) return false;

        return (item1.ItemName == item2.ItemName
                && item1.stateOfIncision == item2.stateOfIncision
                && item1.stateOfPreparing == item2.stateOfPreparing);
    } 

    public static bool operator !=(Item item1, Item item2) 
    {
        bool item1IsNull = object.ReferenceEquals(item1, null);
        bool item2IsNull = object.ReferenceEquals(item2, null);

        if (item1IsNull && item2IsNull) return false;
        if (item1IsNull) return true;
        if (item2IsNull) return true;

        return !(item1.ItemName == item2.ItemName
                && item1.stateOfIncision == item2.stateOfIncision
                && item1.stateOfPreparing == item2.stateOfPreparing);
    }

    public override bool Equals(object item)
    {
        if (!(item is Item)) return false;

        var item2 = item as Item;

        return (ItemName == item2.ItemName
                && stateOfIncision == item2.stateOfIncision
                && stateOfPreparing == item2.stateOfPreparing);
    }

    public override int GetHashCode()
    {
        return ItemName.ToString("F").GetHashCode();
    }
}
