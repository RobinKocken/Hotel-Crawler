using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Conversation : MonoBehaviour
{
    public string[] questions;
    public TextMeshProUGUI text;
    public GameObject yes;
    public GameObject no;
    public int number;
    public bool first;

    // Start is called before the first frame update
    void Start()
    {
        text.text = questions[0];
    }

    // Update is called once per frame
    void Update()
    {
        text.text = questions[number];
        if (number == 2 || number == 3 || number == 5 || number == 6)
        {
            yes.SetActive(false);
            no.SetActive(false);
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
