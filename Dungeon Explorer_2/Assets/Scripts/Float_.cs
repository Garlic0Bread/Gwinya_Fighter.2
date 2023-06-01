using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float_ : MonoBehaviour
{
    [SerializeField] private float Distance;
    [SerializeField] private float down_Distance;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float stickForce = 10f;
    private float _percent = 0.0f;

    private bool isPlayerOnPlatform = false; // Flag to indicate if player is on platform 
    private bool isRising = false; // Flag to indicate if the platform is rising

    [SerializeField] private Direction _direction;
    private Vector3 top, bottom;
    public Transform player;

    // Define direction up and down
    public enum Direction { UP, DOWN };

    void Start()
    {
        top = new Vector3(transform.position.x, transform.position.y + Distance, transform.position.z);
        bottom = new Vector3(transform.position.x, transform.position.y - down_Distance, transform.position.z);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Apply_FloatingEffect();
    }

    
    void Apply_FloatingEffect()
    {
        if (_direction == Direction.UP && _percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(top, bottom, _percent);
        }
        else if (_direction == Direction.DOWN && _percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(bottom, top, _percent);
        }

        if (_percent >= 1)
        {
            _percent = 0.0f;
            if (_direction == Direction.UP)
            {
                _direction = Direction.DOWN;
            }
            else
            {
                _direction = Direction.UP;
            }
        }
    } // Apply the floating effect between the given positions

    private void MovePlayerWithPlatform() //set player to be child of platform so it moves w it, affect transform so player sticks to platform
    {
        if (isRising)
        {
            player.SetParent(this.transform, true);
            Vector3 stickForceVector = -this.transform.up * stickForce;
        }
    }

    private void StartRising()
    {
        isRising = true;
        isPlayerOnPlatform = true;
    } 

    private void StopRising()
    {
        isRising = false;
        isPlayerOnPlatform = false;
        player.SetParent(null); // Unparent the player from the platform
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is on platform");
            if (!isRising)
            {
                StartRising();
            }


            if (isPlayerOnPlatform)
            {
                MovePlayerWithPlatform();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            {
                StopRising();
            }
        }
    }
}
