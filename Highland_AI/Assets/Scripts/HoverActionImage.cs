using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverActionImage : MonoBehaviour {

    public GameObject actionOne;
    public GameObject actionTwo;
    public GameObject actionThree;

    public GameObject startNode1;
    public GameObject startNode2;
    public GameObject startNode3;

    float timeOfTravel = 0.6f;
    float currentPosTime = 0;
    float currentAlphaTime = 0;
    float normalizedPosValue;
    float normalizedAlphaValue;

    public void OnEnterImage()
    {
        Debug.Log("Called");
    }

    public void OnExitImage()
    {

    }

    IEnumerator LerpMenu(Vector3 startPos, Vector3 endPos)
    {
        while (currentPosTime <= timeOfTravel)
        {
            //Debug.Log("Position Time" + currentPosTime);
            currentPosTime += Time.deltaTime;
            normalizedPosValue = currentPosTime / timeOfTravel;

            //rectTrans.anchoredPosition = Vector3.Lerp(startPos, endPos, normalizedPosValue);
            yield return null;
        }
        yield return new WaitUntil(() => currentPosTime >= timeOfTravel);

        currentPosTime = 0;
    }
}
