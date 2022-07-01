using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public FPSController fps;
    public MouseVisibility mouse;

    [SerializeField] public KeyCode menuKey;
    public GameObject overlay;
    bool menuActive;

    public Slider slider;

    public GameObject inv;
    public GameObject men;
    public GameObject inventory;
    public GameObject menu;
    public GameObject user;

    void Start()
    {
        slider.maxValue = fps.playerHealth;
        slider.value = fps.playerHealth;
    }


    void Update()
    {
        slider.value = fps.playerHealth;

        MenuOverlay();
    }

    void MenuOverlay()
    {
        if(Input.GetKeyDown(menuKey))
        {
            menuActive = !menuActive;
            if(menuActive) 
            {
                mouse.MouseMode(false);

                Menu(); 
                Overlay(menuActive);
                user.SetActive(!menuActive);
            }

            else if(!menuActive)
            {
                mouse.MouseMode(true);

                Overlay(menuActive);
                user.SetActive(!menuActive);
            }
        }
    }

    public void Inventory()
    {
        inventory.SetActive(true);
        inv.GetComponent<Image>().color = Color.white;

        menu.SetActive(false);
        men.GetComponent<Image>().color = Color.grey;
    }

    public void Menu()
    {
        menu.SetActive(true);
        men.GetComponent<Image>().color = Color.white;

        inventory.SetActive(false);
        inv.GetComponent<Image>().color = Color.grey;
    }

    public void Return()
    {
        mouse.MouseMode(true);

        menuActive = false;

        Overlay(menuActive);
        user.SetActive(!menuActive);
    }

    void Overlay(bool b)
    {
        overlay.SetActive(b);
    }
}
