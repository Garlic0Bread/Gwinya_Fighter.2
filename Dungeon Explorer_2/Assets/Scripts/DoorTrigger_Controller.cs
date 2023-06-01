using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger_Controller : MonoBehaviour
{
    [SerializeField] private Animator Door = null;
    
    private void OnTriggerEnter (Collider other)
    {
     if(other.gameObject.CompareTag("Block"))
       {
          Door.Play("DoorOpen", 0, 0.0f);
       }
    }
}
