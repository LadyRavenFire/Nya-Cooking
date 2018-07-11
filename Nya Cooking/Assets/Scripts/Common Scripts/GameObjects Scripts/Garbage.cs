using UnityEngine;

// Скрипт описывающий мусорку

public class Garbage : MonoBehaviour {

    public bool IsEnterCollider;

    void Start()
    {
        IsEnterCollider = false;
    }

    void OnMouseEnter()
    {
        IsEnterCollider = true;
    }

    void OnMouseExit()
    {
        IsEnterCollider = false;
    }
}
