using UnityEngine;
using UnityEngine.UI;

public class EndlessUpgrade : MonoBehaviour
{
    public Button UpgradeStoveButton;
    public int UpgradeStoveCost = 100;
    public GameObject[] Stoves;

    void Start()
    {
        UpgradeStoveButton.onClick.AddListener(Upgrade);
        for (int i = 0; i < Stoves.Length; i++)
        {
            Stove stove = Stoves[i].GetComponent<Stove>();
            float upgradelevel = PlayerPrefs.GetFloat("EndlessStoveUpgrade");
            stove.Upgrade(upgradelevel);
        }
    }

    void Upgrade()
    {
        var endlessGameVariables = GameObject.Find("LevelManager").GetComponent<EndlessGameVariables>();
        if (endlessGameVariables.ReturnMoney() > UpgradeStoveCost)
        {
            float upgradelevel = PlayerPrefs.GetFloat("EndlessStoveUpgrade");
            upgradelevel++;
            PlayerPrefs.SetFloat("EndlessStoveUpgrade", upgradelevel);
            print(PlayerPrefs.GetFloat("EndlessStoveUpgrade"));
            for (int i = 0; i < Stoves.Length; i++)
            {
                Stove stove = Stoves[i].GetComponent<Stove>();
                stove.Upgrade(upgradelevel);
            }
            endlessGameVariables.AddMoney(-UpgradeStoveCost);
        }
    }
}

