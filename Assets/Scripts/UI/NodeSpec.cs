using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class NodeSpec : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    public string InputName;
    public string InputIP;

    [SerializeField] Text Name;
    [SerializeField] Text IP;
    [SerializeField] Image Background;
    [SerializeField] GameObject HoverIndicator;



    public bool isOn;



    void Start()
    {
        Name.text = InputName;
        IP.text = InputIP;

        if (isOn)
        {
            SetBgOn();
        }
        else
        {
            SetBgOff();
        }
    }


    void SetBgOn()
    {
        Background.color = new Color(0.2f, 0.9f, 0.6f);
    }


    void SetBgOff()
    {
        Background.color = new Color(0.5f, 0.5f, 0.5f);         
    }



    public void OnPointerDown(PointerEventData data)
    {
        
    }
 
    public void OnPointerUp(PointerEventData data)
    {

    }

    public void OnPointerEnter(PointerEventData data)
    {
        HoverIndicator.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData data)
    {
        HoverIndicator.SetActive(false);

    }


}
