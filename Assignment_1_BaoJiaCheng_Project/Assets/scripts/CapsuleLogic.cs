using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleLogic : MonoBehaviour
{
    public float spinSpeedUp = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // spin
        transform.Rotate(Vector3.up, spinSpeedUp * Time.deltaTime);

     }
}
