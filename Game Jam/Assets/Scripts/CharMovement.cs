using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    public GameObject player;

    public Transform spawnPoint;

    public float moveSpeed = 5f;

    public Animator animator;

    public Rigidbody rb;
    public int maxHealth = 100;
    public int currenthealth;

    [SerializeField]Vector3 movement;
    private float speed;

    GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetComponent<GameManager>().isPlaying)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");


            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                movement = new Vector3(0, 0, 0);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Danger")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}
