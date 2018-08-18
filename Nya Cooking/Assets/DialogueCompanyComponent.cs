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

    public Image AvatarImage;

    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        AvatarImage = Avatar.GetComponent<Image>();

        Answer1.SetActive(false);
        Answer2.SetActive(false);
        Answer3.SetActive(false);
        NextDialogue.SetActive(false);
        Avatar.SetActive(false);
        ChatPanel.SetActive(false);
        DialogueUi.SetActive(false);
    }

    public void OnPanels( bool isDialogueUiVisible, bool isChatPanelVisible, bool isAvatarVisivble, bool isNextDialogueVisible, bool isAnswer1Visible, bool isAnswer2Visible, bool isAnswer3Visible)
    {
        Time.timeScale = 0f;

        _inventory.OffInventory();
        InventoryPanel.SetActive(false);

        // TODO isDialogueUiVisible deprecated (удалить isDialogueUiVisible)
        // TODO make ChatPanel.SetActive(isChatPanelVisible);
        // TODO rename function and params

        if (isDialogueUiVisible)
        {
            DialogueUi.SetActive(true);
        }

        if (isChatPanelVisible)
        {
            ChatPanel.SetActive(true);
        }

        if (isAvatarVisivble)
        {
            Avatar.SetActive(true);
        }

        if (isNextDialogueVisible)
        {
            NextDialogue.SetActive(true);
        }

        if (isAnswer1Visible)
        {
            Answer1.SetActive(true);
        }

        if (isAnswer2Visible)
        {
            Answer2.SetActive(true);
        }

        if (isAnswer3Visible)
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
