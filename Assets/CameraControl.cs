using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private string MoveAxisVertical = "Vertical";
    private string MoveAxisHorizontal = "Horizontal";
    private string TurnAxisHorizontal = "Mouse Y";
    private string TurnAxisVertical = "Mouse X";

    public float rotateRate = 1;
    public float moveRate = 1;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //float moveAxisX = Input.GetAxis(MoveAxisVertical);
        //float moveAxisZ = Input.GetAxis(MoveAxisHorizontal);
        float turnAxisX = Input.GetAxis(TurnAxisHorizontal);
        float turnAxisY = Input.GetAxis(TurnAxisVertical);

        //ApplyMoveInput(moveAxisX, moveAxisZ);
        ApplyCameraInput(turnAxisX, turnAxisY);
    }

    private void ApplyMoveInput(float moveX, float moveZ)
    {
        //transform.Translate(Vector3.forward * moveX * moveRate);
        //transform.Translate(Vector3.right * moveZ * moveRate);
        //rb.AddForce(transform.forward * moveX * moveRate, ForceMode.Force);
        //rb.AddForce(transform.right * moveZ * moveRate, ForceMode.Force);
    }

    private void ApplyCameraInput(float turnX, float turnY)
    {
        transform.Rotate(turnX * rotateRate, 0, 0);
    }
}
