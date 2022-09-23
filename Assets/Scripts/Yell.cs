using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yell : MonoBehaviour
{
    public Text dialogue;
    public string[]  dialogues;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("YellRandomDialogue", 7, Random.Range(7,10));
    }

    private void YellRandomDialogue() {

        if(StateManager.instance.won || StateManager.instance.lost) {
            CancelInvoke("YellRandomDialogue");
            return;
        }

        int dialogueIndex = Random.Range(0, dialogues.Length-1);
        dialogue.text = dialogues[dialogueIndex];

        dialogue.GetComponent<Animator>().SetTrigger("Yell");
    }
}
