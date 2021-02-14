using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemy : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;
    public float health = 100;
    public GameObject bullet;


    [Header("Wait Time Between Shots")]
    public float waitTime;
    [SerializeField] float timer = 0;

    GameObject GameManager;
    [SerializeField] bool isDamageOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        health = health * GameManager.GetComponent<GameManager>().waveIndex;
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

    }
    public void TakeDamage(int Damage)
    {
        if (isDamageOpen)
        {
            health = health - Damage;
            isDamageOpen = false;
            StartCoroutine(DamageBoost());
        }
    }


    IEnumerator DamageBoost()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(.1f);
        isDamageOpen = true;
    }

    void Fire()
    {
        Instantiate(bullet, firePoint.position, firePoint.transform.rotation);
        Instantiate(bullet, firePoint2.position, firePoint2.transform.rotation);
        Instantiate(bullet, firePoint3.position, firePoint3.transform.rotation);
        Instantiate(bullet, firePoint4.position, firePoint4.transform.rotation);
    }
}