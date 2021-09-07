using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EthSwitchCanvas : MonoBehaviour
{

    [SerializeField] GameObject NodeContainer;
    [SerializeField] GameObject NodeSpecPrefab;
    [SerializeField] GameObject NodeSpecPanel;



    public delegate bool boolDelegate();
    public boolDelegate ButtonPressEvent;

    List<NodeSpec> AllNodeSpec = new List<NodeSpec>();

    public void ButtonDownEvent()
    {
        if(ButtonPressEvent == null)return;
        if(ButtonPressEvent())
        {
            AllNodeSpecOn();
        }
        else
        {
            AllNodeSpecOff();
            
        }
        // UpdateAllNodeSpec();
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

        Destroy(GameObject.FindGameObjectWithTag("NodeIndicator"));

        if(AllNodeSpec.Count == 0) return;
        AllNodeSpec.Clear();
    }


    void AddNewNode(GameObject go)
    {

        var NewNode =  Instantiate(NodeSpecPrefab);
        NodeSpec spec = NewNode.GetComponent<NodeSpec>();
        AllNodeSpec.Add(spec);

        spec.TargetNode = go;
        spec.InputIP = GenerateRandomIP();

        if (go.TryGetComponent(out AreaNode A))
        {
            spec.InputName = A.name;
            spec.isOn = A.isOn;
            spec.ToggleCertainNode = A.ToggleNode;
        }
        else if (go.TryGetComponent(out BuildingNode B))
        {
            spec.InputName = B.name;
            spec.isOn = B.isOn;
            spec.ToggleCertainNode = B.ToggleNode;
        }
        else if (go.TryGetComponent(out FloorNode F))
        {
            spec.InputName = F.name;
            spec.isOn = F.isOn;
            spec.ToggleCertainNode = F.ToggleNode;
            spec.TargetNode = F.transform.GetChild(0).gameObject;
            // spec.TargetNode = null;
            
        }



        NewNode.transform.SetParent(NodeContainer.transform);

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



    void UpdateAllNodeSpec()
    {
        foreach (var item in AllNodeSpec)
        {
            item.UpdateNodeSpec();
        }
    }
    

    void AllNodeSpecOn()
    {
        foreach (var item in AllNodeSpec)
        {
            item.isOn = true;
            item.UpdateNodeSpec();
        }
    }

    void AllNodeSpecOff()
    {
        foreach (var item in AllNodeSpec)
        {
            item.isOn = false;
            item.UpdateNodeSpec();
        }
    }
}
