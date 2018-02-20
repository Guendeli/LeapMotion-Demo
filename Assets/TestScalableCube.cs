using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScalableCube : MonoBehaviour {

    public Transform positiveHandle;
    public Transform negativeHandle;
    public float multiplier;
    public GameObject cubeObj;

    private InteractionBehaviour intBehaviour;
    private List<InteractionController> controllers;
    private bool _isGrasped;
    private bool _instancing;
    private GameObject _targetCube;

	// Use this for initialization
	void Start () {
        intBehaviour = GetComponent<InteractionBehaviour>();
        controllers = new List<InteractionController>();
        foreach (var cont in InteractionManager.instance.interactionControllers)
        {
            controllers.Add(cont);
        }
	}
	
	// Update is called once per frame
	void Update () {
        // enter instance mode
        if(controllers[0].intHand.leapHand.PinchStrength > 0.4f &&
            controllers[1].intHand.leapHand.PinchStrength > 0.4f)
        {
            if (!_instancing)
            {
                // instantiate our cube if distance is close enough
                if (Vector3.Distance(controllers[0].position, controllers[1].position) < 0.2f)
                {
                    _targetCube = createCube(cubeObj, controllers[0].position, controllers[1].position);
                }
            }
            _instancing = true;
            ScaleCube(controllers[0].position, controllers[1].position);

        }
        else
        {
            if (controllers[0].intHand.leapHand.PinchStrength < 0.4f &&
            controllers[1].intHand.leapHand.PinchStrength < 0.4f)
            {
                _instancing = false;
                _targetCube = null;
            }
        }
	}


    // helper methods are done here:
    GameObject createCube(GameObject target, Vector3 pos1, Vector3 pos2)
    {
        float zPos = (pos1.z + pos2.z) / 2;
        Vector3 pos = new Vector3(pos1.x, pos1.y, zPos);

        GameObject go = (GameObject)Instantiate(target, pos, Quaternion.identity);
        return go;
    }
    void ScaleCube(Vector3 pos1, Vector3 pos2)
    {
        if (_targetCube == null) { return; }
        // scaleFirst
        float zScale = Vector3.Distance(pos1, pos2);
        Vector3 scale = new Vector3(zScale, zScale, zScale) * multiplier;
        // position
        float zPos = (pos1.z + pos2.z) / 2;
        Vector3 pos = new Vector3(pos1.x, pos2.y, zPos);

        _targetCube.transform.position = pos;
        _targetCube.transform.localScale = scale;
    }
}
