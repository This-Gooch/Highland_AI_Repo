using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HoverActionText : MonoBehaviour {

    public Text actionDescription;

    public void TextOn()
    {
        actionDescription.enabled = true;
        gameObject.layer = 8;
    }

    public void TextOff()
    {
        actionDescription.enabled = false;
        gameObject.layer = 5;
    }
}
