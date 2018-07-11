using UnityEngine;
using UnityEngine.UI;


// Этот скрипт для перемещения камеры из 1 комнаты в другую. В данном случае комнат 4
// Это комната ГГ, комната готовки, склад, комната приема посетителей

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
        _leftButton.onClick.AddListener(LeftButton_Click);
        _rightButton.onClick.AddListener(RightButton_Click);
    }

    //ивент на левую кнопку мыши
    void LeftButton_Click()
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
    void RightButton_Click()
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
