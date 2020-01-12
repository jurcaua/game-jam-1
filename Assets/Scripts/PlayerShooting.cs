using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// built off: https://github.com/FinnbarrOC/game-jam-1/commit/81189f967dfcd5b48b7cf4d7e7f6993f89d7027d#diff-42a27e28db28a42acb161494f04da444

public class PlayerShooting : MonoBehaviour
{
    public float shootSpeed = 120.0f;
    public Transform shootFrom;

    private PlayerManager playerManager;
    //ParticleSystem shotParticles;
    //int num_val = 1; 

    AudioSource throwAudio;

    void Start()
    {
        //shotParticles = GetComponent<ParticleSystem>();
        playerManager = GetComponent<PlayerManager>();
        //throwAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // If user clicks the left mouse button and has any valuables to shoot
        if (playerManager.HoldingValuables() && Input.GetMouseButtonDown(0))
        {
            Valuable valuable = playerManager.GetNextValuable();
            Shoot(valuable);
        }
    }

    void Shoot(Valuable valuable)
    {
        // throwAudio.Play();
        //shotParticles.Stop ();
        //shotParticles.Play ();

        // re-enable valuable physics before spawning it + reset parent
        valuable.gameObject.tag = Constants.TAG_UNTAGGED;
        valuable.gameObject.layer = LayerMask.NameToLayer(Constants.LAYER_PROJECTILE);
        valuable.ResetParent();
        valuable.EnablePhysics();

        // Spawns valuable in front of player
        GameObject shotVal = Instantiate(valuable.gameObject, shootFrom.position, transform.rotation);

        // Destroy valuable that was on player now that we no longer need it
        Destroy(valuable.gameObject);

        // Add force to valuable to be thrown in direction of player
        shotVal.GetComponent<Rigidbody>().AddForce(transform.forward * shootSpeed);

    }
}
