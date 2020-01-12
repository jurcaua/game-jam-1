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

    private Vector3 curPosition;
    private Vector3 prevPosition;

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
        prevPosition = curPosition;
        curPosition = transform.position;

        float moveAxisX = Input.GetAxis(MoveAxisVertical);
        float moveAxisZ = Input.GetAxis(MoveAxisHorizontal);
        //float turnAxisX = Input.GetAxis(TurnAxisHorizontal);
        //float turnAxisY = Input.GetAxis(TurnAxisVertical);

        if (Input.GetKey(KeyCode.A))
            curPosition.x -= 1 * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.D))
            curPosition.x += 1 * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.W))
            curPosition.z += 1 * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.S))
            curPosition.z -= 1 * Time.fixedDeltaTime;

        transform.position = curPosition;

        //if (Input.GetKey(KeyCode.A))
        //    curPosition.x -= 1 * Time.fixedDeltaTime;
        //if (Input.GetKey(KeyCode.D))
        //    curPosition.x += 1 * Time.fixedDeltaTime;
        //if (Input.GetKey(KeyCode.W))
        //    curPosition.z += 1 * Time.fixedDeltaTime;
        //if (Input.GetKey(KeyCode.S))
        //    curPosition.z -= 1 * Time.fixedDeltaTime;

        //transform.position = curPosition;

        ApplyMoveInput(moveAxisX, moveAxisZ);
        ApplyTurnInput(moveAxisX, moveAxisZ);
    }

    private void ApplyMoveInput(float moveX, float moveZ)
    {
        //transform.Translate(Vector3.forward * moveX * moveRate, Space.Self);
        //transform.Translate(Vector3.right * moveZ * moveRate, Space.Self);
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

        rb.AddForce(transform.forward * moveX * moveRate, ForceMode.Force);
        rb.AddForce(transform.right * moveZ * moveRate / 3, ForceMode.Force);
    }

    private void ApplyTurnInput(float moveX, float moveZ)
    {
        //Vector3 compositeDirection = (transform.forward * moveX * moveRate + transform.right * moveZ * moveRate).normalized;
        //Vector3 compositeDirection = (transform.right * moveZ).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(compositeDirection, transform.up);
        //transform.rotation = lookRotation;
        //Debug.Log(lookRotation);

        Vector3 lookRot = Quaternion.LookRotation(transform.position - prevPosition).eulerAngles;
        lookRot.x = 0f;
        lookRot.z = 0f;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(lookRot), Time.fixedDeltaTime * rotateRate);
        // ***
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(compositeDirection), rotateRate * Time.deltaTime);
        //Vector3 currentRotation = transform.rotation.eulerAngles;

        //transform.rotation = Quaternion.Euler(new Vector3(currentRotation.x, currentRotation.y + turnY * rotateRate * Time.deltaTime, currentRotation.z));
    }
}
