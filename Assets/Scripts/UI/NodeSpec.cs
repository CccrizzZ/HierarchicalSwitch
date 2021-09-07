using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class NodeSpec : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    public string InputName;
    public string InputIP;

    public GameObject TargetNode;

    [SerializeField] Text Name;
    [SerializeField] Text IP;
    [SerializeField] Image Background;
    [SerializeField] GameObject HoverIndicator;
    [SerializeField] GameObject ActiveIndicatorPrefab;

    GameObject ActiveIndicator;






    public delegate bool boolDelegate();
    public boolDelegate ToggleCertainNode;

    public void ToggleNodeEvent()
    {
        if(ToggleCertainNode == null)return;
        isOn = ToggleCertainNode();

    }


    public bool isOn;



    void Start()
    {
        Name.text = InputName;
        IP.text = InputIP;

        UpdateNodeSpec();
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
        ToggleNodeEvent();
        UpdateNodeSpec();
    }
 
    public void OnPointerUp(PointerEventData data)
    {

    }

    

    public void OnPointerEnter(PointerEventData data)
    {
        HoverIndicator.SetActive(true);
     
     


        ActiveIndicator = Instantiate(ActiveIndicatorPrefab);
        ActiveIndicator.transform.SetParent(transform);
        // ActiveIndicator.transform.SetParent(this.transform);
        ActiveIndicator.transform.position = Camera.main.WorldToScreenPoint(TargetNode.transform.position);

        
        
    }
    
    public void OnPointerExit(PointerEventData data)
    {
        HoverIndicator.SetActive(false);
 
        Destroy(ActiveIndicator);

        

        

    }


    void OnDestroy() 
    {
        ToggleCertainNode -= ToggleCertainNode;
    }


    public void UpdateNodeSpec()
    {
        if (isOn)
        {
            SetBgOn();
        }
        else
        {
            SetBgOff();
        }
    }

}
