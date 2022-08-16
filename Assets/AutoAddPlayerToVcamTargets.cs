using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AutoAddPlayerToVcamTargets : MonoBehaviour
{
    public string Tag = string.Empty;
    [SerializeField] CinemachineVirtualCamera camera;


    void Update()
    {
        if(tag != null)
        {
            var targets = GameObject.FindGameObjectWithTag("Player");
            camera.Follow = targets.transform;
        }
    }
}
