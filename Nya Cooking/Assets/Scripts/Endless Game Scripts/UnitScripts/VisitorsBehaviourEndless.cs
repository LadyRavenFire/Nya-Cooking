using UnityEngine;
//using Random = System.Random;

//Скрипт описывающий посетителей в первый день
// TODO сделать зависимость выбора рецепта от номера прожитого дня
// TODO решить что делать с временем ожидания клиента и с выбором времени его прихода

public class VisitorsBehaviourEndless : MonoBehaviour
{

    private Item _itemNeed;
    private Item _itemIn;

    //public bool IsEnterCollider; //
    private bool _isEmpty;

    private float _waitTimer;
    private bool _isWaiting;

    public bool IsClientIn; //

    private bool _createdNewTimeToNextClient;
    private float _timeToNextClient;

    //WARNING NEED TO CHANGE!!
    public GameObject SpriteFood;

    private Inventory _inventory;

    /// TODO решить нужно ли переводить на загрузку по имени или лучше выносить объекты через инспектор

    // Use this for initialization
    void Start () {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _itemIn = null;
        TextureAndCollider(false);
        _createdNewTimeToNextClient = false;
        IsClientIn = false;
        _isEmpty = true;
        _isWaiting = false;
        SpriteFood.SetActive(false);

        RandomStart();
    }

    void FixedUpdate()
    {
        if (_isWaiting)
        {
            WaitTimer();
        }

        if (!_isWaiting)
        {
            if (_createdNewTimeToNextClient)
            {
                if (_timeToNextClient > 0)
                {
                    _timeToNextClient -= Time.deltaTime;
                }
                else
                {
                    ClientEnter();
                    _createdNewTimeToNextClient = false;
                }
            }
            else
            {
                var time = RandomCreate.Random.Next(5, 10);
                _timeToNextClient = time;
                _createdNewTimeToNextClient = true;
            }
        }  
    }

    void CheckTheProduct()
    {
        if (!_isEmpty)
        {
            if (_itemNeed == _itemIn)
            {
                ClientExitGood();
            }
            else
            {
                ClientExitBad();
            }
        }
    }

    void OnMouseEnter()
    {
        _inventory.IsInOther();
    }

    void OnMouseExit()
    {
        _inventory.IsNotInOther();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && _inventory.IsDragged() && IsClientIn)
        {
            AddItem(_inventory.GiveDraggedItem());
            _inventory.DeleteDraggedItem();
        }

        if (Input.GetMouseButtonUp(0) && _inventory.IsDragged() && !IsClientIn)
        {
            _inventory.ReturnInInventory();
        }
    }

    public void AddItem(Item item)
    {
        if (_itemIn != null) return;
        _itemIn = item;
        //print("Че то отдал посетителю");
        if (_isEmpty)
        {
            _isEmpty = false;
        }
        CheckTheProduct();
    }

    void NeedItemCreate()
    {
        var receipe = GameObject.FindGameObjectWithTag("Recipes").GetComponent<Recipes>();
        var number = RandomCreate.Random.Next(0, receipe.Receipes.Count);
        _itemNeed = receipe.Receipes[number].Result;

        //warning need to change
        SpriteFood.SetActive(true);
        var sprite = SpriteFood.GetComponent<SpriteRenderer>();
        
        sprite.sprite = Resources.Load<Sprite>("ItemIcons/" + _itemNeed.ItemName + "_" + _itemNeed.stateOfPreparing.ToString("F") + "_" + _itemNeed.stateOfIncision.ToString("F"));
        //warning need to change
    }

    void Paiment(int money) //need to be update to all versions of game
    {
        EndlessGameVariables levelManagerGameObject = GameObject.FindWithTag("LevelManager").GetComponent<EndlessGameVariables>();
        levelManagerGameObject.AddMoney(money);
    }

    void WaitTimer()
    {
        if (_waitTimer > 0)
        {
            _waitTimer -= Time.deltaTime;
        }
        else
        {
            ClientExitBad();
        }          
    }

    void WaitTimerCreate()
    {
        var time = RandomCreate.Random.Next(10, 20);
        _waitTimer = time;   
    }

    void ClientEnter()
    {
        NeedItemCreate();
        WaitTimerCreate();
        _isWaiting = true;
        TextureAndCollider(true);
        IsClientIn = true;
    }

    void ClientExitBad()
    {
        _isWaiting = false;
        TextureAndCollider(false);
        IsClientIn = false;
        _isEmpty = true;
        _itemNeed = null;
        _itemIn = null;
        Paiment(-10);
        SpriteFood.SetActive(false);
    }

    void ClientExitGood()
    {
        _isWaiting = false;
        Paiment(100);
        TextureAndCollider(false);
        IsClientIn = false;
        _isEmpty = true;
        _itemNeed = null;
        _itemIn = null;
        SpriteFood.SetActive(false);
    }

    void TextureAndCollider(bool trigger)
    {       
        var collider2D = gameObject.GetComponent<Collider2D>();
        collider2D.enabled = trigger;
        var texture = gameObject.GetComponent<SpriteRenderer>();
        texture.enabled = trigger;
    }

    void RandomStart()
    {
        // TODO use seed in constructor parameter 
        var text = RandomCreate.Random.Next(5, 10);
    }
}
