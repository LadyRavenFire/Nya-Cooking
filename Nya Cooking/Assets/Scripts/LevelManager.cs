using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        _leftButton.onClick.AddListener(TaskOnClickLeft);
        _rightButton.onClick.AddListener(TaskOnClickRight);
    }

    void Update()
    {
        _money++;
        UpdateMoney();
    }

    void UpdateMoney()
    {
        _textComponent.text = "Money: " + _money.ToString();
    }

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
