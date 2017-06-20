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
    [Tooltip("The background main color. Leave white for none.")]
    public Color _backgroundColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    [Tooltip("The title font color.")]
    public Color _titleFontColor = new Color(0f, 0f, 0f, 1f);
    [SerializeField]
    [Tooltip("The info font color.")]
    public Color _infoFontColor = new Color(0f, 0f, 0f, 1f);
    [SerializeField]
    [Tooltip("The time delay from the pointer entering this object's box and the tooltips apearing. If set to 0, the tooltips will appear instantly on hover.")]
    public float _delay = 0.75f;

    //Member variable
    private float m_Timer;
    private IEnumerator m_Coroutine;
    //Object references
    private GameObject tooltip;
    private Image tooltipImage;
    private Text tooltipTitle;
    private Text tooltipInfo;
    private GameObject UICanvas;
   


    private void Awake()
    {
        //Getting references of UI tooltip object.
        tooltip = GameObject.FindWithTag("UI").transform.FindChild("Popups").transform.FindChild("Tooltip").gameObject;
        tooltipImage = tooltip.transform.FindChild("image").gameObject.GetComponent<Image>();
        tooltipTitle = tooltip.transform.FindChild("name").gameObject.GetComponent<Text>();
        tooltipInfo = tooltip.transform.FindChild("info").gameObject.GetComponent<Text>();
    }

   

    public void OnPointerEnter(PointerEventData eventData)
    {
        //activate the Tooltip
        

        m_Coroutine = Activate(tooltip);
        StartCoroutine(m_Coroutine);

        float sizingFactor = Screen.width / 800f;
        tooltipImage.color = _backgroundColor;

        tooltipTitle.text = _title;
        tooltipTitle.color = _titleFontColor;
        tooltipInfo.text = _info;
        tooltipInfo.color = _infoFontColor;

        if (_info == "")
        {
            float panelWidth = _title.Length * 9f;
            tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(panelWidth, 25f);
            tooltip.transform.FindChild("info").GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);

        }
        else {
            float panelSize = (Mathf.RoundToInt(_info.Length / 35)) * 15;
            tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, panelSize + 40f);
            tooltip.transform.FindChild("info").GetComponent<RectTransform>().sizeDelta = new Vector2(180f, tooltip.transform.FindChild("info").GetComponent<RectTransform>().sizeDelta.y);

        }

        tooltip.transform.position = new Vector3(Mathf.Clamp(eventData.position.x, 0f, Screen.width - tooltip.GetComponent<RectTransform>().sizeDelta.x * sizingFactor), eventData.position.y);



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