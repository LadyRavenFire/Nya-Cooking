using UnityEngine;
using Random = System.Random;

public class VisitorsBehaviourEndless : MonoBehaviour
{

    private Item _itemNeed;
    private Item _itemIn;

    public bool IsEnterCollider; //
    private bool _isEmpty;

    private float _waitTimer;
    private bool _isWaiting;

    public bool IsClientIn;//

    private bool _createdNewTimeToNextClient;
    private float _timeToNextClient;


    // Use this for initialization
    void Start () {
	    _itemIn = null;
        TextureAndCollider(false);
        _createdNewTimeToNextClient = false;
        IsClientIn = false;
        _isEmpty = true;
        _isWaiting = false;
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
                var rnd = new Random();
                var time = rnd.Next(5, 10);
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
        var recipes = receipe.Receipes;
        var rnd = new Random();
        var number = rnd.Next(0, recipes.Count);
        _itemNeed = recipes[number].Result;
        print(_itemNeed.ItemName.ToString() + _itemNeed.stateOfIncision.ToString() + _itemNeed.stateOfPreparing.ToString());

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
        var rnd = new Random();
        var time = rnd.Next(10, 20);
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
    }

    void TextureAndCollider(bool trigger)
    {       
        var collider2D = gameObject.GetComponent<Collider2D>();
        collider2D.enabled = trigger;
        var Texture = gameObject.GetComponent<SpriteRenderer>();
        Texture.enabled = trigger;
    }  
}
