using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagneticRange : MonoBehaviour
{
    public Transform magnetTransform;

    [HideInInspector]
    public bool magnetismActive = false;

    private HashSet<Valuable> valuablesInRange;

    void Start()
    {
        valuablesInRange = new HashSet<Valuable>();
    }

    void Update()
    {
        // *FOR DEBUGGING ONLY*
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"({valuablesInRange.Count})");
            foreach (Valuable valuable in valuablesInRange)
            {
                Debug.Log($" - {valuable.price}");
            }
        }

        if (Input.GetMouseButton(0))
        {
            magnetismActive = true;
            if (valuablesInRange.Count > 0)
            {
                GetValuablesInRange().ForEach(v => v.GetCloser(magnetTransform));
            }
        }
        else
        {
            magnetismActive = false;
        }
    }

    public List<Valuable> GetValuablesInRange()
    {
        return valuablesInRange.ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.VALUABLE_TAG))
        {
            Valuable valuable = other.GetComponent<Valuable>();
            if (!valuable)
            {
                Debug.LogError($"{other.name}: Object with Valuable tag but no component!");
            }
            else
            {
                Debug.Log($"Adding valuable with value of {valuable.price}...");
                if (!valuablesInRange.Contains(valuable))
                {
                    valuablesInRange.Add(valuable);
                }
                else
                {
                    Debug.LogError($"Almost tried to add valuable {other.name} but was already in HashSet!");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.VALUABLE_TAG))
        {
            Valuable valuable = other.GetComponent<Valuable>();
            if (!valuable)
            {
                Debug.LogError($"{other.name}: Object with Valuable tag but no component!");
            }
            else
            {
                Debug.Log($"Removing valuable with value of {valuable.price}...");
                if (valuablesInRange.Contains(valuable))
                {
                    valuablesInRange.Remove(valuable);
                } else
                {
                    Debug.LogError($"Almost tried to remove valuable {other.name} but was not in HashSet!");
                }
            }
        }
    }
}
