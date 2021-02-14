using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaddyBullets : MonoBehaviour
{
    /*private Sprite defaultSprite;
    public Sprite muzzleFlash;*/
    public int framesToFlash = 3;
    public int destroyTime = 3;
    public float damage = 50;
    public int bulletSpeed;
    GameObject GameManager;
    /*public GameObject gun;
    public GameObject impact;*/

    // Start is called before the first frame update
    void Start()
    {

        //defaultSprite = spriteRend.sprite;

        StartCoroutine(TimeDestruction());
        GameManager = GameObject.Find("GameManager");

    }


    IEnumerator TimeDestruction()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }



    // Update is called once per frame
    void Update()
    {

        /*gun = GameObject.FindGameObjectWithTag("Gun");
        Quaternion diff = gun.transform.rotation;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z-180);*/
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);

        damage = damage * GameManager.GetComponent<GameManager>().waveIndex;

    }


    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            GameManager.GetComponent<GameManager>().PlayerTakeDamage(damage);
        }
        Destroy(gameObject);
    }

}