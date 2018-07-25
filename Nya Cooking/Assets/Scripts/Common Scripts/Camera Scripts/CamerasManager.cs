using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


// Этот скрипт для перемещения камеры из 1 комнаты в другую. В данном случае комнат 4
// Это комната ГГ, комната готовки, склад, комната приема посетителей

public class CamerasManager : MonoBehaviour {

    public List<Camera> Cameras; // TODO из проблем - правиьный порядок закидывания камер
    private int _count;

    private Button _leftButton;
    private Button _rightButton;

    void Start()
    {
        //тут пример поиска по имени
        _leftButton = GameObject.Find("LeftChangeCameraButton").GetComponent<Button>();
        _rightButton = GameObject.Find("RightChangeCameraButton").GetComponent<Button>();

        for (int i = 1; i < Cameras.Count; i++)
        {            
            var audio = Cameras[i].GetComponent<AudioListener>();
            audio.enabled = false;
            Cameras[i].enabled = false;
        }

        //var kitchenCamera = Cameras.First(x => x.name == "kitchen");

        //Camera kitchen1 = Cameras.FirstOrDefault(t => t.name == "kitchenCamera");

        _count = 0;

        //Сюда подцепляем кнопки и ждем клика, заодно говорим, какой ивент случится при клике на кнопку.
        _leftButton.onClick.AddListener(LeftButton_Click);
        _rightButton.onClick.AddListener(RightButton_Click);
    }

    //ивент на левую кнопку мыши
    void LeftButton_Click()
    {
        var audio = Cameras[_count].GetComponent<AudioListener>();
        audio.enabled = false;
        Cameras[_count].enabled = false;

        if (_count != 0)
        {
            _count--;
        }

        if (_count == 0)
        {
            _count = Cameras.Count-1;
            //print(_count);
        }

        Cameras[_count].enabled = true;
        audio = Cameras[_count].GetComponent<AudioListener>();
        audio.enabled = true;
        
    }
    //ивент на правую кнопку мыши
    void RightButton_Click()
    {
        var audio = Cameras[_count].GetComponent<AudioListener>();
        audio.enabled = false;
        Cameras[_count].enabled = false;

        if (_count < Cameras.Count)
        {
            _count++;
        }

        if (_count == Cameras.Count)
        {
            _count = 0;
        }

        Cameras[_count].enabled = true;
        audio = Cameras[_count].GetComponent<AudioListener>();
        audio.enabled = true;       
    }
}
