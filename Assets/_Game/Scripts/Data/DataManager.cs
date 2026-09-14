using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : Singleton<DataManager> {
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private DataHandler dataHandler = new DataHandler();

    private void OnEnable() {
        //GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    public void OnInit() {
        dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        //if (GameManager.Instance.IsLoadingGame()) {
        //    PlayerPrefs.DeleteAll();
        //    LoadGame();
        //}
    }

    public void NewGame() {
        gameData = new GameData();
    }

    public void LoadGame() {
        gameData = dataHandler.Load();

        if (gameData == null) {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }

        if (dataPersistenceObjects == null) return;

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame() {
        if (dataPersistenceObjects == null) return;

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    // save game khi chay tren dien thoai
    private void OnApplicationPause(bool pause) {
        if (pause) {
            SaveGame();
        }
    }

    // save game khi chay tren may tinh
    private void OnApplicationQuit() {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }
}