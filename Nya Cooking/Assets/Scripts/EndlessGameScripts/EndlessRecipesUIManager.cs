using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Button = UnityEngine.UI.Button;

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
    }

    void CloseRecipes()
    {
        _recipesScrollView.SetActive(false);
    }
}
