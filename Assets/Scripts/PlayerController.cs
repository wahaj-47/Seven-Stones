using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private bool shouldRun = false;
    public float runSpeed;
    private float moveSpeed;
    public float rotationSpeed;
    private float moveDirection;
    private Rigidbody rb;
    private Animator animator;
    private int collisionCount = 0;
    public AudioClip fall, hurt, collide;
    private AudioSource fallAudioSource, hurtAudioSource, collisionAudioSource;
    public Animator comicAnimator;

    void Awake() {
        rb = GetComponent<Rigidbody>();  
        animator = GetComponent<Animator>();
        moveSpeed = runSpeed;
        fallAudioSource = gameObject.AddComponent<AudioSource>();
        fallAudioSource.clip = fall;
        fallAudioSource.volume = 0.25f;
        hurtAudioSource = gameObject.AddComponent<AudioSource>();
        hurtAudioSource.clip = hurt;
        hurtAudioSource.volume = 0.25f;
        collisionAudioSource = gameObject.AddComponent<AudioSource>();
        collisionAudioSource.clip = collide;
        collisionAudioSource.volume = 0.25f;
    }

    public void ToggleMovement() {
        shouldRun = !shouldRun;
        Run();
    }

    public void Stop() {
        moveSpeed = 0;
        animator.SetBool("Run", false);
        animator.SetBool("Win", true);
    }

    private void Run(){
        transform.Find("FireTrail").gameObject.SetActive(false);
        if(moveSpeed != 0){
            moveSpeed = runSpeed;
            animator.SetBool("Run", true);
        }
    }

    private void Collide(){
        if(moveSpeed != 0){
            collisionCount++;
            moveSpeed = -0.1f * runSpeed;
            animator.SetTrigger("Collision");
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            if(collisionCount > 4) {hurtAudioSource.Play(); collisionCount=0;}
            collisionAudioSource.Play();
            CancelInvoke("Run");
        }
    } 

    private void Fall(Collision other){
        collisionCount++;
        shouldRun = false;
        moveSpeed = 0;
        animator.SetTrigger("Fall");
        if(!StateManager.instance.lost){
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            fallAudioSource.Play();
            StateManager.instance.Lose();
        }
        if(collisionCount > 4) {hurtAudioSource.Play(); collisionCount=0;}
        transform.rotation = Quaternion.Slerp(transform.rotation, other.transform.rotation, 0.625f);
    }

    private void SpeedUp(Collider other){
        comicAnimator.SetTrigger("Speed");
        AudioManager.instance.Play("Powerup");
        other.gameObject.SetActive(false);
        transform.Find("FireTrail").gameObject.SetActive(true);
        moveSpeed = runSpeed * 2;
        Invoke("Run", 7);
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Obstacle"){
            Collide();
        }
        if(other.gameObject.tag == "Bullet"){
            Fall(other);
        }
        if(other.gameObject.tag == "Stone"){
            AudioManager.instance.Play("Collect");
            other.gameObject.SetActive(false);
            string index = other.gameObject.name.Split('-')[1];
            StateManager.instance.CollectStone(int.Parse(index));
        }
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Powerup"){
            if(moveSpeed == runSpeed){
                SpeedUp(other);
            }
        }
    }

    void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal") * rotationSpeed;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.touches[0];
            moveDirection = touch.deltaPosition.x * 0.5f;
        }

    }

    void FixedUpdate() {
        if(shouldRun){
            transform.Translate(0,0,moveSpeed);
            transform.Rotate(0,moveDirection,0);
        }
    }
}
