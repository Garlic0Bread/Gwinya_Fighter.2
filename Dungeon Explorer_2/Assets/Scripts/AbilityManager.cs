using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private bool blockIsShot = false;
    private delegate void Ability();
    private Ability[] abilities; //array of abilities

    [SerializeField] private GameObject ShootingBox;
    [SerializeField] private GameObject RisingBox;
    [SerializeField] private GameObject MovingBox;
    [SerializeField] private GameObject Box;

    private int activeAbilityIndex = 0;
    [SerializeField] private int numberOfClicks = 0;
    [SerializeField] private float shootingForce = 10f;

    private void Start()
    {
        abilities = new Ability[]
        {
            PlaceBlockAbility_RisingPlatfromAbility,
            ShootBlockAbility_MovePlatformAbility,
        };
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            abilities[activeAbilityIndex]?.Invoke();
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if(scrollInput > 0f)
        {
            activeAbilityIndex = (activeAbilityIndex + 1) % abilities.Length;
        }
        else if (scrollInput < 0f)
        {
            activeAbilityIndex = (activeAbilityIndex - 1 + abilities.Length) % abilities.Length;
        }
    }

    private void PlaceBlockAbility_RisingPlatfromAbility()
    {
        if (Input.GetMouseButtonDown(0)) //places block where player is pointing and clicking
        {
            Debug.Log("BlockPlaced");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Floor" || hit.transform.tag == "Block" || hit.transform.tag == "Wall" || hit.transform.tag == "ShootingBlock") 
                {
                    Instantiate(Box, hit.point, Quaternion.identity);
                }

                if (hit.transform.tag == "Fire")
                {
                    Instantiate(Box, hit.point, Quaternion.identity);
                }
            }
        }

        else if (Input.GetMouseButtonDown(1)) //places a rising platform from point of clicking
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Floor" || hit.transform.tag == "Block" || hit.transform.tag == "Wall" || hit.transform.tag == "ShootingBlock")
                {
                    Instantiate(RisingBox, hit.point, Quaternion.identity);
                }
            }
        }
    }
    private void ShootBlockAbility_MovePlatformAbility()
    {
        if (Input.GetMouseButtonDown(0)) //shoots a block forward that pushes enemies away from player
        {
            Debug.Log("BlockShot");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject currentBlock = Instantiate(ShootingBox, hit.point, Quaternion.identity);
                Rigidbody rb = currentBlock.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    Vector3 direction = hit.point - transform.position;
                    rb.AddForce(direction.normalized * shootingForce, ForceMode.Impulse);
                }
            }
            blockIsShot = true;
        }

        else if (Input.GetMouseButtonDown(1)) //places a platform that moves forwards and backwards from point of click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Floor" || hit.transform.tag == "Block" || hit.transform.tag == "Wall")
                {
                    Instantiate(MovingBox, hit.point, Quaternion.identity);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(blockIsShot && collision.gameObject.CompareTag("Floor"))
        {
            this.transform.SetParent(collision.transform);
            blockIsShot = false;
        }
    }
}
