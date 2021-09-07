using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorNode : MonoBehaviour
{
    

    [SerializeField] Material OnMaterial;
    [SerializeField] Material OnGlowingMaterial;
    [SerializeField] Material OffGlowingMaterial;
    [SerializeField] Material OffMaterial;

    public BuildingNode ParentBuildingNode;

    // public List<GameObject> AllChildNodes;


    public LineRenderer ConnectedToParentWire;



    public bool isOn;

    
    void Start()
    {

    }



    void OnMouseDown() 
    {
        // if (!ParentBuildingNode.isOn)return;

        // print(ParentBuildingNode);
        // if (isOn)
        // {
        //     TurnOff();
        //     ParentBuildingNode.UpdateBuildingNode();


        // }
        // else
        // {
        //     TurnOn();

        //     if (!ParentBuildingNode.isOn) ParentBuildingNode.TurnOnNode();
        // }
    
    }



    public bool ToggleNode()
    {
        if (isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }

        return isOn;

    }


    public void TurnOn()
    {
        GetComponent<Renderer>().material = OnMaterial;

        ConnectedToParentWire.GetComponent<EthWire>().SetOnMaterial();
        ParentBuildingNode.GetComponent<BuildingNode>().TurnOnNode();

        if (isOn) return;
        isOn = true;

    }

    public void TurnOff()
    {
        GetComponent<Renderer>().material = OffMaterial;
        
        ConnectedToParentWire.GetComponent<EthWire>().SetOffMaterial();


        if (!isOn) return;
        isOn = false;
    }


    // void OnMouseEnter() 
    // {
    //     if (isOn)
    //     {
    //         GetComponent<Renderer>().material = OnGlowingMaterial;
    //     }
    //     else
    //     {
    //         GetComponent<Renderer>().material = OffGlowingMaterial;
    //     }
    // }
    
    // void OnMouseExit() 
    // {
    //     if (isOn)
    //     {
    //         GetComponent<Renderer>().material = OnMaterial;
    //     }
    //     else
    //     {
    //         GetComponent<Renderer>().material = OffMaterial;
    //     }
    // }
}
