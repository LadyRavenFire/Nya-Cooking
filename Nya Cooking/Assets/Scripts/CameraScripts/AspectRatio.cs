using UnityEngine;
//This is the script for the resising. (if resolution is not like in the unity) 
//вроде работает
/// u will hate my perfect english
public class AspectRatio : MonoBehaviour {
    //is is heigh/weight of aspect that we want. 16/9 == 1.78 
    [SerializeField] private float _targetAspect = 1.78f;

    void Start()
    {
        ChangeSize();
    }

    void ChangeSize()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleAspect = windowAspect / _targetAspect;
        Camera camera = GetComponent<Camera>();
        if (scaleAspect < 1.0f)
        {
            camera.orthographicSize = camera.orthographicSize / scaleAspect;
        }

    }

}
