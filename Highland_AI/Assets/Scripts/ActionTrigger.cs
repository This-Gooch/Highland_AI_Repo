using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour {

    public GameObject sourceUnit;
    Action_Immediate actionRef;
    BattleManager battleMang;

    private void Start()
    {
        actionRef = GetComponent<Action_Immediate>();
        battleMang = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
    public void StartTrigger()
    {
        StartCoroutine(TriggerActions());
    }

    IEnumerator TriggerActions()
    {
        Debug.Log("Start Trigger");
        if (actionRef as Action_Immediate != null)
        {
            Debug.Log("Has Immidiate Action");
            if (actionRef.utilityCost <= sourceUnit.GetComponent<UnitStats>().utility)
            {
                if (actionRef.needsTarget)
                {
                    battleMang.activeAction = transform.gameObject;
                    battleMang.selectingTarget = true;
                    while (actionRef.targetUnit == null)
                    {
                        yield return null;
                    }
                    battleMang.selectingTarget = false;
                    DiscardUsedAction();
                }
                else
                {
                    DiscardUsedAction();
                }
            }
            else
            {
                Debug.Log("Not enough Utility Points");
            }
        }
        //battleMang.CheckLethal();
    }

    void DiscardUsedAction()
    {
        sourceUnit.GetComponent<UnitStats>().utility -= actionRef.utilityCost;
        //actionRef.ExecuteAction();
        battleMang.activeAction = null; 
        actionRef.targetUnit = null;
        sourceUnit.GetComponent<ActionManager>().discardDeck.Add(transform.gameObject);
        sourceUnit.GetComponent<ActionManager>().inHand.Remove(transform.gameObject);
        transform.position = battleMang.discardLoc.transform.position;

    }

    public void DiscardUnusedAction()
    {
        sourceUnit.GetComponent<UnitStats>().utility += actionRef.utilityGain;
        sourceUnit.GetComponent<ActionManager>().discardDeck.Add(transform.gameObject);
        sourceUnit.GetComponent<ActionManager>().inHand.Remove(transform.gameObject);
        transform.position = battleMang.discardLoc.transform.position;
    }
}
