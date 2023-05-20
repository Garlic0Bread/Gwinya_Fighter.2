using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Joystick_Movement joystickMovement;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Weapon weapon;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (joystickMovement.joystickVector.y != 0)
        {
            rb.velocity = new Vector2(joystickMovement.joystickVector.x * playerSpeed, joystickMovement.joystickVector.y * playerSpeed);
        }

        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
    }
}
