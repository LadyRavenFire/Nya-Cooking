using UnityEngine;

public class DialogueSystemLevel1 : MonoBehaviour
{

    private DialogueCompanyComponent _dialogueComponents;
    private int _flag;
    private Inventory _inventory;

	// Use this for initialization
	void Start ()
	{
	    _dialogueComponents = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<DialogueCompanyComponent>();
	    _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        _flag = 0;
	    
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
    }

    void FirstDialogue()
    {      
        Time.timeScale = 0f;        
        _inventory.OffInventory();
        _dialogueComponents.InventoryPanel.SetActive(false);
        _dialogueComponents.DialogueUi.SetActive(true);
        _dialogueComponents.Avatar.SetActive(true);
        _dialogueComponents.ChatPanel.SetActive(true);
        _dialogueComponents.ChatText.text = "Достань мясо из коробки";
        _dialogueComponents.NextDialogue.SetActive(true);
        _dialogueComponents.NextButtonText.text = "Ок";
        _dialogueComponents.NextDialogueButton.onClick.AddListener(Close);

        _flag = 1;
    }

    void Close()
    {
        print("Lol");

        _dialogueComponents.NextDialogue.SetActive(false);
        _dialogueComponents.ChatPanel.SetActive(false);
        _dialogueComponents.Avatar.SetActive(false);
        _dialogueComponents.DialogueUi.SetActive(false);
        _dialogueComponents.InventoryPanel.SetActive(true);
        _inventory.OnInventory();
        Time.timeScale = 1f;
        print(_flag);
    }

    void SecondDialogue()
    {
        Time.timeScale = 0f;
        _inventory.OffInventory();
        _dialogueComponents.InventoryPanel.SetActive(false);
        _dialogueComponents.DialogueUi.SetActive(true);
        _dialogueComponents.Avatar.SetActive(true);
        _dialogueComponents.ChatPanel.SetActive(true);
        _dialogueComponents.ChatText.text = "Молодец, теперь пожарь его";
        _dialogueComponents.NextDialogue.SetActive(true);
        _dialogueComponents.NextButtonText.text = "Ок";
        _dialogueComponents.NextDialogueButton.onClick.AddListener(Close);
        _flag = 2;
    }
    
	
	
}
