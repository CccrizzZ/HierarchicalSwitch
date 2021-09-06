using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingNode : MonoBehaviour
{

    [SerializeField] LineRenderer WirePrefab;
    [SerializeField] GameObject FloorNodesParent;
    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;



    public MainEthSwitch MainDevice;
    public AreaNode ParentAreaNode;
    public List<GameObject> AllChildFloorNodes;

    public LineRenderer ConnectedToParentWire;

    public List<LineRenderer> AllWires = new List<LineRenderer>();



    public bool isOn;


    void Start()
    {
        // isOn = true;

        // get the main switch
        MainDevice = GameObject.FindGameObjectWithTag("MainEthSwitch").GetComponent<MainEthSwitch>();

        
        // get all floor node
        if (!FloorNodesParent) return;
        Transform[] allChildren = FloorNodesParent.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.tag == "FloorNode")
            {
                ConnectWire(child);


                AllChildFloorNodes.Add(child.gameObject);
                
                // // set the parent node in child
                child.gameObject.GetComponent<FloorNode>().ParentBuildingNode = GetComponent<BuildingNode>();
                // ParentDevice.AllBuildingNode.Add(child.gameObject);

            }
        }

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


        // set parent wire for building node
        target.GetComponent<FloorNode>().ConnectedToParentWire = Wire;


        AllWires.Add(Wire);



        // target.GetComponent<BuildingNode>().ConnectedWire = Wire;
    
    }



    public void TurnOff()
    {
        GetComponent<Renderer>().material = OffMaterial;
        
        foreach (var item in AllChildFloorNodes)
        {
            item.GetComponent<FloorNode>().TurnOff();
        }


        foreach (var item in AllWires)
        {
            item.GetComponent<EthWire>().SetOffMaterial();
        }


        if (!isOn)return;
        isOn = false;
    }

    public void TurnOn()
    {

        GetComponent<Renderer>().material = OnMaterial;

        foreach (var item in AllChildFloorNodes)
        {
            item.GetComponent<FloorNode>().TurnOn();
        }



        foreach (var item in AllWires)
        {
            item.GetComponent<EthWire>().SetOnMaterial();
        }



        if (isOn) return;
        isOn = true;
    }


    void OnMouseDown() 
    {
        // if (!ParentAreaNode.isOn) return;


        // print(ParentAreaNode);

        if (isOn)
        {
            TurnOff();
            ConnectedToParentWire.GetComponent<EthWire>().SetOffMaterial();
        }
        else
        {
            TurnOn();
            ConnectedToParentWire.GetComponent<EthWire>().SetOnMaterial();
            ParentAreaNode.TurnOnNode();
        }


        ParentAreaNode.UpdateAreaNode();
    }



    public void UpdateBuildingNode()
    {
        int OffCount = 0;
        foreach (var item in AllChildFloorNodes)
        {
            if (!item.GetComponent<FloorNode>().isOn)
            {
                OffCount++;
            }
        }
 

        if (OffCount == AllChildFloorNodes.Count)
        {
            TurnOff();
        }
    }


}
