using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class gunLogic : MonoBehaviour
{
    [SerializeField]
    GameObject m_bulletPrefab;

    [SerializeField]
    Transform m_spawnPoint;

    [SerializeField]
    Transform monsterSpawnpoint;

    [SerializeField]
    Transform monster;

    [SerializeField]
    MonsterLogic monsterLogic;

    [SerializeField]
    PlayerLogic player;

    [SerializeField]
    GameObject healthBar;

    AudioSource m_audioSource;

    // [SerializeField]
    // AudioClip m_gunShotSound;


    bool getgun;
    bool isleft;
    gunLogic self;

    const float MAX_COOLDOWN = 0.5f;
    float m_cooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        getgun = false;
        self = GetComponent<gunLogic>();
        healthBar.SetActive(false);
    }

    void Shoot()
    {
        if (m_cooldown <= 0.0f)
        {

            Instantiate(m_bulletPrefab, m_spawnPoint.position, m_spawnPoint.rotation);

            m_cooldown = MAX_COOLDOWN;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!getgun)
            return;

        RotateCharacterTowardsMouseCursor();
        if (Input.GetButton("Fire1"))
        {
            Shoot();
            // m_audioSource.PlayOneShot(m_gunShotSound);
        }


        if (m_cooldown > 0.0f)
        {
            m_cooldown -= Time.deltaTime;
        }

    }

    void RotateCharacterTowardsMouseCursor()
    {
        Vector3 mousePosInScreenSpace = Input.mousePosition;
        Vector3 playerPosInScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 directionInScreenSpace = mousePosInScreenSpace - playerPosInScreenSpace;

        float angle = Mathf.Atan2(directionInScreenSpace.y, directionInScreenSpace.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 180.0f, Vector3.forward);

        if (directionInScreenSpace.x > 0)
        {
            targetRotation *= Quaternion.Euler(0f, 180f, 180f);
            if (isleft)
            {
                player.ChangeEquip(self,isleft);
                isleft = false;
            }
       
        }
        else
        {
            if (!isleft)
            {
                player.ChangeEquip(self, isleft);
                isleft = true;
            }
            
        }

        transform.rotation = targetRotation;
    }
    public void Getgun()
    {
        getgun = true;
        isleft = true;
        Debug.Log("Getgun! Show monster!");
        Instantiate(monster, monsterSpawnpoint.position, monsterSpawnpoint.rotation);
        healthBar.SetActive(true);
        monsterLogic.sethealth();
    }
}
