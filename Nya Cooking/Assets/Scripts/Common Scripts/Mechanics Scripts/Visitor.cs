using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visitor
{
    public readonly Name VisitorName;
    public Sprite VisitorSprite;
    public string TexturePath;
    public Expression VisitorExpression;
    public Item Order;

    public enum Name
    {
        Emily
    }
    public enum Expression
    {
        Normal,
        Sad,
        Happy,
        Talk
    }

    public Visitor(Name name, Expression expression, Item order)
    {
        VisitorName = name;
        VisitorExpression = expression;
        Order = order;

        UpdateTexture();
    }

    public void UpdateTexture()
    {
        TexturePath = "Avatars/" + this.ToString();
        var sprite = Resources.Load<Sprite>(TexturePath);
        VisitorSprite = sprite; //загружаем иконку по названию предмета
    }

    public void RecieveOrder(Item product)
    {
        if (Order == product) VisitorExpression = Expression.Happy;
        else VisitorExpression = Expression.Sad;
    }

    public sealed override string ToString()
    {
        return VisitorName + "_" + VisitorExpression.ToString("F");
    }

    public override int GetHashCode()
    {
        return VisitorName.ToString("F").GetHashCode();
    }
}
