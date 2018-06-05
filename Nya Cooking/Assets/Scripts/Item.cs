using UnityEngine;

[System.Serializable]
public class Item
{

    public string ItemName;
    public int ItemID;
    public Texture2D ItemIcon;
    public StateOfRare stateOfRare;
    public StateOfIncision stateOfIncision;

    public enum StateOfRare
    {
        Raw,
        Fried,
        Burnt
    }

    public enum StateOfIncision
    {
        Whole,
        Cutted,
        Diced,
        Stripped

    }

}
