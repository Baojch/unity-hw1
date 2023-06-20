using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    const float SPEED = 10.0f;
    Rigidbody m_rigidbody;
    [SerializeField]
    MonsterLogic monster;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.velocity = SPEED * transform.up;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "monster")
        {
            monster.hurt();
            if (monster.getcurrentHealth() == 0){
                
            }
        }
        // Destroy the Bullet
        Destroy(gameObject);
    }
}
