using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField] private GameObject normalVisual;
    [SerializeField] private GameObject fadeVisual;
    [SerializeField] private SphereCollider detectCollider;
    [SerializeField] private float offset = 1f;

    private float detectRadius;
    private Transform playerTransform;
    private bool isFade = false;

    private void Awake() {
        detectRadius = detectCollider.radius + offset;
        isFade = false;
    }

    private void OnEnable() {
        OnInit();
    }

    private void Update() {
        if (!isFade) return;

        if (IsOutOfDetectRadius()) {
            ResetFade();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Constant.CHARACTER_TAG)) {
            Character character = Cache.GetCharacter(other);
            if (character is Player) {
                Fade();
                isFade = true;
                if (playerTransform == null) {
                    playerTransform = character.TF;
                }
            }
        }
    }

    private bool IsOutOfDetectRadius() {
        if (playerTransform == null) return true;

        return (playerTransform.position - transform.position).sqrMagnitude > detectRadius * detectRadius;
    }

    private void OnInit() {
        ResetFade();
    }

    private void Fade() {
        normalVisual.SetActive(false);
        fadeVisual.SetActive(true);
    }

    private void ResetFade() {
        normalVisual.SetActive(true);
        fadeVisual.SetActive(false);
    }
}
