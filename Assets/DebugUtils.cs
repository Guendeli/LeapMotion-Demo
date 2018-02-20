using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity.Interaction;

public class DebugUtils : MonoBehaviour {

    public Text targetText;
    private string debugString;
    private List<InteractionController> controllers;
    // Use this for initialization
	void Start () {
        debugString = string.Empty;
        controllers = new List<InteractionController>();
        foreach (var cont in InteractionManager.instance.interactionControllers)
        {
            controllers.Add(cont);
        }
	}
	
	// Update is called once per frame
	void Update () {
        debugString = "Pitch Left: " + controllers[0].intHand.leapHand.PinchStrength.ToString() + " Pitch Right: " + controllers[1].intHand.leapHand.PinchStrength.ToString();
        targetText.text = debugString;
	}
}
