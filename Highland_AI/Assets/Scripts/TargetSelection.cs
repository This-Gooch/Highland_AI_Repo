using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour {

    BattleManager battleMang;

	// Use this for initialization
	void Start ()
    {
        battleMang = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }
	
	public void SetAsTarget()
    {
        if(battleMang.selectingTarget)
        {
            battleMang.activeAction.GetComponent<Action_Immediate>().targetUnit = transform.gameObject;
        }
    }
}
