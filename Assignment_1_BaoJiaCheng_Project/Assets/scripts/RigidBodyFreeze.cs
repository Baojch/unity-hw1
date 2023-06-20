using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyFreeze : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
