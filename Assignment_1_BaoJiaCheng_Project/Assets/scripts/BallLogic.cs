using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    public float spinSpeedUp = 90f;
    public float spinSpeedR = 0f;
    public float spinSpeedF = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // spin
        transform.Rotate(Vector3.up, Random.Range(1, 3) * spinSpeedUp * Time.deltaTime);
        transform.Rotate(Vector3.right, Random.Range(1, 3) * spinSpeedR * Time.deltaTime);
        transform.Rotate(Vector3.forward, Random.Range(1, 3) * spinSpeedF * Time.deltaTime);

    }
}
