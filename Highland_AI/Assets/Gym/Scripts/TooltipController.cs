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
    [SerializeField]
    [Tooltip("What should the width of the tooltip panel be in relation to the width of the screen. I.E. 400 pixels/1920 = 4.8.")]
    public float _SizeRationToWidth = 4.8f;
    [SerializeField]
    [Tooltip("The width to height ratio. I.E. 0.5 means the height is half the width.")]
    public float _HeightToWidthRatio = 0.5f;

    //Member variable
    private IEnumerator m_Coroutine;
    private float m_PanelWidth;
    private float m_PanelHeight;
    //Object references
    private GameObject tooltip;
    private Image tooltipImage;
    private Text tooltipTitle;
    private Text tooltipInfo;
    


    private void Awake()
    {
        m_PanelWidth = Screen.width / _SizeRationToWidth;
        m_PanelHeight = m_PanelWidth * _HeightToWidthRatio;
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

        //float sizingFactor = Screen.width / 1920f;
        tooltip.GetComponent<Image>().color = _backgroundColor;

        tooltipTitle.text = _title;
        tooltipTitle.color = _titleFontColor;
        tooltipInfo.text = _info;
        tooltipInfo.color = _infoFontColor;

        //Size the tooltip based on screen size.
        tooltip.GetComponent<RectTransform>().sizeDelta = new Vector2(m_PanelWidth, m_PanelHeight);
        

        tooltip.transform.position = new Vector3(Mathf.Clamp(eventData.position.x, 0f, Screen.width - tooltip.GetComponent<RectTransform>().sizeDelta.x), eventData.position.y);



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