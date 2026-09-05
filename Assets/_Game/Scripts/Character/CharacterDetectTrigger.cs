using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetectTrigger : MonoBehaviour {
    [SerializeField] private Character character;
    [SerializeField] private SphereCollider detectCollider;

    public void SetRange(float attackRange) {
        detectCollider.radius = attackRange;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constant.CHARACTER_TAG)) {
            Character otherCharacter = Cache.GetCharacter(other);
            Debug.Log(character);
            if (otherCharacter != character) {
                character.OnDetectTarget(otherCharacter);
            }
        }
    }
}