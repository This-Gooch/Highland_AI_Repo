using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NameToText : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = transform.parent.name;
	}

}
