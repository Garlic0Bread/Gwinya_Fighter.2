using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private InputActionReference moveActionToUse;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Weapon weapon;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 aimDirection = mousePosition - rb.position;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = aimAngle;


               Vector2 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y));
               transform.position = Vector2.Lerp(transform.position, touchedPos, Time.deltaTime * speed);

                //Vector2 moveDireaction = moveActionToUse.action.ReadValue<Vector2>();
                //transform.Translate(moveDireaction * speed * Time.deltaTime);
            }
        }
    }


    private void FixedUpdate()
    {
        

    }
}
