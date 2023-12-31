using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Fusion;

public class GetPlayerCamera : MonoBehaviour
{
    private void Start()
    {
        NetworkObject thisObject = GetComponent<NetworkObject>();

        if (thisObject.HasStateAuthority)
        {
            
            GameObject normalCamera = GameObject.Find("Normal 3rdPerson");
            GameObject aimCamera = GameObject.Find("Aim 3rdPerson");

            normalCamera.GetComponent<CinemachineVirtualCamera>().Follow = transform;
            aimCamera.GetComponent<CinemachineVirtualCamera>().Follow = transform;

            GetComponent<PlayerController>().enabled = true;
            GetComponent<Animator>().enabled = true;

        }
    }
}
