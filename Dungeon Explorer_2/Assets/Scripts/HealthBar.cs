using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public Text percentageText;

    public int min;
    public int max;

    private int Current_value;
    private float Current_percentage;

    void Start()
    {
        Player_Health(100);
    }

    public void Player_Health(int Health)
    {
        if(Health != Current_value)
        {
            if(max - min == 0)
            {
                Current_value = 0;
                Current_percentage = 0;
            }
            else
            {
                Current_value = Health;
                Current_percentage = (float)Current_value / (float)(max - min);
            }
        }

        percentageText.text = string.Format("{0} %", Mathf.RoundToInt(Current_percentage * 100));
        bar.fillAmount = Current_percentage;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
