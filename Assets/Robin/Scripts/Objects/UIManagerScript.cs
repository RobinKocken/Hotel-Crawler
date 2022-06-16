using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public FPSController fps;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;

    void Start()
    {
        
    }


    void Update()
    {
        healthText.text = fps.playerHealth.ToString();
        shieldText.text = fps.playerShield.ToString();
    }
}
