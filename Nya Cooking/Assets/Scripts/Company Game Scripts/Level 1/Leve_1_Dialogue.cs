using UnityEngine;
using UnityEngine.UI;


//Диалоговая система 1 уровня

public class Leve_1_Dialogue : MonoBehaviour
{
    // TODO подключение элементов через поиск по инспектору
    public GameObject DialoguePanel;
    public Image Avatar;
    public Text Text;
    public Button NextTextButton;

    void Start()
    {
        NextTextButton.onClick.AddListener(Clear);
        Time.timeScale = 0f;
    }

    void Clear()
    {
        DialoguePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
