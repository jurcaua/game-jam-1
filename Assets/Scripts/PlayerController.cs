using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float lookSpeed = 2f;

    private Vector3 forward;
    private Vector3 right;

    private Rigidbody rb;
    private Animator anim;

    public Transform mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RecalculateRelativeUnitVectors();

        float verticalAxis = Input.GetAxis(Constants.MOVE_AXIS_VERTICAL);
        float horizontalAxis = Input.GetAxis(Constants.MOVE_AXIS_HORIZONTAL);

        bool movementOccured = false;
        Vector3 movementDirection = Vector3.zero;
        if (verticalAxis != 0.0f)
        {
            movementDirection += forward * verticalAxis;
            movementOccured = true;
        }
        if (horizontalAxis != 0.0f)
        {
            movementDirection += right * horizontalAxis;
            movementOccured = true;
        }

        if (movementOccured)
        {
            PerformMovement(movementDirection);
            PerformRotation(movementDirection);
        }

        UpdateAnimations(movementOccured);
    }

    void RecalculateRelativeUnitVectors()
    {
        forward = (transform.position - mainCamera.position).normalized;
        forward.y = 0;
        right = -Vector3.Cross(forward.normalized, transform.up.normalized);
    }

    void PerformMovement(Vector3 movementDirection)
    {
        transform.position += movementDirection.normalized * movementSpeed * Time.deltaTime;
    }

    void PerformRotation(Vector3 movementDirection)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection.normalized), lookSpeed * Time.deltaTime);
    }

    void UpdateAnimations(bool movementOccured)
    {
        anim.SetBool("Walking", movementOccured);
    }
}
