using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNode : MonoBehaviour
{
    [SerializeField] LineRenderer WirePrefab;
    [SerializeField] GameObject BuildingNodesParent;

    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;


    public MainEthSwitch MainDevice;
    public List<GameObject> AllChildBuildingNodes;

    public List<LineRenderer> AllWires = new List<LineRenderer>();




    public bool isOn;


    void Start()
    {
        // isOn = true;
        // print("Area Node Start Called");

        // get the main switch
        MainDevice = GameObject.FindGameObjectWithTag("MainEthSwitch").GetComponent<MainEthSwitch>();


        // get all building node
        Transform[] allChildren = BuildingNodesParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        { 
            if(child.tag == "BuildingNode")
            {
                ConnectWire(child);


                AllChildBuildingNodes.Add(child.gameObject);
                
                // // set the parent node in child
                child.gameObject.GetComponent<BuildingNode>().ParentAreaNode = GetComponent<AreaNode>();
                // ParentDevice.AllBuildingNode.Add(child.gameObject);

            }
        }



    }

    void Update()
    {
        


    }



    void ConnectWire(Transform target)
    {
        var Wire = Instantiate(WirePrefab);
        Wire.SetPosition(0, transform.position);
        Wire.SetPosition(1, new Vector3(target.transform.position.x , transform.position.y, transform.position.z));
        Wire.SetPosition(2, new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z));
        Wire.SetPosition(Wire.positionCount - 1, target.transform.position);
        

        // add the wire to wire container in world
        Wire.transform.parent = GameObject.FindGameObjectWithTag("WireContainer").transform;
        
        // add wire to array
        AllWires.Add(Wire);

        // set parent wire for building node
        target.GetComponent<BuildingNode>().ConnectedToParentWire = Wire;
    
    }



    public void TurnOffNode()
    {
        isOn = false;
        GetComponent<Renderer>().material = OffMaterial;
    }

    public void TurnOnNode()
    {
        isOn = true;
        GetComponent<Renderer>().material = OnMaterial;
        
    }




    public void TurnOff()
    {
        
        TurnOffNode();

        foreach (var item in AllWires)
        {
            item.GetComponent<EthWire>().SetOffMaterial();
        }

        // turn all nodes off
        foreach (var item in AllChildBuildingNodes)
        {
            // print(item);
            var temp = item.GetComponent<BuildingNode>();
            temp.TurnOff();
        }

        
    }


    public void TurnOn()
    {
        TurnOnNode();

        foreach (var item in AllWires)
        {
            item.GetComponent<EthWire>().SetOnMaterial();
        }

        // turn all nodes off
        foreach (var item in AllChildBuildingNodes)
        {
            // print(item);
            var temp = item.GetComponent<BuildingNode>();
            temp.TurnOn();
        }
    }


    void OnMouseDown() 
    {
        if (!MainDevice.isMainPowerOn) return;
        
        if (isOn)
        {
            TurnOff();
        }    
        else
        {
            TurnOn();
        }

    }



    public void UpdateAreaNode()
    {
        int OffCount = 0;
        foreach (var item in AllChildBuildingNodes)
        {
            if (!item.GetComponent<BuildingNode>().isOn)
            {
                OffCount++;
            }
        }
 

        if (OffCount == AllChildBuildingNodes.Count)
        {
            TurnOffNode();
        }
 
    }


}
