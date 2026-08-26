using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform TF {
        get {
            if (tf == null) {
                tf = transform;
            }

            return tf;
        }
    }

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed =  7.5f;

    private Transform tf;

    private void LateUpdate() {
        TF.position = Vector3.Lerp(TF.position, target.position + offset, moveSpeed * Time.deltaTime);
    }
}