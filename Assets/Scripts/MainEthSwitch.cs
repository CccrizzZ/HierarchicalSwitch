using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEthSwitch : MonoBehaviour
{

    // connected sockets
    public List<GameObject> AllConnectedSocket;

    // second layer
    public List<GameObject> AllAreaNode;
    
    // third layer
    public List<GameObject> AllBuildingNode;


    public bool isMainPowerOn;

    void Awake() 
    {
        // print("main device awake Called");

        // detect child
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(15,5,35));
        foreach (var item in hitColliders)
        {
            // print(item.name);

            if (item.tag == "EthPort")
            {

                // set parent
                item.GetComponent<EthPort>().ParentDevice = this.GetComponent<MainEthSwitch>();
                
                // add child to list
                AllConnectedSocket.Add(item.gameObject);

            }
        }


    }

    void Start()
    { 
        // print("main device Start Called");

        isMainPowerOn = true;    


    }
    
    



    void OnMouseDown() 
    {
        

        ToggleMainEthDevice();
  
    }



    void ToggleMainEthDevice()
    {
        if (isMainPowerOn)
        {
            // turn off
            isMainPowerOn = false;
            foreach (var item in AllConnectedSocket)
            {
                item.GetComponent<EthPort>().TurnPortOff();
            }
        }
        else
        {
            // turn on
            isMainPowerOn = true;
            foreach (var item in AllConnectedSocket)
            {
                item.GetComponent<EthPort>().TurnPortOn();
            }
        }

    }

}
