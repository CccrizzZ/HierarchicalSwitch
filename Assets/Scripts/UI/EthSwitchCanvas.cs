using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthSwitchCanvas : MonoBehaviour
{

    [SerializeField] GameObject NodeContainer;
    [SerializeField] GameObject NodeSpecPrefab;
    [SerializeField] GameObject NodeSpecPanel;

    public delegate void voidDelegate();
    public voidDelegate ButtonPressEvent;

    public void ButtonDownEvent()
    {
        if(ButtonPressEvent == null)return;
        ButtonPressEvent();
    }


    void Start()
    {
        HidePanel();
    
    }


    public void ShowPanel()
    {
        NodeSpecPanel.SetActive(true);
    }


    public void HidePanel()
    {
        NodeSpecPanel.SetActive(false);

        foreach (Transform child in NodeContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // delete delegate 
        ButtonPressEvent -= ButtonPressEvent;

    }


    void AddNewNode(GameObject go)
    {

        var NewNode =  Instantiate(NodeSpecPrefab);

        NodeSpec spec = NewNode.GetComponent<NodeSpec>();
        spec.InputIP = GenerateRandomIP();

        if (go.TryGetComponent(out AreaNode A))
        {
            spec.InputName = A.name;
            spec.isOn = A.isOn;
        }
        else if (go.TryGetComponent(out BuildingNode B))
        {
            spec.InputName = B.name;
            spec.isOn = B.isOn;
        }
        else if (go.TryGetComponent(out FloorNode F))
        {
            spec.InputName = F.name;
            spec.isOn = F.isOn;
        }



        NewNode.transform.parent = NodeContainer.transform;

    }

    
    string GenerateRandomIP()
    {
        return Random.Range(192,224) + "." + Random.Range(0, 256) + "." + Random.Range(0, 256) + "." + Random.Range(0, 256);
    }


    public void LoadPanel(List<GameObject> GoList)
    {

        foreach (var item in GoList)
        {
            AddNewNode(item);
        }
    }


}
