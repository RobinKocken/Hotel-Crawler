using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public FPSController fps;

    public TextMeshProUGUI healthText;

    void Start()
    {
        
    }


    void Update()
    {
        healthText.text = fps.playerHealth.ToString();
    }
}
