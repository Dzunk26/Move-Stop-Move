using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttackState {
    public static void OnEnter(Bot bot) {
        bot.OnEnterAttack();
    }

    public static void OnExecute(Bot bot) {
        bot.OnExecuteAttack();
    }

    public static void OnExit(Bot bot) {

    }
}