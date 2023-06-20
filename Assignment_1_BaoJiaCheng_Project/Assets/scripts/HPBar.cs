using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    public Slider s;
    public MonsterLogic monster;
    [SerializeField]
    TextMeshProUGUI Ending;

    bool flag = true;

    // Start is called before the first frame updatess
    void Start()
    {
        s.value = 100;
        flag = true;
        Ending.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(monster.getcurrentHealth() > 0)
        {
            UpdateHealthBar(monster.getcurrentHealth());
            //Debug.Log("monster.currentHealth"+ monster.currentHealth+"health:" + s.value);
            flag = true;
 
        }
        else if (monster.getcurrentHealth() == 0 && flag)
        {
            UpdateHealthBar(monster.getcurrentHealth());
            Debug.Log("You win!");
            flag = false;
            Ending.gameObject.SetActive(true);
        }
    }
    public void UpdateHealthBar(int currentHealth)
    {
        s.value = currentHealth;
    }

}
