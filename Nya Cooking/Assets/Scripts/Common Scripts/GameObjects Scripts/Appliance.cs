using UnityEngine;

public class Appliance: MonoBehaviour
{
    [SerializeField]
    protected GameObject _itemSprite;

    protected Item _item;
    protected bool _itemIsInside;
    protected Inventory _inventory;
    protected bool _isCooking;
    protected float _timer;
    protected float _upgrade;

    public bool IsEmpty { get { return _item == null; } }

    void Start()
    {
        _itemSprite.SetActive(false);
        _item = null;
        _itemIsInside = false;
        _isCooking = false;
        _upgrade = 1f;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }


    private void OnMouseEnter()
    {
        _itemIsInside = true;
        _inventory.ItemTriggered = true;
    }

    void OnMouseExit()
    {
        _itemIsInside = false;
        _inventory.ItemTriggered = false;
    }

    public void AddItem(Item item)
    {
        _item = item;
        _itemSprite.SetActive(true);
    }

    public void DeleteItem()
    {
        _item = null;
        _isCooking = false;
        _itemSprite.SetActive(false);
    }

    public Item ReturnItem()
    {
        return _item;
    }

    public void Upgrade(float level)
    {
        _upgrade = level;
    }

    public void PlaceItem()
    {
        var spriteRenderer = _itemSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _item.ItemIcon;
        //spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
    }
}
