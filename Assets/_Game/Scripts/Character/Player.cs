using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    Idle,
    Running,
    Attack,
    Dead
}

public class Player : Character {
    [SerializeField] private float rotateSpeed = 15;
    [SerializeField] private float deadTimerMax = 2f;
    [SerializeField] private PlayerDetectTriggerVisual playerDetectTriggerVisual;

    private Vector2 inputVector;
    private PlayerState currentState;
    private float deadTimer;

    private void OnEnable() {
        OnInit();
    }

    private void Update() {
        if (!GameManager.Instance.IsPlayingGame()) return;

        ListenInput();

        HandlePlayerState();
    }

    public override void OnInit() {
        base.OnInit();
    }

    public override void OnDespawn() {
        base.OnDespawn();
    }

    public override void OnHitted() {
        if (isDead) return;

        ChangeState(PlayerState.Dead);
    }

    protected override void OnAttackRangeChanged() {
        base.OnAttackRangeChanged();
        playerDetectTriggerVisual.SetRange(worldAttackRange);
    }


    private bool IsMoving() {
        return inputVector.sqrMagnitude > 0.01f;
    }

    private void ListenInput() {
        inputVector = GameInput.Instance.GetMovementVectorNormalized();
    }

    private void HandlePlayerState() {
        switch (currentState) {
            case PlayerState.Idle:
                characterVisual.OnIdle();
                if (IsMoving()) {
                    ChangeState(PlayerState.Running);
                }
                else if (HasTarget()) {
                    ResetCoolDownAttack();
                    ChangeState(PlayerState.Attack);
                }
                break;
            case PlayerState.Running:
                HandleMovement();
                characterVisual.OnRun();
                if (!IsMoving()) {
                    ChangeState(PlayerState.Idle);
                }
                break;
            case PlayerState.Attack:
                BeginAttack();
                OnCoolDownAttack();
                if (IsMoving()) {
                    ChangeState(PlayerState.Running);

                    if (attackTimer <= attackDelay) {
                        CancelAttack();
                        ResetCoolDownAttack();
                    }
                }
                if (!HasTarget() && currentTarget == null) {
                    ChangeState(PlayerState.Idle);
                }
                break;
            case PlayerState.Dead:
                Dead();
                deadTimer += Time.deltaTime;
                if (deadTimer >= deadTimerMax) {
                    // tat hinh anh
                    OnDespawn();
                }
                break;
        }
    }

    private void HandleMovement() {
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        Debug.DrawLine(TF.position, TF.position + moveDir * characterStat.GetWorldAttackRange()); // for testing
        moveDir = CheckFront(moveDir);
        TF.position += moveDir * characterStat.moveSpeed.GetValue() * Time.deltaTime;
        if (inputVector.sqrMagnitude > 0.001f) {
            TF.forward = Vector3.Lerp(TF.forward, moveDir, rotateSpeed * Time.deltaTime);
        }
    }

    private Vector3 CheckFront(Vector3 moveDir) {
        if (moveDir.sqrMagnitude < 0.001f) return moveDir;

        if (Physics.Raycast(TF.position, moveDir, out RaycastHit raycastHit, 0.75f)) {
            if (raycastHit.collider.CompareTag(Constant.OBSTACLE_TAG) || raycastHit.collider.CompareTag(Constant.WALL_TAG)) {
                moveDir = Vector3.zero;
            }
        }

        return moveDir;
    }

    private void ChangeState(PlayerState state) {
        if (currentState == state) return;

        currentState = state;
    }
}