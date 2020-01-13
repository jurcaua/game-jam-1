using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Multiplier Values")]
    public float movementSpeed = 20f;
    public float lookSpeed = 3f;

    [Header("External Components")]
    public Transform mainCamera;

    // Used for keeping track of player/camera-relative directions
    private Vector3 forward;
    private Vector3 right;
    
    // Private component references
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.LogError($"Missing animator component for {name}!");
        }
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

        UpdateAnimations(Mathf.Max(Mathf.Abs(verticalAxis), Mathf.Abs(horizontalAxis)));
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

    void UpdateAnimations(float movementSpeed)
    {
        anim.SetFloat("WalkingSpeed", movementSpeed);
    }
}
