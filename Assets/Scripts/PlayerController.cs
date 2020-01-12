using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float lookSpeed = 2f;

    private Vector3 curLoc;
    private Vector3 prevLoc;

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
        Vector3 forward = (transform.position - mainCamera.position).normalized;
        forward.y = 0;
        Vector3 right = -Vector3.Cross(forward.normalized, transform.up.normalized);

        Vector3 movementDirection = Vector3.zero;
        bool doMove = false;
        bool doRotate = false;

        // Movement for forward and backwards
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection = forward;
            doMove = true;
            doRotate = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection = -forward;
            doMove = true;
            doRotate = false;
        }

        // Movement for left and right
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection = -right;
            doMove = true;
            doRotate = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection = right;
            doMove = true;
            doRotate = true;
        }

        if (doMove)
        {
            transform.position += movementDirection * movementSpeed * Time.deltaTime;
        }
        if (doRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection.normalized), lookSpeed * Time.deltaTime);
        }

        anim.SetBool("Forward", doMove);
    }
}
