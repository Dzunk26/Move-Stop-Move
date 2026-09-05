using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectTriggerVisual : MonoBehaviour {
    public Transform TF {
        get {
            if (tf == null) {
                tf = transform;
            }

            return tf;
        }
    }

    [SerializeField] private float scaleConverter = 2f;

    private Transform tf;

    public void SetRange(float worldAttackRange) {
        float scale = worldAttackRange * scaleConverter;
        TF.localScale = new Vector3(scale, scale, scale);
    }
}