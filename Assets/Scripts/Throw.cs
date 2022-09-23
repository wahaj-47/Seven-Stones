using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public float power;
    private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = transform.Find("Armature/Bone/Bone.001/Bone.003/Bone.005/Bone.006/Bone.007/LeftHand/Ball").gameObject;
    }

    public void ThrowBall(){
        ball.transform.parent = null;
        ball.AddComponent<Rigidbody>();
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.AddForce((new Vector3(-0.2f,8.5f,2.2597f) - transform.position).normalized * power, ForceMode.Impulse);
    }
}
