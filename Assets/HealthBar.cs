using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public float fillSpeed = 0.51f;
    private float currentHealth = 1f;
    private float targetHealth = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    public float ChangeHealth(float newProgress)
    {
        targetHealth = slider.value + newProgress;
        return targetHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(slider.value - targetHealth) > .01)
        {
            if (slider.value < targetHealth)
            {
                slider.value += fillSpeed * Time.deltaTime;
            }
            else if (slider.value > targetHealth)
            {
                slider.value -= fillSpeed * Time.deltaTime;
            }
        }
    }
}
