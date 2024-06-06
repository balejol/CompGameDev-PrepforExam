using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Min(0f)]
    [SerializeField]
    private float moveSpeed = 6f;

    [Min(0f)]
    [SerializeField]
    private float rotationSpeed = 60f;

    //[Min(0f)]
    //[SerializeField]
    //private float jumpForce = 5f;

    private Rigidbody rb;
    private Vector2 movementAxis;
    //private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateMovementAxis();
        //Jump();
    }

    private void FixedUpdate()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdateMovementAxis()
    {
        movementAxis.x = Input.GetAxis("Horizontal");
        movementAxis.y = Input.GetAxis("Vertical");
    }

    //private void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    //    {
    //        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //    }
    //}

    //// Ensure player is grounded by checking collisions with the ground
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }

    //    if (collision.gameObject.CompareTag("Cube"))
    //    {
    //        var force = collision.gameObject.transform.localScale;
    //        rb.AddForce(10 * force.y * Vector3.up, ForceMode.Impulse);
    //        Debug.Log("Force = " + force.y * 20);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}

    private void UpdatePosition()
    {
        var positionMovement = transform.forward * (
            movementAxis.y * moveSpeed * Time.deltaTime
        );

        var currentPosition = rb.position;
        var newPosition = currentPosition + positionMovement;

        rb.MovePosition(newPosition);
    }

    private void UpdateRotation()
    {
        var rotationMovement = movementAxis.x * rotationSpeed * Time.deltaTime;
        var currentRotation = rb.rotation.eulerAngles;
        currentRotation.y += rotationMovement;

        var newRotation = Quaternion.Euler(currentRotation);

        rb.MoveRotation(newRotation);
    }

    // gameObject (player) is forced to jump when interacting with another gameObject (cube)
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            var force = other.gameObject.transform.localScale;
            rb.AddForce(10 * force.y * Vector3.up, ForceMode.Impulse);
            Debug.Log("Force = " + force.y * 20);
        }
    }
}
