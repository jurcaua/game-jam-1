using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Player player;
    public float shootSpeed = 120.0f;
    //ParticleSystem shotParticles;
    //int num_val = 1; 

    AudioSource throwAudio;

    void Start()
    {
        //shotParticles = GetComponent<ParticleSystem>();
        player = GetComponent<Player> ();
        //throwAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // If user clicks the left mouse button and has any valuables to shoot
        if (player.hasValuables() && Input.GetMouseButtonDown(0))
        {
            Valuable valuable = player.getNextValuable();
            // Shoot the valuable
            Shoot(valuable);

            Debug.Log("Shot valuable");
        }
    }

    void Shoot(Valuable valuable)
    {   
        // throwAudio.Play();
        //shotParticles.Stop ();
        //shotParticles.Play ();

        // Spawns valuable in front of player
        GameObject shotVal = Instantiate(valuable.gameObject, this.transform.position, this.transform.rotation);

        // Destroy valuable that was on player
        //Destroy(valuable);

        // Add force to valuable to be thrown in direction of player
        shotVal.GetComponent<Rigidbody>().AddForce(transform.forward * shootSpeed);
      
    }
}
