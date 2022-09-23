using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceStones : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            bool[] collectedStones = StateManager.instance.collectedStones;

            for(int i=0; i<collectedStones.Length; i++){
                if(collectedStones[i]){
                    string child = "Stone-" + i;
                    Transform childObject = transform.Find(child);
                    childObject.gameObject.SetActive(true);
                    StateManager.instance.PlaceStone(i);
                    AudioManager.instance.Play("Collect");
                }
            }

            if(StateManager.instance.placedStones == 7){
                Transform confetti = transform.Find("ConfettiSpray");
                GetComponent<AudioSource>().Play();
                confetti.GetComponent<ParticleSystem>().Play();
                player.GetComponent<PlayerController>().Stop();
                StateManager.instance.Win();
            }
        }
    }
}
