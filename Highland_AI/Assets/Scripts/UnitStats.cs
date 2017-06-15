using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UnitStats : MonoBehaviour {

    public int baseHealth;
    public int health;
    public Text healthText;
    public int baseAttack;
    public int attack;
    public Text attackText;
    public int baseDefense;
    public int defense;
    public Text defenceText;
    public int baseUtility;
    public int utility;
    public Text utilityText;

    public int exhaustStateCount;

    private void Start()
    {
        healthText.text = "" + health;
        attackText.text = "" + attack;
        defenceText.text = "" + defense;
        utilityText.text = "" + utility;
    }

    private void Update()
    {
        healthText.text = "" + health;
        defenceText.text = "" + defense;
        utilityText.text = "" + utility;   
    }
}
