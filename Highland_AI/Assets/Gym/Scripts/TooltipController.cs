using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// Simple tooltip display. The UI contains the tooltip object under Popups/tooltip.
/// The tooltip object can be edited/styled.
/// To implement a new tooltip on an object in the scene, simply add this script to the object
/// and make sure it is a valid target for IPointerEnterHandler.
/// The information of the tooltip is edited on each object.
/// </summary>
public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //For in editor use.
    [SerializeField][Tooltip("The title displayed on the Tooltip.")]
    string _title;
    [SerializeField][Tooltip("The main text displayed inside the popup.")]
    public string _info;   
    [SerializeField]
    [Tooltip("The time delay from the pointer entering this object's box and the tooltips apearing. If set to 0, the tooltips will appear instantly on hover.")]
    public float _delay = 0.75f;
    

    //Member variable
    private IEnumerator m_Coroutine;    
    //Object references
    private GameObject tooltip;
    private Text tooltipTitle;
    private Text tooltipInfo;
    


    private void Awake()
    {
        //Getting references of UI tooltip object.
        tooltip = GameObject.FindWithTag("UI").transform.FindChild("Popups").transform.FindChild("Tooltip").gameObject;
        tooltipTitle = tooltip.transform.FindChild("name").gameObject.GetComponent<Text>();
        tooltipInfo = tooltip.transform.FindChild("mask").FindChild("Holder").FindChild("info").gameObject.GetComponent<Text>();
    }

   

    public void OnPointerEnter(PointerEventData eventData)
    {
        //activate the Tooltip
        

        m_Coroutine = Activate(tooltip);
        StartCoroutine(m_Coroutine);


        tooltipTitle.text = _title;
        tooltipInfo.text = _info;

        RectTransform rect = tooltip.GetComponent<RectTransform>();
        tooltip.transform.position = 
            new Vector3( Mathf.Clamp( (eventData.position.x + rect.sizeDelta.x/2f),
                                        0f, Screen.width - rect.sizeDelta.x), 
                                        Mathf.Clamp(eventData.position.y + rect.sizeDelta.y/2f, rect.sizeDelta.y/2f, Screen.height - rect.sizeDelta.y/2f));

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
        StopCoroutine(m_Coroutine);

    }
    IEnumerator Activate(GameObject toActivate)
    {
        Debug.Log(toActivate);
        yield return new WaitForSeconds(_delay);
        toActivate.SetActive(true);
    }


}