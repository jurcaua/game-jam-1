using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform followTarget;
    public float speed = 1f;

    private Rigidbody r;

    // Start is called before the first frame update
    void Start()
    {
        if (!followTarget)
        {
            Debug.LogError("$Object {name} has no followTarget set!");
        }

        r = GetComponent<Rigidbody>();
        if (!r)
        {
            Debug.LogError($"No Ridigbody for Enemy: {name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowards();
        MoveTowards();
    }

    void RotateTowards()
    {
        transform.LookAt(followTarget);
    }

    void MoveTowards()
    {
        Vector3 towardTarget = (followTarget.position - transform.position).normalized;
        r.AddForce(towardTarget * speed * Time.deltaTime);
    }
}
