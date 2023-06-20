using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Rigidbody boxRigidbody;
    private float desiredZPosition;
    private Quaternion initialRotation;
    [SerializeField]
    GameObject targetObject;

    AudioSource m_audioSource;

    [SerializeField]
    AudioClip button_click;

    private void Start()
    {
        boxRigidbody = GetComponent<Rigidbody>();

        desiredZPosition = transform.position.z;

        initialRotation = transform.rotation;
        
        m_audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        //freeze z axis
        Vector3 currentPosition = transform.position;
        currentPosition.z = desiredZPosition;
        transform.position = currentPosition;

        Quaternion currentRotation = transform.rotation;
        currentRotation = initialRotation;
        transform.rotation = currentRotation;
    }
    public void PushBox(Vector3 direction)
    {
        boxRigidbody.AddForce(direction, ForceMode.Impulse);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger collision occurred with: " + other.gameObject.name);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Rbutton"))
        {
            if (targetObject != null)
            {
                Vector3 newPosition = new Vector3(-5.78f, 4.45f, -0.48f);
                targetObject.transform.position = newPosition;
                m_audioSource.PlayOneShot(button_click);
            }
            
        }
        Destroy(other.gameObject);
        Debug.Log("destroy");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rbutton"))
        {
            boxRigidbody = null;
            Debug.Log("TriggerExit");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision:"+ collision.gameObject.name);
        if (collision.gameObject.CompareTag("Wall-LR"))
        {
            // box stop moving
            boxRigidbody.velocity = Vector3.zero;
            boxRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
