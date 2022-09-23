using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSounds : MonoBehaviour
{
    public AudioClip whoosh, explosion;
    private AudioSource whooshSound, explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        whooshSound = gameObject.AddComponent<AudioSource>();
        whooshSound.clip = whoosh;
        whooshSound.volume = 0.15f;
        whooshSound.Play();

        explosionSound = gameObject.AddComponent<AudioSource>();
        explosionSound.clip = explosion;
        explosionSound.volume = 0.25f;
    }

    private void OnCollisionEnter(Collision other) {
        gameObject.GetComponent<Rigidbody>().mass = 15;
        if(other.gameObject.tag == "Environment"){
            whooshSound.Pause();
            explosionSound.Play();
        }
    }


}
