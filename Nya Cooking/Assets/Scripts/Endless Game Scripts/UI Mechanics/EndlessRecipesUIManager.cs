using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Button = UnityEngine.UI.Button;

// Скрипт описывающий меню рецептов в бесконечной игре

public class EndlessRecipesUIManager : MonoBehaviour
{
    private Button _recipes;
    private Button _exitRecipes;
    private GameObject _recipesScrollView;

	// Use this for initialization
	void Start ()
	{
	    _recipes = GameObject.Find("RecipesButton").GetComponent<Button>();
	    _recipesScrollView = GameObject.Find("RecipesUI");
	    _exitRecipes = GameObject.Find("ExitRecipesButton").GetComponent<Button>();

	    _recipes.onClick.AddListener(OpenRecipes);
	    _exitRecipes.onClick.AddListener(CloseRecipes);

        _recipesScrollView.SetActive(false);
    }

    void OpenRecipes()
    {
        _recipesScrollView.SetActive(true);
        Time.timeScale = 0f;
    }

    void CloseRecipes()
    {
        _recipesScrollView.SetActive(false);
        Time.timeScale = 1f;
    }
}
