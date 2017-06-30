using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Immediate : MonoBehaviour {

    private GameObject sourceUnit;
    public GameObject targetUnit;
    public int inHandLocation;

    public bool isDamage;
    public bool isHeal;
    public bool isReactive;
    public bool needsTarget;

    public int utilityCost;
    public int utilityGain;
    public int damageOutput;
    public int healingOutput;

    BattleManager battleMang;

    void Start ()
    {
        battleMang = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        sourceUnit = GetComponent<ActionTrigger>().sourceUnit;
    }
	/*
	public void SetAction()
    {
        battleMang.act_Immediate.Add(this);
    }

    int dmgCarryOver;
    public void ExecuteAction()
    {
        if(isDamage)
        {
            targetUnit.GetComponent<UnitStats>().defense -= ((damageOutput + sourceUnit.GetComponent<UnitStats>().attack));
            dmgCarryOver = targetUnit.GetComponent<UnitStats>().defense;
            if(targetUnit.GetComponent<UnitStats>().defense < 0)
            {
                targetUnit.GetComponent<UnitStats>().defense = 0;
            }
            dmgCarryOver = dmgCarryOver * -1;
            Debug.Log("Damage to health: " + dmgCarryOver);
            targetUnit.GetComponent<UnitStats>().health -= dmgCarryOver;
            dmgCarryOver = 0;
        }

        if(isHeal)
        {
            sourceUnit.GetComponent<UnitStats>().health += healingOutput;
        }
    }*/
}
