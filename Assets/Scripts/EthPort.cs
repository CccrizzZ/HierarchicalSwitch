using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthPort : MonoBehaviour
{
    [SerializeField] LineRenderer WirePrefab;

    [SerializeField] GameObject A_end;
    [SerializeField] GameObject B_end;

    [SerializeField] Material WireOn;
    [SerializeField] Material WireOff;

    public MainEthSwitch ParentDevice;


    LineRenderer Wire;



    public bool isOn;

    void Start()
    {
        // print("eth port Start Called");

        Wire = Instantiate(WirePrefab);
        Wire.SetPosition(0, transform.position);
        Wire.SetPosition(1, new Vector3(B_end.transform.position.x , transform.position.y, transform.position.z));
        Wire.SetPosition(2, new Vector3(B_end.transform.position.x, B_end.transform.position.y, transform.position.z));
        Wire.SetPosition(Wire.positionCount - 1, B_end.transform.position);

        // add the wire to wire container in world
        Wire.transform.parent = GameObject.FindGameObjectWithTag("WireContainer").transform;
        

        // add 
        ParentDevice.AllAreaNode.Add(B_end);


        // set all area nodes parent
        B_end.GetComponent<AreaNode>().MainDevice = ParentDevice;


        if (isOn)
        {
            Wire.material = WireOn;
        }
        else
        {
            Wire.material = WireOff;
        }


    }
    


    void Update()
    {
        // // stick the wire to the socket
        // Wire.SetPosition(Wire.positionCount - 1, B_end.transform.position);
    }


    void OnMouseEnter() 
    {
        
    }

    void OnMouseExit() 
    {
        
    }

    void OnMouseDown() 
    {

        if (!isOn)
        {
            // Wire.gameObject.SetActive(false);
            TurnPortOn();
        }
        else
        {
            // Wire.gameObject.SetActive(true);
            TurnPortOff();
        }
    }




    public void TurnPortOn()
    {
        Wire.GetComponent<EthWire>().SetOnMaterial();

        B_end.GetComponent<AreaNode>().TurnOn();

        if (isOn) return;
        isOn = true;
    }


    public void TurnPortOff()
    {
        Wire.GetComponent<EthWire>().SetOffMaterial();

        // turn off the connected area node
        B_end.GetComponent<AreaNode>().TurnOff();
        if (!isOn) return;
        isOn = false;
    }

    


}
