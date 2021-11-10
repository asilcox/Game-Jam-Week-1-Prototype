using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private float horizontalMovement;
    private float verticalMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();
        transform.Translate(new Vector3(horizontalMovement * moveSpeed * Time.deltaTime, verticalMovement * moveSpeed * Time.deltaTime));
    }

    public void HandleMovement(InputAction.CallbackContext ctx)
    {
        Vector2 val = ctx.ReadValue<Vector2>();
        horizontalMovement = val.x;
        verticalMovement = val.y;
    }

}
