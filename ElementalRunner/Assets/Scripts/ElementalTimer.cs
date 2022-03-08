using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementalTimer : MonoBehaviour
{
    Slider slider;
    public static float fireTime = 4.0f;

    private void Start()
    {
        slider = this.GetComponent<Slider>();
    }

    private float timer = fireTime;
    void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            timer += Time.deltaTime * 2.0f;
        }

        if (timer < 0)
        {
            Animator sliderAnimator = slider.GetComponentInChildren<Animator>();
            sliderAnimator.SetBool("iswater", true);
            timer = fireTime;
        }
        timer = Mathf.Clamp(timer, 0, fireTime);
        slider.value = timer / fireTime;
    }
}
