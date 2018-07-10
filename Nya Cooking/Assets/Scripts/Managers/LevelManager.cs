using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    private Button _pauseMenuButton;
    private GameObject _pauseComponent;
    private Button _resumeGameButton;
    private Button _quitGameButton;

    void Start()
    {
        _pauseComponent = GameObject.FindGameObjectWithTag("PauseMenu");

        _pauseMenuButton = GameObject.FindGameObjectWithTag("PauseMenuButton").GetComponent<Button>();
        _resumeGameButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        _quitGameButton = GameObject.Find("QuitButton").GetComponent<Button>();

        _pauseMenuButton.onClick.AddListener(PauseGame);
        _resumeGameButton.onClick.AddListener(ResumeGame);
        _quitGameButton.onClick.AddListener(QuitGame);

        _pauseComponent.SetActive(false);
    }



    void QuitGame()
    {
        Application.Quit();
    }

    void ResumeGame()
    {
        _pauseComponent.SetActive(false);
        Time.timeScale = 1f;
    }
    void PauseGame()
    {
        //timescale паузит игру, тоесть паузит все кроме того,что делается в update и gui
        Time.timeScale = 0f;
        _pauseComponent.SetActive(true);
    }
}
