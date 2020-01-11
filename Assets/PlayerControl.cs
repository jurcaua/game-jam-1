using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    private string MoveAxisVertical = "Vertical";
    private string MoveAxisHorizontal = "Horizontal";
    private string TurnAxisHorizontal = "Mouse Y";
    private string TurnAxisVertical = "Mouse X";

    public float rotateRate = 1;
    public float moveRate = 1;

    Animator anim;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveAxisX = Input.GetAxis(MoveAxisVertical);
        float moveAxisZ = Input.GetAxis(MoveAxisHorizontal);
        float turnAxisX = Input.GetAxis(TurnAxisHorizontal);
        float turnAxisY = Input.GetAxis(TurnAxisVertical);

        ApplyMoveInput(moveAxisX, moveAxisZ);
        ApplyTurnInput(turnAxisX, turnAxisY);
    }

    private void ApplyMoveInput(float moveX, float moveZ)
    {
        transform.Translate(Vector3.forward * moveX * moveRate, Space.Self);
        transform.Translate(Vector3.right * moveZ * moveRate, Space.Self);
        anim.SetFloat("Forward", Math.Abs(moveZ) + Math.Abs(moveX));

        // Press shift to run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveRate *= 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveRate /= 1.5f;
        }

       // rb.AddForce(transform.forward * moveX * moveRate, ForceMode.Force);
       // rb.AddForce(transform.right * moveZ * moveRate / 3, ForceMode.Force);
    }

    private void ApplyTurnInput(float turnX, float turnY)
    {
        transform.Rotate(0, turnY * rotateRate, 0);
    }
}
