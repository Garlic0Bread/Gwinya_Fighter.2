using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_FollowePlayer : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - player.transform.forward * offset;
        transform.LookAt(player.transform);
    }
}
