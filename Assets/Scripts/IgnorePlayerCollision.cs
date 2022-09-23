using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayerCollision : MonoBehaviour
{
    public Collider player;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(player, gameObject.GetComponent<Collider>());
    }

}
