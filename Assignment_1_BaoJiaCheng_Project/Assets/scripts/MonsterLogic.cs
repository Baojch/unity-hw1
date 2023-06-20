using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterLogic : MonoBehaviour
{
    public int currentHealth = 100;
    int left = 30;
    int right;
    [SerializeField]
    MonsterLogic monsterLogic;

    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        monsterLogic = GetComponent<MonsterLogic>();
        // monsterLogic.sethealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        // if (monsterLogic.getcurrentHealth() <= 0)
        // {
        //     Debug.Log("destroy monster");
        //     Destroy(gameObject);
        //     return;
        // }

        // get current position
        Vector3 currentPosition = transform.position;
        if(left > 0){
            // calculate new position
            newPosition = currentPosition + new Vector3(-8.0f * Time.deltaTime, 0, 0);
            left--;
            if(left == 0){
                right = 60;
            }
        }else if(right > 0){
            newPosition = currentPosition + new Vector3(8.0f * Time.deltaTime, 0, 0);
            right--;
            if(right == 0){
                left = 60;
            }
        }

        // update position
        transform.position = Vector3.Lerp(transform.position, newPosition, 8.0f * Time.deltaTime);
    }

    public void hurt()
    {
        if(currentHealth == 0){
            return;
        }
        currentHealth -= 10;
        Debug.Log("currentHealth" + currentHealth);
    }
    public int getcurrentHealth()
    {
        return currentHealth;
    }
    public void sethealth()
    {
        currentHealth = 100;
    }

}
