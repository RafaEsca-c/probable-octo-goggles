using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public GameObject player;
    Vector3 look;
    float scale;
    public Transform firePoint;
    public int distance;
    public Vector3 firepos;
    public GameObject bullet;
    //public Animator animator;
    float rotation;
    public float rot_z;
    public Camera gunCamera;

    public float waitTime;
    float timer = 0;

    GameObject GameManager;

    // Start is called before the first frame update
    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetComponent<GameManager>().isPlaying)
        {
            Vector3 diff = gunCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();

            rot_z = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, rot_z - 90, 0);
            look = player.transform.localScale;
            look.Normalize();
            scale = look.x;

            //animator.SetFloat("rotation", transform.rotation.z);

            int number = Random.Range(1, 2);

            if (waitTime == 0)
            {
                if (Input.GetButton("Fire1"))
                {
                    Fire();
                }
            }
            else
            {
                if (Input.GetButton("Fire1") && Time.time > timer)
                {
                    timer = Time.time + 1 / waitTime;
                    Fire();
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                timer = Time.time + 1 / waitTime;
                Fire();
            }
        }

    }

    void Fire()
    {
        Instantiate(bullet, firePoint.position, firePoint.transform.rotation);
    }


}