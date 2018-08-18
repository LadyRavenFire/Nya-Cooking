using UnityEngine;
using UnityEngine.UI;

public class DialogueCompanyComponent : MonoBehaviour
{
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
    public Button Anwer3Button;

    public Text ChatText;

    public Text NextButtonText;

    void Start()
    {
        Answer1.SetActive(false);
        Answer2.SetActive(false);
        Answer3.SetActive(false);
        NextDialogue.SetActive(false);
        Avatar.SetActive(false);
        ChatPanel.SetActive(false);
        DialogueUi.SetActive(false);
    }

}
