using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//я решил не перемещать камеру, а делать 4 камеры и менять их, т.к. просто перемещать main camera нельзя. 
//возможен вариант с тем, что бы прикрепить камеру к пустому объекту, но тогда нужно запоминать позиции, в которые нужно переместить камеру... ну, как вариант
//нужно будет перенести в другой скрипт все связанное с камерой, ибо тут будут храниться данные, которые я буду загружать и выгружать в игру при начале уровня.
//главным минусом пока что то, что я компоненты не получаю кодом, а в инспектор переношу. Стоит подумать о том, как начать их получать, но вводить 7 тегов для кнопок и камер это убого
//на самом деле сделана довольно большая работа над кнопками и текстом, теперь кнопки, текст, ТЕКСТУРКИ В ИГРЕ, масштабируются относительно разрешения. 
//Осталось сделать ещё какой - то сложный принцип работы и масштабирования для инвентаря.... 
public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _money = 0;
    [SerializeField] private Text _textComponent;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Camera _camera1;
    [SerializeField] private Camera _camera2;
    [SerializeField] private Camera _camera3;
    [SerializeField] private Camera _camera4;
    [SerializeField] private Room _room;

    //тут будут номера комнат храниться, в которых может быть игрок. Удобно и можно расширять в зависимости от ивентов(для ивентов?).
    private enum Room
    {
        Kitchen,
        Gosti,
        Sklad,
        MyRoom
    }

    void Start()
    {
        CameraRoom(_room);
        //Сюда подцепляем кнопки и ждем клика, заодно говорим, какой ивент случится при клике на кнопку.
        _leftButton.onClick.AddListener(TaskOnClickLeft);
        _rightButton.onClick.AddListener(TaskOnClickRight);
    }

    void Update()
    {
        //просто увеличиваем деньги каждый кадр и выводим их на верх
        _money++;
        UpdateMoney();
    }

    void UpdateMoney()
    {
        _textComponent.text = "Money: " + _money.ToString();
    }
    //ивент на левую кнопку мыши
    void TaskOnClickLeft()
    {
        //Debug.Log("You have clicked the button left!");
        if (_room == Room.Kitchen)
        {   
            _room = Room.MyRoom;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.MyRoom)
        {
            _room = Room.Sklad;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.Sklad)
        {
            _room = Room.Gosti;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.Gosti)
        {
            _room = Room.Kitchen;
            CameraRoom(_room);
        }
        
    }
    //ивент на правую кнопку мыши
    void TaskOnClickRight()
    {
        //Debug.Log("You have clicked the button right!");
        if (_room == Room.MyRoom)
        {
            _room = Room.Kitchen;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.Kitchen)
        {
            _room = Room.Gosti;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.Gosti)
        {
            _room = Room.Sklad;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.Sklad)
        {
            _room = Room.MyRoom;
            CameraRoom(_room);
        }    
    }

    //теперь о наркомании ниже. Т.к. в игре нельзя включать 2 audiolistner, в каждой камере их нужно отключать. 
    //позже просто возможно вынесу audiolistner в... хм.. в левел манагер и буду оттуда его контролировать, но пока что так
    //в остальном суть проста, мы включаем камеру в нужной комнате, выключаем в остальных. 
    //можно оптимизировать, есть мысли как. Но не знаю на сколько они хорошие. можно наверное принимать 2 параметра. Старая комната и новая
    //и в старой отключать все, в новой включать и тут же менять значение _room на новое. Перепишу потом, хотя если вы знаете способ лучше...

    void CameraRoom(Room newRoom)
    {
        //print(newRoom.ToString());
        if (newRoom == Room.Kitchen)
        {
            _camera1.enabled = true;
            var audio = _camera1.GetComponent<AudioListener>();
            audio.enabled = true;
            _camera2.enabled = false;
            audio = _camera2.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera3.enabled = false;
            audio = _camera3.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera4.enabled = false;
            audio = _camera4.GetComponent<AudioListener>();
            audio.enabled = false;
        }
        if (newRoom == Room.Gosti)
        {
            _camera1.enabled = false;
            var audio = _camera1.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera2.enabled = true;
            audio = _camera2.GetComponent<AudioListener>();
            audio.enabled = true;
            _camera3.enabled = false;
            audio = _camera3.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera4.enabled = false;
            audio = _camera4.GetComponent<AudioListener>();
            audio.enabled = false;
        }
        if (newRoom == Room.Sklad)
        {
            _camera1.enabled = false;
            var audio = _camera1.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera2.enabled = false;
            audio = _camera2.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera3.enabled = true;
            audio = _camera3.GetComponent<AudioListener>();
            audio.enabled = true;
            _camera4.enabled = false;
            audio = _camera4.GetComponent<AudioListener>();
            audio.enabled = false;
        }
        if (newRoom == Room.MyRoom)
        {
            _camera1.enabled = false;
            var audio = _camera1.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera2.enabled = false;
            audio = _camera2.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera3.enabled = false;
            audio = _camera3.GetComponent<AudioListener>();
            audio.enabled = false;
            _camera4.enabled = true;
            audio = _camera4.GetComponent<AudioListener>();
            audio.enabled = true;
        }
    }

}
