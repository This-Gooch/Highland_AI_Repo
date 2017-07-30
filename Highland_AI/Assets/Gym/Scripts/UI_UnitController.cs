using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NSGameplay;

public class UI_UnitController : MonoBehaviour {

    public static UI_UnitController instance;

    private Unit m_SelectedUnit;


    #region Editor References
    [SerializeField]
    Text _Name;
    [SerializeField]
    Image _Portrait;

    [SerializeField]
    Text _CurrentHealth;
    [SerializeField]
    Text _MaxHealth;

    [SerializeField]
    Text _Currentdefense;
    [SerializeField]
    Text _BaseDefense;

    [SerializeField]
    Text _CurrentUtility;

    [SerializeField]
    Text _CurrentLevel;

    [SerializeField]
    GameObject _ExhaustStatus;

    [SerializeField]
    GameObject _AbilityHolder;

    [SerializeField]
    GameObject _CardHolder;
    #endregion

    private string m_PortraitImagePath = "Units/Portraits/";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Select(Unit unit)
    {

    }

    public void Deselect()
    {

    }

    public void SetActive(bool isActive)
    { 
        gameObject.SetActive(isActive);
    }

    public void SetActive(bool isActive, Unit unit)
    {
        m_SelectedUnit = unit;
        Refresh(unit);        
        gameObject.SetActive(isActive);
    }

    public void Refresh(Unit unit)
    {
        _Name.text = unit.info.name.ParseName();
        _Portrait.sprite = Resources.Load<Sprite>(m_PortraitImagePath + unit.info.portraitPath);
        _CurrentHealth.text = unit.info.health.ToString();
        _MaxHealth.text = unit.info.baseHealth.ToString();
        _Currentdefense.text = unit.info.defence.ToString();
        _BaseDefense.text = unit.info.baseDefence.ToString();
        _CurrentLevel.text = unit.info.level.ToString();
        _ExhaustStatus.SetActive(unit.info.exhausted);
    }
    /// <summary>
    /// Ability Usage.
    /// </summary>
    public void UseAbilityOne()
    {
        m_SelectedUnit.SelectAbilityOne();
    }
    public void UseAbilityTwo()
    {
        m_SelectedUnit.SelectAbilityTwo();
    }
    public void UseAbilitySpecial()
    {
        m_SelectedUnit.SelectAbilitySpecial();
    }
    public void UseAbilityUltimate()
    {
        m_SelectedUnit.SelectAbilityUltimate();
    }

}
