using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
    public GameObject asteroid;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAsteroidAtPlayerPosition", 2, 2);
    }

    private void Update() {
        if(StateManager.instance.lost || StateManager.instance.won)
            CancelInvoke("SpawnAsteroidAtPlayerPosition");
    }

    private void SpawnAsteroidAtPlayerPosition(){
        Instantiate(asteroid, player.position + Vector3.Scale(player.up, new Vector3(30,30,30)) + Vector3.Scale(player.forward.normalized, new Vector3(30,30,30)), player.rotation);
    }


}
