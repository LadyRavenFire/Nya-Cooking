using UnityEngine;
using UnityEngine.UI;


//я решил не перемещать камеру, а делать 4 камеры и менять их, т.к. просто перемещать main camera нельзя. 
//возможен вариант с тем, что бы прикрепить камеру к пустому объекту, но тогда нужно запоминать позиции, в которые нужно переместить камеру... ну, как вариант
//нужно будет перенести в другой скрипт все связанное с камерой, ибо тут будут храниться данные, которые я буду загружать и выгружать в игру при начале уровня.
//главным минусом пока что то, что я компоненты не получаю кодом, а в инспектор переношу. Стоит подумать о том, как начать их получать, но вводить 7 тегов для кнопок и камер это убого
//на самом деле сделана довольно большая работа над кнопками и текстом, теперь кнопки, текст, ТЕКСТУРКИ В ИГРЕ, масштабируются относительно разрешения. 
//Осталось сделать ещё какой - то сложный принцип работы и масштабирования для инвентаря.... 

public class CamerasManager : MonoBehaviour {

    private Button _leftButton;
    private Button _rightButton;

    private Camera _kitchenCamera;
    private Camera _guestRoomCamera;
    private Camera _storageCamera;
    private Camera _myRoomCamera;

    [SerializeField] private Room _room;

    //тут будут номера комнат храниться, в которых может быть игрок. Удобно и можно расширять в зависимости от ивентов(для ивентов?).
    private enum Room
    {
        Kitchen,
        GuestRoom,
        Storage,
        MyRoom
    }

    void Start()
    {
        //тут пример поиска по имени
        _leftButton = GameObject.Find("LeftChangeCameraButton").GetComponent<Button>();
        _rightButton = GameObject.Find("RightChangeCameraButton").GetComponent<Button>();

        _kitchenCamera = GameObject.Find("KitchenCamera").GetComponent<Camera>();
        _guestRoomCamera = GameObject.Find("GuestRoomCamera").GetComponent<Camera>();
        _storageCamera = GameObject.Find("StorageCamera").GetComponent<Camera>();
        _myRoomCamera = GameObject.Find("MyRoomCamera").GetComponent<Camera>();

        CameraRoom(_room);
        //Сюда подцепляем кнопки и ждем клика, заодно говорим, какой ивент случится при клике на кнопку.
        _leftButton.onClick.AddListener(TaskOnClickLeft);
        _rightButton.onClick.AddListener(TaskOnClickRight);
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
            _room = Room.Storage;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.Storage)
        {
            _room = Room.GuestRoom;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.GuestRoom)
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
            _room = Room.GuestRoom;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.GuestRoom)
        {
            _room = Room.Storage;
            CameraRoom(_room);
            return;
        }
        if (_room == Room.Storage)
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
            _kitchenCamera.enabled = true;
            var audio = _kitchenCamera.GetComponent<AudioListener>();
            audio.enabled = true;
            _guestRoomCamera.enabled = false;
            audio = _guestRoomCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _storageCamera.enabled = false;
            audio = _storageCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _myRoomCamera.enabled = false;
            audio = _myRoomCamera.GetComponent<AudioListener>();
            audio.enabled = false;
        }
        if (newRoom == Room.GuestRoom)
        {
            _kitchenCamera.enabled = false;
            var audio = _kitchenCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _guestRoomCamera.enabled = true;
            audio = _guestRoomCamera.GetComponent<AudioListener>();
            audio.enabled = true;
            _storageCamera.enabled = false;
            audio = _storageCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _myRoomCamera.enabled = false;
            audio = _myRoomCamera.GetComponent<AudioListener>();
            audio.enabled = false;
        }
        if (newRoom == Room.Storage)
        {
            _kitchenCamera.enabled = false;
            var audio = _kitchenCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _guestRoomCamera.enabled = false;
            audio = _guestRoomCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _storageCamera.enabled = true;
            audio = _storageCamera.GetComponent<AudioListener>();
            audio.enabled = true;
            _myRoomCamera.enabled = false;
            audio = _myRoomCamera.GetComponent<AudioListener>();
            audio.enabled = false;
        }
        if (newRoom == Room.MyRoom)
        {
            _kitchenCamera.enabled = false;
            var audio = _kitchenCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _guestRoomCamera.enabled = false;
            audio = _guestRoomCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _storageCamera.enabled = false;
            audio = _storageCamera.GetComponent<AudioListener>();
            audio.enabled = false;
            _myRoomCamera.enabled = true;
            audio = _myRoomCamera.GetComponent<AudioListener>();
            audio.enabled = true;
        }
    }
}
