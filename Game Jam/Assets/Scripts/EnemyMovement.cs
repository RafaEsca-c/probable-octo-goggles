using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject deathParticles;
    public GameObject target;
    GameObject child;
    GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        child=this.transform.GetChild(0).gameObject;
        target = GameObject.FindWithTag("Player"); 
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().speed= GetComponent<UnityEngine.AI.NavMeshAgent>().speed* GameManager.GetComponent<GameManager>().waveIndex;
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        if (child.GetComponent<Enemy>().health <= 0)
        {
            child.SetActive(false);
            if (child != null)
            {

                StartCoroutine(Death());
            }
            Destroy(child);
            
        }
    }
    IEnumerator Death()
    {
        GameManager.GetComponent<GameManager>().enemyKilled++;
        Instantiate(deathParticles, gameObject.transform);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
