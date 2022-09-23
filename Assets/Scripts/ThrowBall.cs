using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public float force;
    public GameObject ball;
    private GameObject spawnedBall;
    public Vector3 offset;
    private Animator animator;
    public AudioClip laugh;
    private AudioSource laughSound;

    private int throwCount = 0;

    private void Start() {
        animator = GetComponent<Animator>();
        laughSound = gameObject.AddComponent<AudioSource>();
        laughSound.clip = laugh;
        laughSound.spatialBlend = 1;
        laughSound.volume = 0.4f;
    }

    private void Update() {
        if(StateManager.instance.won || StateManager.instance.lost) 
            animator.SetBool("Throw", false);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
            animator.SetBool("Throw", true);
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player")
            animator.SetBool("Throw", false);
    }

    private void SpawnBall(){
        Transform arm = transform.Find("Armature/Bone/Bone.001/Bone.003/Bone.005/Bone.006/Bone.007/LeftHand");
        spawnedBall = Instantiate(ball, transform.position + Vector3.Scale(offset, transform.up), transform.rotation);
        spawnedBall.transform.parent = arm;
        spawnedBall.transform.position = arm.position;
    }

    private void AddForceToBall(){
        throwCount++;
        if(throwCount > 5){
            throwCount = 0;
            if(Random.value > 0.5)
                laughSound.Play();
        }
        spawnedBall.transform.parent = null;
        spawnedBall.tag = "Bullet";
        spawnedBall.AddComponent<FauxGravityBody>();
        spawnedBall.GetComponent<FauxGravityBody>().attractor = this.GetComponent<FauxGravityBody>().attractor;
        Rigidbody rb = spawnedBall.GetComponent<Rigidbody>();
        GetComponent<AudioSource>().Play();
        rb.AddForce(transform.forward.normalized*force, ForceMode.Impulse);
    }
}
