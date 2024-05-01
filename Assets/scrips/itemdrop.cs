using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemdrop : MonoBehaviour
{
    private Rigidbody rb;
    public float dropForce = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
