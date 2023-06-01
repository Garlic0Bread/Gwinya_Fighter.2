using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump_Force;
    [SerializeField] private float additional_Force;
    [SerializeField] private float raycast_JumpDistance;
    [SerializeField] private float jumpSpeedMultiplier = 0.5f;
    [SerializeField] private float DownjumpSpeedMultiplier = 0.5f;
    [SerializeField] private Vector3 topFaceNormal = Vector3.up; // The normal vector of the top face of the cube

    private Rigidbody rb;
    public HealthBar my_Health;
    private int player_Health = 100;

    private bool LeverTriggered = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Jump();
        if (LeverTriggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject leverToOpen = GameObject.FindGameObjectWithTag("Lever");
                leverToOpen.GetComponentInChildren<Animator>().Play("Lever_Open", 0, 0.0f);

                GameObject building1Rise = GameObject.FindGameObjectWithTag("LeverTriggered");
                building1Rise.GetComponent<Animator>().Play("Building1_Rise");
            }
        }
    }
    private void FixedUpdate()
    {
        Move();

        if(rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * jumpSpeedMultiplier, ForceMode.Acceleration);
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * -DownjumpSpeedMultiplier, ForceMode.Acceleration);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Is_Grounded())
            {
                rb.AddForce(Vector3.up * jump_Force, ForceMode.Impulse);
            }          
        }
    }
    private bool Is_Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycast_JumpDistance);
    }
    private void Move()
    {
        float V_Axis = Input.GetAxisRaw("Vertical");
        float H_Axis = Input.GetAxisRaw("Horizontal");

        Vector3 movement = new Vector3(H_Axis, 0, V_Axis) * speed * Time.fixedDeltaTime;
        Vector3 new_Position = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(new_Position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player_Health <= 0)
        {
            player_Health = 100;
            SceneManager.LoadScene(1);
        }

        if (other.gameObject.tag == "Trigger")
        {
            GameObject building2Rise = GameObject.FindGameObjectWithTag("Trigger_Triggered");
            building2Rise.GetComponent<Animator>().Play("Building2_Rise");
        }

        if (other.gameObject.tag == "Fire")
        {
            Debug.Log("Fire works");
            player_Health = player_Health - 5;
            my_Health.Player_Health(player_Health);
        }

        if(other.gameObject.tag == "Lever")
        {
            LeverTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lever")
        {
            LeverTriggered = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Rigidbody disableConstraints_rb = collision.gameObject.GetComponent<Rigidbody>();
            if (Vector3.Dot(collision.contacts[0].normal, topFaceNormal) > 0.9f) //if player is ontop of block, apply constraints
            {
                disableConstraints_rb.constraints = RigidbodyConstraints.FreezePosition;
                Debug.Log("On top");
            }
            if (Vector3.Dot(collision.contacts[0].normal, topFaceNormal) > 0.9f) //if player is ontop of block, apply constraints
            {
                disableConstraints_rb.constraints = RigidbodyConstraints.FreezeRotation;
                Debug.Log("On top");
            }
            else if (disableConstraints_rb != null)
            {
                disableConstraints_rb.constraints = RigidbodyConstraints.None; //if player pushes block, disable constraints
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Rigidbody enableConstraints_rb = collision.gameObject.GetComponent<Rigidbody>();
            if (enableConstraints_rb != null) //when player is no longer pushing block, apply constraints
            {
                enableConstraints_rb.constraints = RigidbodyConstraints.FreezePosition;
                enableConstraints_rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}
