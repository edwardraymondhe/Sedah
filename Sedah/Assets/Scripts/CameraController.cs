using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    private CinemachineVirtualCamera vCam;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                // vCam.Follow = player.transform;
                vCam.LookAt = player.transform;
            }
            return;
        }

        transform.position = player.transform.position + offset;
    }
}
