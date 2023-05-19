using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private InputActionReference moveActionToUse;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDireaction = moveActionToUse.action.ReadValue<Vector2>();
        transform.Translate(moveDireaction * speed * Time.deltaTime);
    }
}
