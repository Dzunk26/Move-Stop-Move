using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaitingState {
    public static void OnEnter(Bot bot) {
        bot.OnEnterWaiting();
    }

    public static void OnExecute(Bot bot) {
        bot.OnExecuteWaiting();
    }

    public static void OnExit(Bot bot) {

    }
}