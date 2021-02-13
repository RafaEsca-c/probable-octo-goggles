using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform firePoint;
    public float health=100;
    public GameObject deathParticles;
    public GameObject bullet; 
    [Header("Wait Time Between Shots")]
    public float waitTime;
    [SerializeField]float timer = 0;

    GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

        if (waitTime == 0)
        {
            Fire();
        }
        else
        {
            if (Time.time > timer)
            {
                timer = Time.time + 1 / waitTime;
                Fire();
            }
        }
        health = waitTime * GameManager.GetComponent<GameManager>().waveIndex;
    }
    public void TakeDamage(int Damage)
    {
        health -= Damage;
    }


    void Fire()
    {
        Instantiate(bullet, firePoint.position, firePoint.transform.rotation);
    }
}
