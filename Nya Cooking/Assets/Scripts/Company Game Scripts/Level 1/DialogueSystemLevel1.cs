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
    private Stove _stove;

    struct DialogueInfo
    {
        public bool IsDialogueUiVisible;
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

    // Use this for initialization
    void Start()
    {
        _dialogueComponents = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DialogueCompanyComponent>();
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _stove = GameObject.FindGameObjectWithTag("Stove").GetComponent<Stove>();
        _stage = 0;

        _dialogues = new Dictionary<Stage, DialogueInfo>
        {
            {
                Stage.Begining, new DialogueInfo
                {
                    IsDialogueUiVisible = true,
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
                    IsDialogueUiVisible = true,
                    IsChatPanelVisible = true,
                    IsAvatarVisible = true,
                    IsNextBtnVisible = true,
                    IsAnswer1BtnVisible = false,
                    IsAnswer2Visible = false,
                    IsAnswer3Visible = false,

                    AvatarPath = "Avatars/tyan",

                    DialogueText = "Молодец, теперь прижарь его на сковороде.",
                    NextBtnText = "Хорошо",
                    NextBtnCallback = () =>
                    {
                        Close();
                        _stage = Stage.Last;
                    }
                }
            },
            {
                Stage.Last, new DialogueInfo
                {
                    IsDialogueUiVisible = true,
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
                    Answer1BtnCallback = () => { Close(); },
                    Answer2BtnCallback = () => { Close(); },
                    Answer3BtnCallback = () =>
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

    enum Stage
    {
        Begining,
        WaitForMeat,
        Last,
        End
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
                        if (_inventory.ReturnItem(i).ItemName == Item.Name.Meat)
                        {
                            UpdateDialogue(_stage);
                            break;
                        }
                    }
                }

                break;
            case Stage.Last:
                if (_stove.ReturnItem() != null)
                {
                    if (_stove.ReturnItem().ItemName == Item.Name.Meat)
                    {
                        UpdateDialogue(_stage);
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

            _dialogueComponents.OnPanels(info.IsDialogueUiVisible, info.IsChatPanelVisible, info.IsAvatarVisible,
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