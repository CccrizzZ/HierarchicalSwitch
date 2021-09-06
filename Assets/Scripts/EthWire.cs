using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthWire : MonoBehaviour
{
    [SerializeField] Material OnMat;
    [SerializeField] Material OffMat;

    public void SetOnMaterial()
    {
        GetComponent<Renderer>().material = OnMat;
    }


    public void SetOffMaterial()
    {
        GetComponent<Renderer>().material = OffMat; 
    }
}
