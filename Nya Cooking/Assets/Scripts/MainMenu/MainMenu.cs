using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //все панели меню
    private GameObject _mainMenuBasic;
    private GameObject _companyMenuBasic;
    private GameObject _endlessGameMenuBasic;
    private GameObject _settingsBasic;

    //кнопки основного меню


    //кнопки меню компании


    //кнопки бесконечной игры


    //кнопки настроек

	// Use this for initialization
	void Start () {
		_mainMenuBasic = GameObject.Find("MainMenuBasic");
        _companyMenuBasic = GameObject.Find("CompanyMenuBasic");
        _endlessGameMenuBasic = GameObject.Find("EndlessGameMenuBasic");
        _settingsBasic = GameObject.Find("SettingsBasic");

        _companyMenuBasic.SetActive(false);
        _endlessGameMenuBasic.SetActive(false);
        _settingsBasic.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
