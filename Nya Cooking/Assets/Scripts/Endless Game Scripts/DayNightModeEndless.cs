using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Скрипт описывающий смену дней в бесконечной игре
// TODO реализовать фактическую смену дней за счет подведения итогов и включения возможности прокачки способностей

public class DayNightModeEndless : MonoBehaviour
{
    public float DayTime = 15;
    private int _number;

    private GameObject _inventoryPanel;
    private GameObject _nextDayPanel;
    private Button _changedaybutton;
    private Inventory _inventory;

    private EndlessGameVariables _endlessGameVariables;
    
    private List<GameObject> _visitors = new List<GameObject>();


    void Start()
    {
        _number = 3;

        _nextDayPanel = GameObject.Find("ChangeDayPanel");
        _changedaybutton = GameObject.Find("ChangeDayButton").GetComponent<Button>();
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        _endlessGameVariables = GameObject.Find("LevelManager").GetComponent<EndlessGameVariables>();

        _inventoryPanel = GameObject.Find("InventoryPanel");

        _changedaybutton.onClick.AddListener(NewTime);

        for (int i = 0; i < _number; i++)
        {
            _visitors.Add(null);
        }

        for (int i = 0; i < _number; i++)
        {
            GameObject _visitor = GameObject.Find("mexican" + i);
            _visitors[i] = _visitor;
        }

        _nextDayPanel.SetActive(false);
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		TimeCalculating();
	}

    void TimeCalculating()
    {
        if (DayTime > 0)
        {
            DayTime -= Time.deltaTime;
        }

        if (DayTime < 0)
        {
            DayChanging();
        }
    }

    void DayChanging()
    {
        //print("День закончился");

        for (int i = 0; i < _number; i++)
        {
            if (!_visitors[i].GetComponent<VisitorsBehaviourEndless>().IsClientIn)
            {
                //print(i);
                _visitors[i].SetActive(false);
            }
        }

        bool flag = false;

        for (int i = 0; i < _number; i++)
        {
            if (_visitors[i].GetComponent<VisitorsBehaviourEndless>().IsClientIn)
            {
                flag = true;
            }
        }

        if (!flag)
        {
            Time.timeScale = 0f;
            _inventory.OffInventory();
            _nextDayPanel.SetActive(true);
            _inventoryPanel.SetActive(false);

            _endlessGameVariables.SaveToData();
            PlayerPrefs.Save();
        }       
    }

    void NewTime()
    {
        DayTime = 30;
        //_endlessGameVariables.AddMoney(400);
        _inventory.OnInventory();
        _nextDayPanel.SetActive(false);
        _inventoryPanel.SetActive(true);

        for (int i = 0; i < _number; i++)
        {
            _visitors[i].SetActive(true);
        }

        Time.timeScale = 1f;
    }
}
