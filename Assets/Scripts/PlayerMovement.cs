using MidiParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    [SerializeField] private float limitX = 3f;

    Rigidbody rb;
    private DynamicJoystick joystick;
    private float horizontalInput;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        joystick = GameObject.FindObjectOfType<DynamicJoystick>();
    }

    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * SongsManager.velocity * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update()
    {
        horizontalInput = joystick.Horizontal;
        LimitMovement();
    }

    private void LimitMovement()
    {
        Vector3 p = transform.position;
        if (p.x >= limitX)
        {
            p.x = limitX;
            transform.position = p;
        }
        if (p.x <= -limitX)
        {
            p.x = -limitX;
            transform.position = p;
        }
    }
}
