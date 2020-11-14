using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{
    public Text goldText;
    public HeadQuarterBehaviour headQuarterBehaviour;

    // Update is called once per frame
    void Update()
    {
        if(goldText && headQuarterBehaviour)
            goldText.text = "Gold: " + headQuarterBehaviour.gold.ToString();
    }
}
