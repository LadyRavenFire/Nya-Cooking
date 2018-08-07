using System;
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

        if (PlayerPrefs.GetFloat("EndlessStoveUpgrade") >= 3)
        {
            UpgradeStoveButton.enabled = false;
        }
        foreach (var t in Stoves)
        {
            Stove stove = t.GetComponent<Stove>();
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

            foreach (var t in Stoves)
            {
                Stove stove = t.GetComponent<Stove>();
                stove.Upgrade(upgradelevel);
            }

            endlessGameVariables.AddMoney(-UpgradeStoveCost);
        }

        if (PlayerPrefs.GetFloat("EndlessStoveUpgrade") >= 3)
        {
            UpgradeStoveButton.enabled = false;
        }
    }
}

