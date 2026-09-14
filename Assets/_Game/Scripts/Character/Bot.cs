using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character {
    public bool IsDestination => (currentTargetPoint - TF.position).sqrMagnitude < 0.001f;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float stateTimerMax = 5f;
    [SerializeField] private int attackLimitMax = 3;

    private BotState currentState;
    private Vector3 currentTargetPoint;
    private float stateTimer;
    private int attackLimit;
    private int attackCount;
    private bool isPaused;

    private void Update() {
        if (isPaused) return;

        RemoveInvalidTagets();

        if (currentState != null) {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit() {
        base.OnInit();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        if (GameManager.Instance.IsPlayingGame()) {
            ChangeState(BotStates.Idle);
        }
        else {
            ChangeState(BotStates.Waiting);
        }
    }

    public override void OnDespawn() {
        base.OnDespawn();
        isPaused = false;
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }
    public void OnEnterWaiting() {
        StopMoving();
    }

    public void OnExecuteWaiting() {
        characterVisual.OnIdle();
    }

    public void OnEnterIdle() {
        stateTimer = Random.Range(stateTimerMax / 2, stateTimerMax);
        StopMoving();
    }

    public void OnExecuteIdle() {
        stateTimer -= Time.deltaTime;
        characterVisual.OnIdle();
        if (HasTarget()) {
            ChangeState(BotStates.Attack);
        }
        if (stateTimer <= 0) {
            ChangeState(BotStates.Patrol);
        }
    }

    public void OnEnterPatrol() {
        Vector3 target = GetRandomTargetPoint();
        SetDestination(target);
        ContinueMoving();
    }

    public void OnExecutePatrol() {
        characterVisual.OnRun();
        if (IsDestination) {
            ChangeState(BotStates.Idle);
        }
    }

    public void OnEnterAttack() {
        attackCount = 0;
        attackLimit = Random.Range(1, attackLimitMax + 1);
        StopMoving();
    }

    public void OnExecuteAttack() {
        OnCoolDownAttack();

        if (isAttacking) return;

        if (attackCount >= attackLimit || !HasTarget()) {
            ChangeState(BotStates.Patrol);
            return;
        }

        BeginAttack();
    }

    public void OnEnterDead() {
        stateTimer = Random.Range(stateTimerMax / 2, stateTimerMax);
        StopMoving();
        Dead();
    }

    public void OnExecuteDead() {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0) {
            OnDespawn();
        }
    }

    public Vector3 GetRandomTargetPoint() {
        Vector3 targetPoint = Vector3.zero;
        NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();

        int indiceIndex = Random.Range(0, navMeshTriangulation.indices.Length); // random dinh cua tam giac tao nen navmesh
        int vertexIndex = navMeshTriangulation.indices[indiceIndex];

        targetPoint = navMeshTriangulation.vertices[vertexIndex];
        return targetPoint;
    }

    public void StopMoving() {
        if (!agent.isOnNavMesh) return;

        agent.isStopped = true;
    }

    public void ContinueMoving() {
        agent.isStopped = false;
    }

    public void SetDestination(Vector3 target) {
        this.currentTargetPoint = target;
        agent.SetDestination(target);
    }

    public void ChangeState(BotState newState) {
        currentState?.OnExit(this);

        currentState = newState;

        currentState?.OnEnter(this);
    }

    public override void OnHitted() {
        if (isDead) return;

        Dead();
    }

    protected override void Dead() {
        base.Dead();
        ChangeState(BotStates.Dead);
    }

    protected override void BeginAttack() {
        base.BeginAttack();
        attackCount++;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsPauseGame()) {
            isPaused = true;
            StopMoving();
            characterVisual.OnPause();
        }
        else if (GameManager.Instance.IsPlayingGame()) {
            if (currentState == BotStates.Waiting) {
                isPaused = false;
                ChangeState(BotStates.Patrol);
                return;
            }

            if (!isPaused) return;

            isPaused = false;

            if (currentState == BotStates.Patrol) {
                ContinueMoving();
            }
        }
    }
}