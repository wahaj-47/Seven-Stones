using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Vector3 offset = new Vector3(15,15,15);
    public Transform player;

    void FixedUpdate()
    {
            Vector3 desiredPostion = player.position + Vector3.Scale(offset, player.up);
            transform.position = Vector3.Lerp(transform.position, desiredPostion, 0.125f);
            transform.LookAt(player, transform.up);
    }
}
