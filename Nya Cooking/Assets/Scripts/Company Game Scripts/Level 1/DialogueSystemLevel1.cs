using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystemLevel1 : MonoBehaviour
{

    private DialogueCompanyComponent _dialogueComponents;

    private IEnumerator _textCoroutine;
    private Stage _stage;

    private Inventory _inventory;


    private CuttingBoard _cuttingBoard;
    private Grater _grater;
    private MeatGrinder _meatGrinder;
    private Pan _pan;
    private Stove _stove;

    private Repository _boxWithMeat;
    private Repository _boxWithBread;


    struct DialogueInfo
    {
        public bool IsChatPanelVisible;
        public bool IsAvatarVisible;
        public bool IsNextBtnVisible;
        public bool IsAnswer1BtnVisible;
        public bool IsAnswer2Visible;
        public bool IsAnswer3Visible;

        public string AvatarPath;

        public string DialogueText;
        public string Answer1Text;
        public string Answer2Text;
        public string Answer3Text;
        public string NextBtnText;

        public UnityAction Answer1BtnCallback;
        public UnityAction Answer2BtnCallback;
        public UnityAction Answer3BtnCallback;
        public UnityAction NextBtnCallback;
    }

    private Dictionary<Stage, DialogueInfo> _dialogues;

    enum Stage
    {
        Begining,

        WaitForMeat,
        TakeBreadBeforeMeat,

        DropWholeRawMeatInStove,
        DropWholeRawMeatInAllAnotherPlaces,
        DropWholeRawMeatInGarbage,

        TalkAboutStateOfPreparing,
        TalkAboutCookingInStove,
        AgainTalkWhenDropWholeRawMeatInStove,


        CreateBadMeat,
        TakeBreadWhenCreateWholeFriedMeat,

        WaitForWholeFriedMeat,



        TakeMeatBeforeBread,
        WaitForBread,

        DoBadThing,
        LostAll,
        WaitForSandwich,

        End

    }

    // Use this for initialization
    void Start()
    {
        _dialogueComponents = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DialogueCompanyComponent>();

        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();


        _cuttingBoard = GameObject.Find("CuttingBoard").GetComponent<CuttingBoard>();
        _meatGrinder = GameObject.Find("MeatGrinder").GetComponent<MeatGrinder>();
        _grater = GameObject.Find("Grater").GetComponent<Grater>();
        _pan = GameObject.Find("Pan").GetComponent<Pan>();
        _stove = GameObject.FindGameObjectWithTag("Stove").GetComponent<Stove>();

        _boxWithMeat = GameObject.Find("BoxWithMeat").GetComponent<Repository>();
        _boxWithBread = GameObject.Find("BoxWithBread").GetComponent<Repository>();

        _stage = 0;

        _dialogues = new Dictionary<Stage, DialogueInfo>
        {
            {
                Stage.Begining, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan2",

                    DialogueText =
                        "Доброе утро, братик! С сегодняшнего дня эта закусочная твоя, надеюсь ты сможешь о ней позаботиться. Давай попробуем сделать дедушкин сэндвич. Сходи на склад и достань мясо из коробки.",
                    NextBtnText = "Ок",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForMeat;
                    }
                }
            },
            {
                Stage.WaitForMeat, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan2",

                    DialogueText = "Сейчас прижарь его на сковороде.",
                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.DropWholeRawMeatInStove;
                    }
                }
            },
            {
                Stage.TakeBreadBeforeMeat, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",

                    DialogueText = "Это похоже на мясо? Мне кажется это хлеб. Возьми мясо из соседней коробки!",
                    NextBtnText = "Угу",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForMeat;
                    }
                }
            },
            {
                Stage.DropWholeRawMeatInStove, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = false,
                    IsAnswer1BtnVisible = true,
                    IsAnswer2Visible = true,
                    IsAnswer3Visible = true,

                    AvatarPath = "Avatars/tyan2",

                    DialogueText = "Хорошо, теперь прожарь его до легкой корочки.",
                    Answer1Text = "A что будет, если я его пережарю?",
                    Answer2Text = "Как понять, что мясо готово?",
                    Answer3Text = "Oкей",
                    Answer1BtnCallback = () =>
                    {
                        _stage = Stage.TalkAboutStateOfPreparing;
                        Close();
                    },
                    Answer2BtnCallback = () =>
                    {
                        _stage = Stage.TalkAboutCookingInStove;
                        Close();
                    },
                    Answer3BtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForWholeFriedMeat;
                    }
                }
            },
            {
                Stage.DropWholeRawMeatInAllAnotherPlaces, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",


                    DialogueText = "Я же сказала, положи мясо на сковороду!",

                    NextBtnText = "Хорошо, хорошо...",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.DropWholeRawMeatInStove;
                        _meatGrinder.DeleteItem();
                        _cuttingBoard.DeleteItem();
                        _grater.DeleteItem();
                        _pan.DeleteItem();
                        _inventory.AddItem(Item.Name.Meat,Item.StateOfIncision.Whole,Item.StateOfPreparing.Raw,false);
                    }
                }
            },
            {
                Stage.DropWholeRawMeatInGarbage, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",


                    DialogueText = "Зачем ты выкинул кусок отличной вырезки в мусорку? Ты ебобо?! Иди и возьми новый кусок мяса!",

                    NextBtnText = "Ой...",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _boxWithMeat.AddtoRepository(1, Item.Name.Meat);
                        _stage = Stage.WaitForMeat;
                    }
                }
            },
            {
                Stage.AgainTalkWhenDropWholeRawMeatInStove, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = false,
                    IsAnswer1BtnVisible = true,
                    IsAnswer2Visible = true,
                    IsAnswer3Visible = true,

                    AvatarPath = "Avatars/tyan2",

                    DialogueText = "Готов жарить мясо?",
                    Answer1Text = "A что будет, если я его пережарю?",
                    Answer2Text = "Как понять, что мясо готово?",
                    Answer3Text = "Дыа",
                    Answer1BtnCallback = () =>
                    {
                        _stage = Stage.TalkAboutStateOfPreparing;
                        Close();
                    },
                    Answer2BtnCallback = () =>
                    {
                        _stage = Stage.TalkAboutCookingInStove;
                        Close();
                    },
                    Answer3BtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForWholeFriedMeat;
                    }
                }
            },
            {
                Stage.TalkAboutStateOfPreparing, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",


                    DialogueText = "Оно сгорит, глупенький! Что же оно ещё может сделать?!",

                    NextBtnText = "Ладно, ладно...",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.AgainTalkWhenDropWholeRawMeatInStove;
                    }
                }
            },
            {
                Stage.TalkAboutCookingInStove, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan2",


                    DialogueText = "Все просто! Когда мясо покраснеет, оно будет готово!",

                    NextBtnText = "Окей",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.AgainTalkWhenDropWholeRawMeatInStove;
                    }
                }
            },

            {
                Stage.CreateBadMeat, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",


                    DialogueText = "Братик, ты испортил кусок отличного мяса! Постарайся так больше не делать.",

                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForWholeFriedMeat;
                    }
                }
            },

            {
                Stage.TakeBreadWhenCreateWholeFriedMeat, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",


                    DialogueText = "Положи этот кусок хлеба, сейчас он тебе ещё не нужен.",

                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForWholeFriedMeat;
                    }
                }
            },

            {
                Stage.WaitForWholeFriedMeat, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan2",


                    DialogueText = "Отлично, а сейчас достань кусок хлеба из коробки.",

                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForBread;
                    }
                }
            },

            {
                Stage.TakeMeatBeforeBread, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",


                    DialogueText = "Я попросила взять тебя кусок хлеба, а ты взял ещё один кусок мяса. Зачем?",

                    NextBtnText = "Оу...",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForBread;
                    }
                }
            },

            {
                Stage.WaitForBread, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan2",


                    DialogueText = "Теперь просто сделай бутерброд на рабочем столе. Рецепт бутреброда можно посмотреть в книге рецептов. " +
                                   "(Рабочий стол работает по принципу положил в него нужные ингридиенты, нажал, получил продукт. Однако, если положить туда не те продукты, которые требуются, на выходе получишь что - то совсем иное...)",

                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForSandwich;
                    }
                }
            },

            {
                Stage.DoBadThing, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",


                    DialogueText =
                        "Не очень то похоже на сэндвич! Открой книгу рецептов, прочти её и попробуй снова! >_>",

                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForSandwich;
                    }
                }
            },

            {
                Stage.LostAll, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",


                    DialogueText =
                        "<_< Ты похерил всю еду на кухне! Фиговый из тебя повар, попробуй ещё раз. >_>",

                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.WaitForSandwich;
                    }
                }
            },

            {
                Stage.WaitForSandwich, new DialogueInfo
                {
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan2",


                    DialogueText = "Думаю ты справишься с покупателями в первые дни. Главное не забудь читать книгу рецептов в поисках новых рецептов!",

                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.End;
                    }
                }
            }

        };
    }

    void Update()
    {
        _textCoroutine.MoveNext();
    }

    

    void FixedUpdate()
    {
        switch (_stage)
        {
            case Stage.Begining:
                UpdateDialogue(_stage);
                break;

            case Stage.WaitForMeat:
                for (int i = 0; i < _inventory.ReturnSlots(); i++)
                {
                    if (_inventory.ReturnItem(i) != null)
                    {
                        if (_inventory.ReturnItem(i).ItemName == Item.Name.Meat &&
                            _inventory.ReturnItem(i).stateOfPreparing == Item.StateOfPreparing.Raw &&
                            _inventory.ReturnItem(i).stateOfIncision == Item.StateOfIncision.Whole)
                        {
                            UpdateDialogue(_stage);
                            break;
                        }
                        else
                        {
                            if (_inventory.ReturnItem(i).ItemName == Item.Name.Bread)
                            {
                                _stage = Stage.TakeBreadBeforeMeat;
                                UpdateDialogue(_stage);
                                _boxWithBread.AddtoRepository(1, Item.Name.Bread);
                                _inventory.DeleteItem(i);
                                break;                              
                            }
                        }
                    }
                }
                break;

            case Stage.DropWholeRawMeatInStove:
                if (_stove.ReturnItem() != null)
                {
                    if (_stove.ReturnItem().ItemName == Item.Name.Meat)
                    {
                        UpdateDialogue(_stage);
                        break;
                    }
                }
                else if (_meatGrinder.ReturnItem() != null)
                {
                    if (_meatGrinder.ReturnItem().ItemName == Item.Name.Meat)
                    {
                        _stage = Stage.DropWholeRawMeatInAllAnotherPlaces;
                        UpdateDialogue(_stage);
                        break;
                    }
                }
                else if (_cuttingBoard.ReturnItem() != null)
                {
                    if (_cuttingBoard.ReturnItem().ItemName == Item.Name.Meat)
                    {
                        _stage = Stage.DropWholeRawMeatInAllAnotherPlaces;
                        UpdateDialogue(_stage);
                        break;
                    }
                }
                else if (_grater.ReturnItem() != null)
                {
                    if (_grater.ReturnItem().ItemName == Item.Name.Meat)
                    {
                        _stage = Stage.DropWholeRawMeatInAllAnotherPlaces;
                        UpdateDialogue(_stage);
                        break;
                    }
                }
                else if (_pan.ReturnItem() != null)
                {
                    if (_pan.ReturnItem().ItemName == Item.Name.Meat)
                    {
                        _stage = Stage.DropWholeRawMeatInAllAnotherPlaces;
                        UpdateDialogue(_stage);
                        break;
                    }
                }
                else
                if (_pan.ReturnItem() == null && _grater.ReturnItem() == null && _cuttingBoard.ReturnItem() == null && _meatGrinder.ReturnItem() == null && _stove.ReturnItem() == null)
                {
                    bool garbageFlag = true;
                    for (int i = 0;
                        i < _inventory.ReturnSlots();
                        i++)
                    {
                        if (_inventory.ReturnItem(i) != null)
                        {
                            garbageFlag = false;
                        }
                    }

                    if (_inventory.ReturnDraggedItem() != null)
                    {
                        garbageFlag = false;
                    }

                    if (garbageFlag)
                    {
                        _stage = Stage.DropWholeRawMeatInGarbage;
                        UpdateDialogue(_stage);
                        break;
                    }
                }              
                break;

            case Stage.TalkAboutStateOfPreparing:
                UpdateDialogue(_stage);
                break;

            case Stage.TalkAboutCookingInStove:
                UpdateDialogue(_stage);
                break;

            case Stage.AgainTalkWhenDropWholeRawMeatInStove:
                UpdateDialogue(_stage);
                break;

            case Stage.WaitForWholeFriedMeat:
                for (int i = 0; i < _inventory.ReturnSlots(); i++)
                {
                    Item item = _inventory.ReturnItem(i);
                    if (item != null)
                    {
                        if (item.ItemName == Item.Name.Meat &&
                            item.stateOfPreparing == Item.StateOfPreparing.Fried &&
                            item.stateOfIncision == Item.StateOfIncision.Whole)
                        {
                            UpdateDialogue(_stage);
                            break;
                        }

                        if (item.ItemName == Item.Name.Meat &&
                            item.stateOfIncision != Item.StateOfIncision.Whole)
                        {
                            _inventory.DeleteItem(i);
                            _boxWithBread.AddtoRepository(1, Item.Name.Bread);
                            _boxWithMeat.AddtoRepository(1, Item.Name.Meat);
                            _stage = Stage.CreateBadMeat;
                            UpdateDialogue(_stage);
                            break;
                        }

                        if (item.ItemName == Item.Name.Meat &&
                            item.stateOfPreparing != Item.StateOfPreparing.Fried)
                        {
                            if (item.stateOfPreparing != Item.StateOfPreparing.Raw)
                            {
                                _inventory.DeleteItem(i);
                                _boxWithBread.AddtoRepository(1, Item.Name.Bread);
                                _boxWithMeat.AddtoRepository(1, Item.Name.Meat);
                                _stage = Stage.CreateBadMeat;
                                UpdateDialogue(_stage);
                                break;
                            }
                        }

                        if (item.ItemName == Item.Name.Bread)
                        {
                            _inventory.DeleteItem(i);
                            _boxWithBread.AddtoRepository(1, Item.Name.Bread);
                            _boxWithMeat.AddtoRepository(1, Item.Name.Meat);
                            _stage = Stage.TakeBreadWhenCreateWholeFriedMeat;
                            UpdateDialogue(_stage);
                            break;
                        }

                    }
                }
                break;

            case Stage.WaitForBread:
                for (int i = 0; i < _inventory.ReturnSlots(); i++)
                {
                    if (_inventory.ReturnItem(i) != null)
                    {
                        if (_inventory.ReturnItem(i).ItemName == Item.Name.Bread &&
                            _inventory.ReturnItem(i).stateOfPreparing == Item.StateOfPreparing.Raw &&
                            _inventory.ReturnItem(i).stateOfIncision == Item.StateOfIncision.Whole)
                        {
                            UpdateDialogue(_stage);
                            break;
                        }
                        if (_inventory.ReturnItem(i).ItemName == Item.Name.Meat &&
                            _inventory.ReturnItem(i).stateOfPreparing == Item.StateOfPreparing.Raw &&
                            _inventory.ReturnItem(i).stateOfIncision == Item.StateOfIncision.Whole)
                        {
                            _stage = Stage.TakeMeatBeforeBread;
                            _boxWithMeat.AddtoRepository(1, Item.Name.Meat);
                            _inventory.DeleteItem(i);
                            UpdateDialogue(_stage);
                            break;
                        }
                    }
                }
                break;

            case Stage.WaitForSandwich:
                bool emptyflag = true;
                for (int i = 0; i < _inventory.ReturnSlots(); i++)
                {
                    if (_inventory.ReturnItem(i) != null)
                    {
                        if (_inventory.ReturnItem(i).ItemName == Item.Name.Sandwich &&
                            _inventory.ReturnItem(i).stateOfPreparing == Item.StateOfPreparing.Raw &&
                            _inventory.ReturnItem(i).stateOfIncision == Item.StateOfIncision.Whole)
                        {
                            UpdateDialogue(_stage);
                            break;
                        }
                        else if(_inventory.ReturnItem(i).ItemName == Item.Name.Ubisoft)
                        {
                            _stage = Stage.DoBadThing;
                            UpdateDialogue(_stage);
                            _inventory.DeleteItem(i);
                            _boxWithMeat.AddtoRepository(1, Item.Name.Meat);
                            _boxWithBread.AddtoRepository(1, Item.Name.Bread);
                            break;
                        }

                        emptyflag = false;
                    }
                }

                if (emptyflag)
                {
                    if (_boxWithBread.IsEmptyRepository())
                    {
                        if (_boxWithMeat.IsEmptyRepository())
                        {
                            if (_inventory.ReturnDraggedItem() == null)
                            {
                                _stage = Stage.LostAll;
                                _boxWithBread.AddtoRepository(1, Item.Name.Bread);
                                _boxWithMeat.AddtoRepository(1, Item.Name.Meat);
                                UpdateDialogue(_stage);
                                break;
                            }
                        }
                    }  
                }
                break;


            default:
                break;
        }
    }

    void UpdateDialogue(Stage stage)
    {
        try
        {
            DialogueInfo info = _dialogues[stage];

            _dialogueComponents.OnPanels(info.IsChatPanelVisible, info.IsAvatarVisible,
                info.IsNextBtnVisible, info.IsAnswer1BtnVisible, info.IsAnswer2Visible, info.IsAnswer3Visible);
            _dialogueComponents.ChatText.text = null;
            _textCoroutine = TextCoroutine(info.DialogueText);
            _dialogueComponents.AvatarImage.sprite = Resources.Load<Sprite>(info.AvatarPath);

            _dialogueComponents.NextButtonText.text = info.NextBtnText;
            _dialogueComponents.Answer1Text.text = info.Answer1Text;
            _dialogueComponents.Answer2Text.text = info.Answer2Text;
            _dialogueComponents.Answer3Text.text = info.Answer3Text;

            _dialogueComponents.NextDialogueButton.onClick.RemoveAllListeners();
            _dialogueComponents.Answer1Button.onClick.RemoveAllListeners();
            _dialogueComponents.Answer2Button.onClick.RemoveAllListeners();
            _dialogueComponents.Answer3Button.onClick.RemoveAllListeners();

            _dialogueComponents.NextDialogueButton.onClick.AddListener(info.NextBtnCallback);
            _dialogueComponents.Answer1Button.onClick.AddListener(info.Answer1BtnCallback);
            _dialogueComponents.Answer2Button.onClick.AddListener(info.Answer2BtnCallback);
            _dialogueComponents.Answer3Button.onClick.AddListener(info.Answer3BtnCallback);
        }
        catch (Exception)
        {
            print("Oops. Wrong stage");
        }
    }

    void Close()
    {
        _dialogueComponents.OffPanels();
        print(_stage);
        
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