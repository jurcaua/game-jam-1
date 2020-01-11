using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticRange : MonoBehaviour
{
    [HideInInspector]
    //public List<Valuable> valuablesInRange;

    void Start()
    {
        //valuablesInRange = new List<Valuable>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.VALUABLE_TAG))
        {
            Debug.Log("Valuable in range!");
        } else
        {
            Debug.Log($"Collided with object with tag {other.tag}.");
        }
    }
}
