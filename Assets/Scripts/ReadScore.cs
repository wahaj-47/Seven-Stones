﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadScore : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Stones: "+StateManager.instance.placedStones.ToString();
    }
}
