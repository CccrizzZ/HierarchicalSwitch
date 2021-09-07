using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIndicator : MonoBehaviour
{


    [SerializeField] GameObject EthIndicatorPrefab;
    GameObject Indicator;



    void OnMouseEnter()
    {
        if (GameObject.FindGameObjectWithTag("Popup"))return;

        Indicator = Instantiate(EthIndicatorPrefab);
        Indicator.transform.position = transform.position + new Vector3(0,8,0);
    }

    
    void OnMouseExit()
    {
        if (GameObject.FindGameObjectWithTag("Popup"))return;
        // Destroy(Indicator);
        HideIndicator();
        
    }


    void HideIndicator()
    {
        Destroy(Indicator);
    }

}
