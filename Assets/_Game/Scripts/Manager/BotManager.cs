using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BotManager : Singleton<BotManager> {
    private List<Bot> bots = new List<Bot>();
    private Queue<Transform> availableSpawnPoints = new Queue<Transform>();
    private Queue<Transform> inUseSpawnPoints = new Queue<Transform>();
    private int maxActiveBot;
    private int currentActiveBot;
    private int remainBotAmount;

    private void Update() {
        if (!GameManager.Instance.IsPlayingGame()) return;

        RemoveDeadBots();

        if (CanSpawn()) {
            SpawnBot();
        }
    }
    
    public void OnLoadLevel(int totalBotAmount, int maxActiveBot, Level level) {
        ClearBots();
        currentActiveBot = 0;
        remainBotAmount = totalBotAmount;
        this.maxActiveBot = maxActiveBot;
        availableSpawnPoints.Clear();
        inUseSpawnPoints.Clear();
        foreach (Transform point in level.GetListSpawnPoint()) {
            availableSpawnPoints.Enqueue(point);
        }
    }

    public int GetRemainBotCount() {
        return remainBotAmount;
    }

    private void SpawnBot() {
        Transform spawnPoint = availableSpawnPoints.Dequeue();
        inUseSpawnPoints.Enqueue(spawnPoint);
        Vector3 pos = spawnPoint.position;

        Bot bot = SimplePool.GetFromPool<Bot>(PoolType.Bot, pos, Quaternion.identity);
        bot.OnInit();
        bots.Add(bot);
        currentActiveBot++;
        remainBotAmount--;
    }

    private void RemoveDeadBots() {
        for (int i = bots.Count - 1; i >= 0; i--) {
            if (bots[i].IsDead) {
                RemoveDeadBot(bots[i]);
            }
        }
    }

    private void RemoveDeadBot(Bot bot) {
        if (!bots.Remove(bot)) return;
        
        currentActiveBot--;
        Transform inUseSpawnPoint = inUseSpawnPoints.Dequeue();
        availableSpawnPoints.Enqueue(inUseSpawnPoint);
        SimplePool.ReturnToPool(bot);
    }

    private void ClearBots() {
        if (bots == null) return;

        foreach (Bot bot in bots) {
            SimplePool.ReturnToPool(bot);
        }

        bots.Clear();
        availableSpawnPoints.Clear();
        inUseSpawnPoints.Clear();
        currentActiveBot = 0;
        remainBotAmount = 0;
    }

    private bool CanSpawn() {
        return currentActiveBot < maxActiveBot && remainBotAmount > 0 && availableSpawnPoints.Count > 0;
    }
}