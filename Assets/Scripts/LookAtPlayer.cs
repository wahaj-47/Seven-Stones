using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
   public Transform player;

   private void FixedUpdate() {
      float distanceToPlane = Vector3.Dot(transform.up, player.position - transform.position);
      Vector3 planePoint = player.position - transform.up * distanceToPlane;
      transform.LookAt(planePoint, transform.up);
   }  

}
