using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCubeSpawner : MonoBehaviour {

    public Transform targetCube;

    public void ResetCube()
    {
        Vector3 initPos = Camera.main.transform.position;
        initPos.z += 0.5f;
        targetCube.transform.position = initPos;
    }
}
