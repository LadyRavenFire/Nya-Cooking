using UnityEngine;
using UnityEngine.UI;

public class DialogueCompanyComponent : MonoBehaviour
{
    private Inventory _inventory;

    public GameObject InventoryPanel;
    public GameObject DialogueUi;

    public GameObject ChatPanel;
    public GameObject Avatar;

    public GameObject NextDialogue;
    public GameObject Answer1;
    public GameObject Answer2;
    public GameObject Answer3;

    public Button NextDialogueButton;
    public Button Answer1Button;
    public Button Answer2Button;
    public Button Answer3Button;

    public Text ChatText;

    public Text NextButtonText;

    public Text Answer1Text;
    public Text Answer2Text;
    public Text Answer3Text;

    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        Answer1.SetActive(false);
        Answer2.SetActive(false);
        Answer3.SetActive(false);
        NextDialogue.SetActive(false);
        Avatar.SetActive(false);
        ChatPanel.SetActive(false);
        DialogueUi.SetActive(false);
    }

    public void OnPanels( bool DialogueUibool, bool ChatPanelbool, bool Avatarbool, bool NextDialoguebool, bool Answer1bool, bool Answer2bool, bool Answer3bool)
    {
        Time.timeScale = 0f;

        _inventory.OffInventory();
        InventoryPanel.SetActive(false);        

        if (DialogueUibool)
        {
            DialogueUi.SetActive(true);
        }

        if (ChatPanelbool)
        {
            ChatPanel.SetActive(true);
        }

        if (Avatarbool)
        {
            Avatar.SetActive(true);
        }

        if (NextDialoguebool)
        {
            NextDialogue.SetActive(true);
        }

        if (Answer1bool)
        {
            Answer1.SetActive(true);
        }

        if (Answer2bool)
        {
            Answer2.SetActive(true);
        }

        if (Answer3bool)
        {
           Answer3.SetActive(true); 
        }
        
    }

    public void OffPanels()
    {
        DialogueUi.SetActive(false);
        ChatPanel.SetActive(false);
        Avatar.SetActive(false);
        NextDialogue.SetActive(false);
        Answer1.SetActive(false);
        Answer2.SetActive(false);
        Answer3.SetActive(false);

        InventoryPanel.SetActive(true);
        _inventory.OnInventory();

        Time.timeScale = 1f;
    }
}
