using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{

    [Header("Players's Current Status")]
    public float playerHealth;
    public int playerMoney;

    [Header("WaveManagement")]
    public int enemyOnScreen;
    public int enemyKilled;
    public int wave=1;
    public bool isPlaying=true;


    [Header("Dificulty Scaling")]
    public float waveIndex;
    public float waveIndexAddition=.2f;


    [Serializable]
    class SaveData
    {
        public int money;
        public int progress;
        public bool savedBool;
    }

    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
                     + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.money = playerMoney;
        data.progress = wave;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
                      + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
                              + "/MySaveData.dat");
            playerMoney=0;
            wave=0;
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }

    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            playerMoney = data.money;
            wave = data.progress;
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }


    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
        waveIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        enemyOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("Saving...");
            SaveGame();
        }
        if (playerHealth <= 0)
        {
            isPlaying = false;
        }
        if (playerHealth >= 100)
        {
            playerHealth = 100;
        }
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("Ereasing Data...");
            ResetData();
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        playerHealth -= damage;
    }
    public void CompleteWave()
    {
        waveIndex = waveIndex + waveIndexAddition;
        wave++;
        isPlaying = false;
        Debug.Log("Wave Completed!!!");
    }
}