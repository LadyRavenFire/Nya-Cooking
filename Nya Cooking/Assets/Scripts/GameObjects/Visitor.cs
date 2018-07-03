using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visitor : MonoBehaviour
{
    private Item Wanted;
    private float TimeToBlow;
    // 3 текстуры

	// Use this for initialization
	void Start ()
	{
	    Visitor v1 = GameObject.FindWithTag("Visitor0").GetComponent<Visitor>();
	    var v2 = Instantiate(v1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
