using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : MonoBehaviour
{
    public int price;
    //public GameObject player;
    public float magnetismStrength = 0.2f;

    //private Transform target;

    // Start is called before the first frame update
    //void Start()
    //{
    //    target = player.transform;
    //}

    public void GetCloser(Transform target)
    {
        float closeness = Mathf.Max(8, 10 - Vector3.Distance(transform.position, target.position));

        if (closeness > 0)
        {
            closeness = Mathf.Pow(closeness, 2);
            float step = magnetismStrength * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step * closeness);
        }
        Debug.Log(closeness);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (ReferenceEquals(collision.gameObject, player))
    //    {
    //        // TODO: call scoreboard function, passing this.gameObject
    //        Debug.Log("Passing this.gameObject to scoreboard");
    //        Destroy(gameObject);
    //    }
    //}
}
