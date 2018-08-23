using UnityEngine;

public class texturesize : MonoBehaviour
{
    public Texture2D Texture2D;

	// Use this for initialization
	void Start ()
	{
	    float aspect = (float)Texture2D.width / Texture2D.height;


	    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,
	        gameObject.transform.localScale.x / aspect, gameObject.transform.localScale.z);

	}

}
