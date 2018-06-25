using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Workbench : MonoBehaviour {
  
    private readonly List<Item> _itemInWorkbench = new List<Item>();
    public bool IsEnterCollider;
    public bool IsEmpty;
    private Inventory _inventory;
    [SerializeField]
    private int SlotsInWorkbench = 5;

    void Start()
    {
        for (int i = 0; i < SlotsInWorkbench; i++)
        {
            _itemInWorkbench.Add(null);
        }
        IsEnterCollider = false;
        IsEmpty = true;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
    }

    void OnMouseEnter()
    {
        IsEnterCollider = true;
    }

    void OnMouseExit()
    {
        IsEnterCollider = false;
    }

    public void AddItem(Item item)
    {
        print("Start call add");
        for (int i = 0; i < _itemInWorkbench.Count; i++)
        {
            if (_itemInWorkbench[i] == null)
            {
                _itemInWorkbench[i] = item;
                print(_itemInWorkbench[i].ItemName);
                if (IsEmpty)
                {
                    IsEmpty = false;
                    print("V pechky sto to polozili");
                }
                break;
            }
        }
    }

    void DeleteItem(int index)
    {
        _itemInWorkbench[index] = null;
        bool flag = false;
        for (int i = 0; i < _itemInWorkbench.Count; i++)
        {
            if (_itemInWorkbench != null)
            {
                flag = true;
//                print(i);
            }
        }

        if (flag)
        {
            IsEmpty = true;
            print("Pechka pysta, milord");
        }
        else
        {
            print("V pechke stoto ostalos` moi gospodin");            
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewProduct();
        }
    }

    class IngridientFound
    {
        public Item Item;
        public bool IsFound;

        public bool Check(Item item2)
        {
            if (this.Item == null && item2 == null) return true;
            if (this.Item == null && item2 != null) return false;
            if (this.Item != null && item2 == null) return false;

            if (this.Item.ItemName == item2.ItemName
                && this.Item.stateOfIncision == item2.stateOfIncision
                && this.Item.stateOfPreparing == item2.stateOfPreparing
                && this.Item.Breading == item2.Breading)
                return true;

            return false;
        }
    }

    void CreateNewProduct()
    {
        if (!IsEmpty)
        {
            List<Item> receipe = new List<Item>()
            {
                new Item
                {
                    ItemName = Item.Name.Meat,
                    stateOfPreparing = Item.StateOfPreparing.Raw,
                    stateOfIncision = Item.StateOfIncision.Whole,
                    Breading = false
                },
                new Item
                {
                    ItemName = Item.Name.Bread,
                    stateOfPreparing = Item.StateOfPreparing.Raw,
                    stateOfIncision = Item.StateOfIncision.Whole,
                    Breading = false
                }
            };

            List<IngridientFound> receipeFound = receipe.Select(x => new IngridientFound
            {
                Item = x,
                IsFound = false
            }).ToList();

            var items = new List<Item>(_itemInWorkbench.Where(x => x != null).ToList());
            foreach (var rec in receipeFound)
            {
                var toDelete = new List<Item>();
                foreach (var item in items)
                {
                    if (rec != null && (rec.Check(item) && !rec.IsFound))
                    {
                        if (item != null)
                        {
                            print("Find " + item.ItemName.ToString("F"));
                            rec.IsFound = true;
                            toDelete.Add(item);
                        }
                    }
                }
                print("toDelete = " + toDelete.Count);
                foreach (var item in toDelete)
                {
                    items.Remove(item);
                }
                print("items = " + items.Count);
            }

            print(items.Any() + " " + receipeFound.Any(x => !x.IsFound));
            print(items.Count);

            if (items.Any() || receipeFound.Any(x => !x.IsFound))
            {
                print("vsyakofign9");
                _inventory.AddItem(Item.Name.Ubisoft, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
            }
            else
            {
                print("byter");
                _inventory.AddItem(Item.Name.Sandwich, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
            }

            for (int i = 0; i < _itemInWorkbench.Count; i++)
            {
                if(_itemInWorkbench[i] != null)
                    DeleteItem(i);
            }


            //            int isBread = SlotsInWorkbench + 1;
            //            int isMeat = SlotsInWorkbench + 1;
            //            for (int i = 0; i < _itemInWorkbench.Count; i++)
            //            {
            //                if (_itemInWorkbench[i] != null)
            //                {
            //                    if (_itemInWorkbench[i].ItemName == Item.Name.Bread)
            //                    {
            //                        isBread = i;
            //                        print("Find Bread");
            //                        break;
            //                    }
            //                }
            //            }
            //            for (int i = 0; i < _itemInWorkbench.Count; i++)
            //            {
            //                if (_itemInWorkbench[i] != null)
            //                {
            //                    if (_itemInWorkbench[i].ItemName == Item.Name.Meat)
            //                    {
            //                        isMeat = i;
            //                        print("Find Meat");
            //                        break;
            //                    }
            //                }
            //            }
            //
            //            if (isMeat !=SlotsInWorkbench + 1 && isBread != SlotsInWorkbench + 1)
            //            {
            //                print("byter");
            //                _inventory.AddItem(Item.Name.Sandwich, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
            //                for (int i = 0; i < _itemInWorkbench.Count; i++)
            //                {
            //                    DeleteItem(i);
            //                }
            //            }
            //            else
            //            {
            //                print("vsyakofign9");
            //                _inventory.AddItem(Item.Name.Ubisoft, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
            //                for (int i = 0; i < _itemInWorkbench.Count; i++)
            //                {
            //                    DeleteItem(i);
            //                }
            //            }
        }
    }

    public bool IsPlace()
    {
        print("start call isplace");
        for (int i = 0; i < _itemInWorkbench.Count; i++)
        {
            if (_itemInWorkbench[i] == null)
            {
                print("Est` mesto");
                return true;
            }
        }
        return false;
    }
}
