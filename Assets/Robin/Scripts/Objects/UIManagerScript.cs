using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public FPSController fps;

    public Slider slider;

    void Start()
    {
        slider.maxValue = fps.playerHealth;
        slider.value = fps.playerHealth;
    }


    void Update()
    {
        slider.value = fps.playerHealth;
    }
}
