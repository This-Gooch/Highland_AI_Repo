using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

    public List<GameObject> masterDeckList = new List<GameObject>();
    public List<GameObject> inHand = new List<GameObject>();
    public List<GameObject> stackDeck = new List<GameObject>();
    public List<GameObject> discardDeck = new List<GameObject>();

    public GameObject handParent;

    //public List<Text> actionTexts = new List<Text>();

    int inHandRef;
    public bool selectingTarget;
    private void Start()
    {
        foreach(GameObject action in masterDeckList)
        {
            action.GetComponent<ActionTrigger>().sourceUnit = transform.gameObject;
            GameObject allyAction = Instantiate(action);
            stackDeck.Add(allyAction);
        }
        /*
        foreach(GameObject action in stackDeck)
        {
            action.GetComponent<Action_Immediate>().sourceUnit = GameObject.Find("AllyOne").transform.gameObject;
        }
        */
        ShuffleStack();
    }

    int maxHandSize = 3;
    int currentHandSize;
    public void DrawActions()
    {
        currentHandSize = inHand.Count;

        for(int i = 0; i < maxHandSize - currentHandSize; i++)
        {
            if (stackDeck.Count > 0)
            {
                inHand.Add(stackDeck[0]);
                stackDeck.RemoveAt(0);
            }
        }
        // Set Physical Location of Cards on the Screen. This will probably be scrapped at some point for something better
        float handLocation = 0;
        foreach (GameObject action in inHand)
        {
            action.transform.SetParent(handParent.transform);
            action.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0 - handLocation, 0);
            handLocation += 65;
        }
        handLocation = 0;

    }

    public void ShuffleStack()
    {
        for(int i = 0; i < stackDeck.Count; i++)
        {
            GameObject temp = stackDeck[i];
            int randomIndex = Random.Range(i, stackDeck.Count);
            stackDeck[i] = stackDeck[randomIndex];
            stackDeck[randomIndex] = temp;
        }
    }

    public void RestockStack()
    {
        foreach (GameObject action in discardDeck)
        {
            stackDeck.Add(action);
        }
        discardDeck.Clear();
        ShuffleStack();
    }
}
