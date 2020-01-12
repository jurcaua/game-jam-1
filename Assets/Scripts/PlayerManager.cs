using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float blowBackForce;

    private Rigidbody playerRb;
    private MagneticRange magneticRange;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        magneticRange = GetComponentInChildren<MagneticRange>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.VALUABLE_TAG))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            MeshFilter meshFilter = collision.gameObject.GetComponent<MeshFilter>();
            if (!rb)
            {
                Debug.LogError("No Rigidbody!");
                return;
            }
            if (!meshFilter)
            {
                Debug.LogError("No MeshFilter!");
                return;
            }

            // set the parent of the object to be the player now (moves with player now)
            collision.transform.parent = transform;

            // freeze pos and rot, and stop collision (no weird, rigidbody physics + no more colliding with magnetic cone)
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            rb.detectCollisions = false;

            // push player back based on the mesh size
            // we only push back the player if they sucked the object to them, otherwise it just sticks
            if (magneticRange.magnetismActive)
            {
                playerRb.AddForce((transform.position - collision.transform.position).normalized * Vector3.Scale(collision.transform.localScale, meshFilter.mesh.bounds.size).magnitude * blowBackForce, ForceMode.Impulse);
            }
        }
    }
}
