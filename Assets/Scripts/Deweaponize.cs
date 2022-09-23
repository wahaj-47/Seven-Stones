using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deweaponize : MonoBehaviour
{
    public Material wornOut;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy"){
            gameObject.tag = "Untagged";
            GetComponent<MeshRenderer>().material = wornOut;
        }
    }
}
