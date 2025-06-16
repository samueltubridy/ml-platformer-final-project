using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;

    void Update() {
        transform.position = new Vector3(target.position.x, target.position.y + 2, -1f);
    }
}