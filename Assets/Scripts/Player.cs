using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public List<Valuable> heldValuables;

    // Start is called before the first frame update
    void Start()
    {
        heldValuables = new List<Valuable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add valuable to the list of held items
    void AddValuable(Valuable valuable)
    {
        heldValuables.Add(valuable);
    }

    void RemoveValuable(Valuable valuable)
    {
        heldValuables.Remove(valuable);
    }


    public bool hasValuables()
    {
        return heldValuables.Count != 0;
    }

    public Valuable getNextValuable()
    {
        // Sanity check to see if list of held valuables is not empty
        if (hasValuables())
        {
            // Retrieve and remove valuable from list of held valuables
            Valuable shotVal = heldValuables[0];
            RemoveValuable(shotVal);
            return shotVal;
        }
        else
        {
            return null;
        }
    }
}