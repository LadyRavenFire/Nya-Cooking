using UnityEngine;
using Random = System.Random;

public class VisitorsBehaviourEndless : MonoBehaviour
{

    private Item _itemNeed;
    private Item _itemIn;

    public bool IsEnterCollider;
    public bool IsEmpty;

    private float _waitTimer;
    public bool IsWaiting;

    public bool IsClientIn;//need??

    private bool _createdNewTimeToNextClient;
    private float _timeToNextClient;


    // Use this for initialization
    void Start () {
	    _itemIn = null;
        TextureAndCollider(false);
        _createdNewTimeToNextClient = false;
        IsClientIn = false;
        IsEmpty = true;
        IsWaiting = false;
    }

    void FixedUpdate()
    {
        if (IsWaiting)
        {
            WaitTimer();
        }

        if (!IsWaiting)
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
                var time = rnd.Next(10, 15);
                _timeToNextClient = time;
                _createdNewTimeToNextClient = true;
            }
        }  
    }

    void CheckTheProduct()
    {
        if (!IsEmpty)
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
        print("Че то отдал посетителю");
        if (IsEmpty)
        {
            IsEmpty = false;
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

    void Paiment() //need to be update to all versions of game
    {
        EndlessGameVariables levelManagerGameObject = GameObject.FindWithTag("LevelManager").GetComponent<EndlessGameVariables>();
        levelManagerGameObject.AddMoney(100);
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
        var time = rnd.Next(15, 20);
        _waitTimer = time;   
    }

    void ClientEnter()
    {
        NeedItemCreate();
        WaitTimerCreate();
        IsWaiting = true;
        TextureAndCollider(true);
        IsClientIn = true;
    }

    void ClientExitBad()
    {
        IsWaiting = false;
        TextureAndCollider(false);
        IsClientIn = false;
        IsEmpty = true;
        _itemNeed = null;
        _itemIn = null;
    }

    void ClientExitGood()
    {
        IsWaiting = false;
        Paiment();
        TextureAndCollider(false);
        IsClientIn = false;
        IsEmpty = true;
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
