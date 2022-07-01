using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Conversation : MonoBehaviour
{
    public MouseVisibility mouse;

    public string[] questions;

    public TextMeshProUGUI text;
    public GameObject yes;
    public GameObject no;
    public GameObject canvas;
    public int number;
    public bool first;

    void Start()
    {
        text.text = questions[0];
    }

    void Update()
    {
        Debug.Log("bing");
        text.text = questions[number];
        if (number == 2 || number == 3 || number == 5 || number == 6)
        {
            canvas.SetActive(false);
            mouse.MouseMode(true);
        }
        else
        {
            mouse.MouseMode(false);
        }
    }

    public void ButtenYes()
    {
        if (first == true)
        {
            number += 1;
        }

        if (first == false)
        {
            number = 1;
            first = true;
        }
    }

    public void ButtenNo()
    {
        if (first == true)
        {
            number += 2;
        }

        if (first == false)
        {
            number = 4;
            first = true;
        }
    }
}
