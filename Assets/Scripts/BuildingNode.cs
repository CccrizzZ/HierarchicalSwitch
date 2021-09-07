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


    EthSwitchCanvas CanvasScript;

    public bool isOn;


    void Start()
    {
        // isOn = true;
        CanvasScript = GameObject.FindGameObjectWithTag("EthSwitchCanvas").GetComponent<EthSwitchCanvas>();

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
                
                // set the parent node in child
                // child.gameObject.GetComponent<FloorNode>().ParentBuildingNode = GetComponent<BuildingNode>();

                child.GetComponent<FloorNode>().ParentBuildingNode = this;


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
        Wire.transform.SetParent(GameObject.FindGameObjectWithTag("WireContainer").transform);


        // set parent wire for building node
        target.GetComponent<FloorNode>().ConnectedToParentWire = Wire;


        AllWires.Add(Wire);
    
    }


    public void TurnOnNode()
    {
        GetComponent<Renderer>().material = OnMaterial;
        
        ConnectedToParentWire.GetComponent<EthWire>().SetOnMaterial();
        if (!ParentAreaNode.isOn) ParentAreaNode.TurnOnNode();


        if (isOn) return;
        isOn = true;

    }


    public void TurnOffNode()
    {
        GetComponent<Renderer>().material = OffMaterial;

        if (!isOn)return;
        isOn = false;
    }
    

    public void TurnOff()
    {
        TurnOffNode();
        
        foreach (var item in AllChildFloorNodes)
        {
            item.GetComponent<FloorNode>().TurnOff();
        }


        foreach (var item in AllWires)
        {
            item.GetComponent<EthWire>().SetOffMaterial();
        }
    }


    public void TurnOn()
    {

        TurnOnNode();

        foreach (var item in AllChildFloorNodes)
        {
            item.GetComponent<FloorNode>().TurnOn();
        }


        foreach (var item in AllWires)
        {
            item.GetComponent<EthWire>().SetOnMaterial();
        }

    }


    void OnMouseDown() 
    {
        // if (!ParentAreaNode.isOn) return;
        
        if(GameObject.FindGameObjectWithTag("Popup"))return;
        
        CanvasScript.ShowPanel();
        CanvasScript.LoadPanel(AllChildFloorNodes);

        // add delegate
        CanvasScript.ButtonPressEvent = ToggleNode;


        // print(ParentAreaNode);



        // ParentAreaNode.UpdateAreaNode();
    }


    public bool ToggleNode()
    {

        if (isOn)
        {
            TurnOff();
            ConnectedToParentWire.GetComponent<EthWire>().SetOffMaterial();
        }
        else
        {
            TurnOn();

        }

        return isOn;
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
            TurnOffNode();
        }
 
    }
}
