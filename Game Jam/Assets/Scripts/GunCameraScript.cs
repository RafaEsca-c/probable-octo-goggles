using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCameraScript : MonoBehaviour
{
    public Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = mainCamera.transform.position; 
    }
}
