using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    CharacterController m_CharacterController;
    GameObject gun;

    [SerializeField]
    Transform m_GunAttachmentTransformLeft;

    [SerializeField]
    Transform m_GunAttachmentTransformRight;

    AudioSource m_audioSource;

    [SerializeField]
    AudioClip eat_capsule;


    const float GRAVITY = -1.0f;
    const float MOVEMENT_SPEED = 5.0f;
    const float JUMPFORCE = 5.0f;
    
    private float desiredZPosition;

    float m_horizontalInput;
    // float m_verticalInput;
    int m_isJumping;
    int m_falling;
    int changing_m_isjumping;
    bool secondJump_Lock;
    bool has_gun;

    Vector3 m_movement;

    private Rigidbody boxRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        secondJump_Lock = false;

        boxRigidbody = null;
        
        desiredZPosition = transform.position.z;
        changing_m_isjumping = 4;
        m_audioSource = GetComponent<AudioSource>();
        has_gun = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        // m_verticalInput = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space) && m_CharacterController.isGrounded){
            m_isJumping += changing_m_isjumping;
            Debug.Log("m_isJumping");
            secondJump_Lock = false;
        }else
        { //on the ground
            m_movement.y =+ GRAVITY * Time.deltaTime;   
        }
        // judge second jump
        if (Input.GetKeyDown(KeyCode.Space) && !m_CharacterController.isGrounded && !secondJump_Lock)
        {
            Debug.Log("Second Jump!!!"); 
            m_isJumping += changing_m_isjumping - 1;
            secondJump_Lock = true;
        }
        //push box
        if (boxRigidbody != null)
        {
            Vector3 pushDirection = transform.forward;
            float pushForce = 1.0f;
            pushDirection.y = 0;
            pushDirection.z = 0;
            boxRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
        //freeze z axis
        Vector3 currentPosition = transform.position;

        currentPosition.z = desiredZPosition;

        transform.position = currentPosition;

    }

    void FixedUpdate() {
        m_movement.x = m_horizontalInput * MOVEMENT_SPEED * Time.deltaTime;
        // m_movement.z = m_verticalInput * MOVEMENT_SPEED * Time.deltaTime;

        if (m_isJumping != 0)
        {
            m_movement.y += JUMPFORCE * m_isJumping * Time.deltaTime;
            m_isJumping--;

            if(m_isJumping == 1)
            {
                m_falling = 99;
            }
            //Debug.Log("m_isjumping: " + m_isJumping);
        }
        else{
            // falling
            if (m_falling != 0 && !m_CharacterController.isGrounded)
            {
                m_movement.y += GRAVITY * Time.deltaTime * (100 - m_falling) / 2;
                //Debug.Log("m_movemnet.y:" + m_movement.y + ",m_falling" + m_falling);
                m_falling--;
            }
            else // On ground
            {
                m_falling = 100;
                m_movement.y += GRAVITY * Time.deltaTime;
            }
        }
        m_CharacterController.Move(m_movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("pill"))
        {
            Debug.Log("TriggerEnter:" + other.gameObject.name);
            changing_m_isjumping = 5;
            Destroy(other.gameObject);
            m_audioSource.PlayOneShot(eat_capsule);
        }
        else if (other.CompareTag("BallLeft"))
        {
            Debug.Log("TriggerEnter:" + other.gameObject.name);
            //player move position
            Vector3 currentPosition = transform.position;
            m_movement.x += 17.0f;
            m_CharacterController.Move(m_movement);
        }
        else if (other.CompareTag("gun"))
        {
            if(has_gun)
            {
                return;
            }
            Debug.Log("TriggerEnter: gun");
            gunLogic gun = other.GetComponent<gunLogic>();
            if (gun != null)
            {
                Debug.Log("TriggerEnter: gun");
                gun.Getgun();
                has_gun = true;
                EquipWeaponLeft(gun);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("box"))
        {
            boxRigidbody = other.GetComponent<Rigidbody>();
            //Debug.Log("TriggerStay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("box"))
        {
            boxRigidbody = null;
            //Debug.Log("TriggerExit");
        }
    }
    void EquipWeaponLeft(gunLogic interactionObject)
    {
        // Set Position and Direction of Gun
        interactionObject.transform.position = m_GunAttachmentTransformLeft.position;
        interactionObject.transform.forward = m_GunAttachmentTransformLeft.forward;

        // Parent Gun to Attach Point
        interactionObject.transform.parent = m_GunAttachmentTransformLeft;
    }
    void EquipWeaponRight(gunLogic interactionObject)
    {

        // Set Position and Direction of Gun
        interactionObject.transform.position = m_GunAttachmentTransformRight.position;
        interactionObject.transform.forward = m_GunAttachmentTransformRight.forward;

        // Parent Gun to Attach Point
        interactionObject.transform.parent = m_GunAttachmentTransformRight;
    }
    public void ChangeEquip(gunLogic interactionObject, bool isleft)
    {

        if (isleft)
        {
            EquipWeaponRight(interactionObject);
        }
        else
        {
            EquipWeaponLeft(interactionObject);
        }

    }
}
