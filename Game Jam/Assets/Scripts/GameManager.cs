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
    public bool playerVulnerable=true;
    public float damageBoost=1f;



    [Header("WaveManagement")]
    public int enemyOnScreen;
    public int enemyKilled;
    public int wave=1;
    public bool isPlaying=true;


    [Header("Dificulty Scaling")]
    public float waveIndex;
    public float waveIndexAddition=.2f;


    public Animator playerAnimator;
    public GameObject player;
    public GameObject playerDeath;
    public GameObject gun;

    [Header("UI References")]
    public bool isPaused=false;
    public Image lifeGauge;
    public GameObject pausePanel;
    public GameObject lifeBar;
    public GameObject optionPanel;

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
        playerDeath.SetActive(false);
        player.SetActive(true);
        gun.SetActive(true);
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
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            lifeBar.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            lifeBar.SetActive(true);
        }

        lifeGauge.fillAmount = playerHealth * .01f;


        if (playerHealth <= 0)
        {
            isPlaying = false;
            playerDeath.SetActive(true);
            player.SetActive(false);
            gun.SetActive(false);
        }







        if (playerHealth <= 0)
        {
            playerAnimator.SetBool("isAlive", false);
        }
        else
        {
            playerAnimator.SetBool("isAlive", true);
        }
        
        enemyOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (Input.GetKeyDown("i"))
        {
            Debug.Log("Saving...");
            SaveGame();
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
        if (playerVulnerable)
        {
            playerHealth -= damage;
            StartCoroutine(DamageBoost());
        }
    }
    IEnumerator DamageBoost()
    {
        playerVulnerable = false;
        yield return new WaitForSeconds(damageBoost);
        playerVulnerable = true;
    }
    public void CompleteWave()
    {
        waveIndex = waveIndex + waveIndexAddition;
        wave++;
        isPlaying = false;
        Debug.Log("Wave Completed!!!");
    }

    public void Resume()
    {
        isPaused = !isPaused;
    }

    public void Options()
    {
        optionPanel.SetActive(true);


    }
    public void Back()
    {
        optionPanel.SetActive(false);



    }

}
