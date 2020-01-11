using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : MonoBehaviour
{
    public int price;
    public GameObject player;
    public float magnetism_strength = 0.2f;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float closeness = Mathf.Max(0, 15 - Vector3.Distance(transform.position, target.position));

        if (closeness > 0)
        {
            closeness = Mathf.Pow(closeness, 2);
            float step = magnetism_strength * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step * closeness);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.ReferenceEquals(collision.gameObject, player))
        {
            // TODO: call scoreboard function, passing this.gameObject
            Debug.Log("Passing this.gameObject to scoreboard");
            Destroy(this.gameObject);
        }
    }
}
