﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystemLevel1 : MonoBehaviour
{

    private DialogueCompanyComponent _dialogueComponents;

    private IEnumerator _textCoroutine;
    private int _flag;

    private Inventory _inventory;
    private Stove _stove;

	// Use this for initialization
	void Start ()
	{
	    _dialogueComponents = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DialogueCompanyComponent>();
	    _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
	    _stove = GameObject.FindGameObjectWithTag("Stove").GetComponent<Stove>();
        _flag = 0;	    
	}

    void Update()
    {
        _textCoroutine.MoveNext();
    }

    void FixedUpdate()
    {
        if (_flag == 0)
        {
            FirstDialogue();
        }

        if (_flag == 1)
        {
            for (int i = 0; i < _inventory.ReturnSlots(); i++)
            {
                if (_inventory.ReturnItem(i)!= null)
                {
                    if (_inventory.ReturnItem(i).ItemName == Item.Name.Meat)
                    {
                        SecondDialogue();
                        break;
                    }                   
                }
            }
        }

        if (_flag == 2)
        {
            if (_stove.ReturnItem() != null)
            {
                if (_stove.ReturnItem().ItemName == Item.Name.Meat)
                {
                   ThirdDialogue(); 
                }
            }
        }
    }

    void FirstDialogue()
    {      
        _dialogueComponents.OnPanels(true, true, true, true, false, false, false);
        Image avatar = _dialogueComponents.Avatar.GetComponent<Image>();
        avatar.sprite = Resources.Load<Sprite>("Avatars/tyan2");
        _dialogueComponents.ChatText.text = null;
        _textCoroutine = TextCoroutine("Доброе утро, братик! С сегодняшнего дня эта закусочная твоя, надеюсь ты сможешь о ней позаботиться. Давай попробуем сделать дедушкин сэндвич. Сходи на склад и достань мясо из коробки."); 
        _dialogueComponents.NextButtonText.text = "Ок";
        _dialogueComponents.NextDialogueButton.onClick.AddListener(Close);

        _flag = 1;
    }

    void Close()
    {
        _dialogueComponents.OffPanels();
        print(_flag);
    }

    void SecondDialogue()
    {
        _dialogueComponents.OnPanels(true, true, true, true, false, false, false);
        Image avatar = _dialogueComponents.Avatar.GetComponent<Image>();
        avatar.sprite = Resources.Load<Sprite>("Avatars/tyan");
        _dialogueComponents.ChatText.text = null;
        _textCoroutine = TextCoroutine("Молодец, теперь прижарь его на сковороде.");
        _dialogueComponents.NextButtonText.text = "Хорошо";
        _dialogueComponents.NextDialogueButton.onClick.AddListener(Close);
        _flag = 2;
    }

    void ThirdDialogue()
    {
        _dialogueComponents.OnPanels(true, true, true, false, true, true, true);
        _dialogueComponents.ChatText.text = null;
        _textCoroutine = TextCoroutine("Хорошо, теперь прожарь его до легкой корочки.");
        Image avatar = _dialogueComponents.Avatar.GetComponent<Image>();
        avatar.sprite = Resources.Load<Sprite>("Avatars/tyan2");
        _dialogueComponents.Answer1Text.text = "A что будет, если я его пережарю?";
        _dialogueComponents.Answer2Text.text = "Как понять, что мясо готово?";
        _dialogueComponents.Answer3Text.text = "Oкей";

        _dialogueComponents.Answer1Button.onClick.AddListener(delegate { ThirdDialogue2(1); });
        _dialogueComponents.Answer2Button.onClick.AddListener(delegate { ThirdDialogue2(2); });
        _dialogueComponents.Answer3Button.onClick.AddListener(delegate {ThirdDialogue2(3);});
        
    }

    void ThirdDialogue2(int flag)
    {
        if (flag == 1)
        {
            Close();
            return;
        }

        if (flag == 2)
        {
            Close();
            return;
        }

        if (flag == 3)
        {
            _flag = 3;
            Close();
            return;
        }
    }

    IEnumerator TextCoroutine(string text)
    {
        foreach (char c in text)
        {
            //print(c);
            _dialogueComponents.ChatText.text = _dialogueComponents.ChatText.text + c;
            yield return new WaitForEndOfFrame();
        }
    }

}
