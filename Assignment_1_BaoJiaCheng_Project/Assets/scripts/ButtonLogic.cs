using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    Rigidbody boxRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        // get collider
        Collider collider = GetComponent<Collider>();

        // judge collider
        if (collider != null)
        {
            Debug.Log("This object has a collider.");
        }
        else
        {
            Debug.Log("This object does not have a collider.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}