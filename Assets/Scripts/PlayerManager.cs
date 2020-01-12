using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.VALUABLE_TAG))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                collision.transform.parent = transform;
                rb.constraints = RigidbodyConstraints.FreezePosition;
                rb.detectCollisions = false;
            }
        }
    }
}
