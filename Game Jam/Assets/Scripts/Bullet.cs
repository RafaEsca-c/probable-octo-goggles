using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    /*private Sprite defaultSprite;
    public Sprite muzzleFlash;*/
    public int framesToFlash = 3;
    public int destroyTime = 3;
    public int damage = 50;
    public int bulletSpeed;
    /*public GameObject gun;
    public GameObject impact;*/

    // Start is called before the first frame update
    void Start()
    {
        //defaultSprite = spriteRend.sprite;

        StartCoroutine(TimeDestruction());

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

    }


    void OnTriggerEnter(Collider hitInfo)
     {
         
         Enemy enemy = hitInfo.GetComponent<Enemy>();
         if (enemy != null)
         {
             enemy.TakeDamage(damage);
         }
     }

}