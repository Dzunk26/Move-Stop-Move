using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetectTrigger : MonoBehaviour {
    [SerializeField] private Character character;

    private Transform tf;

    private void Awake() {
        tf = transform;
    }

    public void OnInit() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constant.CHARACTER_TAG)) {
            Character otherCharacter = Cache.GetCharacter(other);
            if (otherCharacter != character) {
                character.OnDetectTarget(otherCharacter);
            }
        }
    }
}