using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Workbench : MonoBehaviour {

    [SerializeField]
    private int _slotsCount = 5;
    private Item[] _items;

    public bool IsEnterCollider;
    public bool IsEmpty;

    private Inventory _inventory;

    void Start()
    {
        _items = new Item[_slotsCount];
        for (int i = 0; i < _slotsCount; i++)
        {
            _items[i] = null;
        }
        IsEnterCollider = false;
        IsEmpty = true;
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
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
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                if (IsEmpty)
                {
                    IsEmpty = false;
                }
                break;
            }
        }
    }

    void DeleteItem(int index)
    {
        _items[index] = null;
        bool flag = false;
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items != null)
            {
                flag = true;
            }
        }

        if (flag)
        {
            IsEmpty = true;
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
    }
    class ReceipeFound
    {
        public List<IngridientFound> IngridientFounds { get; set; }
        public Item Result { get; set; }
        public bool IsFound { get; set; }
    }

    void CreateNewProduct()
    {
        if (!IsEmpty)
        {
            var receipe = GameObject.FindGameObjectWithTag("Recipes").GetComponent<Recipes>();
            var recipes = receipe.Receipes;     

            // Создаем структуру рецепт - был ли рецепт найден
            List<ReceipeFound> receipeFounds = recipes.Select(x => new ReceipeFound
            {
                // у каждого рецепта есть список ингридиентов - и был ли этот ингридиент найден
                IngridientFounds = x.Ingridients.Select(y => 
                    new IngridientFound
                    {
                        // сохраняем ингридиент
                        Item = y,
                        // ингридиент еще не был найден
                        IsFound = false
                    }).ToList(),
                // рецепт еще не был найден
                IsFound = false,
                // какое блюдо будет в результате рецепта
                Result = x.Result
            }).ToList();

            // для каждого рецепта в списке рецептов...
            foreach (var receipeFound in receipeFounds)
            {
                // сохраняем элементы из воркбенча в отдельный список для удобной работы
                // в дальнейшем именно этот список мы будем называть элементами воркбенча
                // в каждой отдельной итерации
                var items = new List<Item>(_items.Where(x => x != null).ToList());

                // для каждого ингридиента в рецепте...
                foreach (var rec in receipeFound.IngridientFounds)
                {
                    // создаем список найденных ингридиентов для удаления из общего списка жлементов,
                    // чтобы не мешались при проверках
                    var toDelete = new List<Item>();
                    // для каждого элемента на воркбенче...
                    foreach (var item in items)
                    {
                        // проверяем совпадает ли ингридиент из рецепта с элементом на воркбенче
                        if (rec.Item == item && !rec.IsFound)
                        {
                            // если совпал...

                            // помечаем ингридиент из рецепта, как найденный
                            rec.IsFound = true;
                            // помечаем, что нужно удалить этот элемент с воркбенча, потому что
                            // мы его уже нашли и не нужно его дальше учитывать
                            toDelete.Add(item);
                        }
                    }

                    // удаляем все найденные элементы из списка воркбенча
                    foreach (var item in toDelete)
                    {
                        items.Remove(item);
                    }
                }

                // Если остались неиспользованные элементы на воркбенче или
                // есть хотя бы один не найденный ингридиент в рецепте,
                // то - юбисофт вторгается в наш мир
                if (items.Any() || receipeFound.IngridientFounds.Any(x => !x.IsFound))
                {
                    print("vsyakofign9 " + receipeFound.Result.ItemName.ToString("F"));
                }
                else
                {
                    print("found " + receipeFound.Result.ItemName.ToString("F"));

                    // иначе помечаем рецепт как найденный
                    receipeFound.IsFound = true;
                }
            }

            // После того, как мы пробежались по всем рецептам и нашли или нет подходящие, проверяем...

            // если кол-во ненайденных рецепт == кол-ву рецептов в целом, то...
            if (receipeFounds.Count(x => !x.IsFound) == receipeFounds.Count)
            {
                print("no correct receipes!");

                // значит, нет ни одного найденного рецепта и... 
                // мы добавляем фигню в инвентарь
                _inventory.AddItem(Item.Name.Ubisoft, Item.StateOfIncision.Whole, Item.StateOfPreparing.Raw, false);
            }
            else
            {
                // иначе...

                // если найденных рецептов несколько, то берем только первый из найденных
                var found = receipeFounds.First(x => x.IsFound);
                print("result " + found.Result.ToString());

                // и добавляем в инвентарь результат работы рецепта
                _inventory.AddItem(found.Result);
            }

            // очищаем воркбенч
            for (int i = 0; i < _items.Length; i++)
            {
                if(_items[i] != null)
                    DeleteItem(i);
            }
        }
    }

    public bool IsPlace()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
            {
                return true;
            }
        }
        return false;
    }
}
