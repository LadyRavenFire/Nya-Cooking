using UnityEngine;
//using Random = System.Random;

//Скрипт описывающий посетителей в первый день
// TODO сделать зависимость выбора рецепта от номера прожитого дня
// TODO решить что делать с временем ожидания клиента и с выбором времени его прихода

public class VisitorsBehaviourEndless : MonoBehaviour
{

    private Item _itemNeed;
    private Item _itemIn;

    public bool IsEnterCollider; //
    private bool _isEmpty;

    private float _waitTimer;
    private bool _isWaiting;

    public bool IsClientIn; //

    private bool _createdNewTimeToNextClient;
    private float _timeToNextClient;

    //WARNING NEED TO CHANGE!!
    public GameObject SpriteFood;

    /// TODO решить нужно ли переводить на загрузку по имени или лучше выносить объекты через инспектор

    // Use this for initialization
    void Start () {
	    _itemIn = null;
        TextureAndCollider(false);
        _createdNewTimeToNextClient = false;
        IsClientIn = false;
        _isEmpty = true;
        _isWaiting = false;
        SpriteFood.SetActive(false);

        // TODO use seed in constructor parameter 
        var text = RandomCreate.Random.Next(5, 10);
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
                    //print("Client will come: " + _timeToNextClient);
                }
                else
                {
                    ClientEnter();
                    _createdNewTimeToNextClient = false;
                }
            }
            else
            {
                //var rnd = new Random();
                
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
                //gameObject.SetActive(false);
            }
            else
            {
                ClientExitBad();
            }
        }
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
       // var rnd = new Random();
        var number = RandomCreate.Random.Next(0, receipe.Receipes.Count);
        _itemNeed = receipe.Receipes[number].Result;
        //print(_itemNeed.ItemName.ToString() + _itemNeed.stateOfIncision.ToString() + _itemNeed.stateOfPreparing.ToString());

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
            //print("Client will go away frome: " + _waitTimer);
        }
        else
        {
            ClientExitBad();
        }          
    }

    void WaitTimerCreate()
    {
        //var rnd = new Random();
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
}
