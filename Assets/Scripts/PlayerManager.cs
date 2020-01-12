using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// some logic from: https://github.com/FinnbarrOC/game-jam-1/commit/81189f967dfcd5b48b7cf4d7e7f6993f89d7027d#diff-633fb8fee885300ac044cb4787b27d04

public class PlayerManager : MonoBehaviour
{
    public float blowBackForce;

    private List<Valuable> heldValuables;

    private Rigidbody playerRb;
    private MagneticRange magneticRange;
    
    void Start()
    {
        heldValuables = new List<Valuable>();

        playerRb = GetComponent<Rigidbody>();
        magneticRange = GetComponentInChildren<MagneticRange>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer(Constants.LAYER_PROJECTILE), LayerMask.NameToLayer(Constants.LAYER_PLAYER));
    }

    // Add valuable to the list of held items
    private void AddValuable(Valuable valuable)
    {
        heldValuables.Add(valuable);
    }

    void RemoveValuable(Valuable valuable)
    {
        heldValuables.Remove(valuable);
    }

    public bool HoldingValuables()
    {
        return heldValuables.Count > 0;
    }

    public Valuable GetNextValuable()
    {
        // Sanity check to see if list of held valuables is not empty
        if (HoldingValuables())
        {
            // Retrieve and remove valuable from list of held valuables
            Valuable shotVal = heldValuables[0];
            RemoveValuable(shotVal);
            return shotVal;
        }
        else
        {
            Debug.LogError("Asked for a valuable when holding none! Check HoldingValuables() first!");
            return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG_VALUABLE))
        {
            Valuable valuable = collision.gameObject.GetComponent<Valuable>();
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            MeshFilter meshFilter = collision.gameObject.GetComponent<MeshFilter>();
            if (!valuable)
            {
                Debug.LogError("No Valuable!");
                return;
            }
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

            // update internal list
            AddValuable(valuable);

            // set the parent of the object to be the player now (moves with player now)
            valuable.SetParent(transform);

            // freeze pos and rot, and stop collision (no weird, rigidbody physics + no more colliding with magnetic cone)
            valuable.DisablePhysics();

            // push player back based on the mesh size
            // we only push back the player if they sucked the object to them, otherwise it just sticks
            if (magneticRange.magnetismActive)
            {
                playerRb.AddForce((transform.position - collision.transform.position).normalized * Vector3.Scale(collision.transform.localScale, meshFilter.mesh.bounds.size).magnitude * blowBackForce, ForceMode.Impulse);
            }
        }
    }
}
