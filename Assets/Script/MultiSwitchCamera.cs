using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MultiSwitchCamera : MonoBehaviour
{
    public CinemachineVirtualCamera vCamera;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player")
        {
            vCamera.Priority = 100;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            vCamera.Priority = 5;
        }
    }


}
