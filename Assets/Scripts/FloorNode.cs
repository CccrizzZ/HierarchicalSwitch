using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorNode : MonoBehaviour
{
    

    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;

    public BuildingNode ParentBuildingNode;

    public List<GameObject> AllChildNodes;


    public LineRenderer ConnectedToParentWire;



    public bool isOn;

    
    void Start()
    {
        
    }



    void OnMouseDown() 
    {
        if (!ParentBuildingNode.isOn)return;
        
        if (isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    
    }






    public void TurnOn()
    {
        GetComponent<Renderer>().material = OnMaterial;

        ConnectedToParentWire.GetComponent<EthWire>().SetOnMaterial();

        if (isOn) return;
        isOn = true;
    
    }

    public void TurnOff()
    {
        GetComponent<Renderer>().material = OffMaterial;
        
        ConnectedToParentWire.GetComponent<EthWire>().SetOffMaterial();


        if (!isOn) return;
        isOn = false;

        // ParentBuildingNode.UpdateBuildingNode();

    }
}
