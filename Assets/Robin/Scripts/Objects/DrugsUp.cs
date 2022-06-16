using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsUp : MonoBehaviour
{
    public FPSController fps;

    bool picked;

    public int health;
    public int shield;

    public float moveSpeed;
    public float runSpeed;

    public int bleed;
    public float speeedBleed;
    float startTimeBleed;

    float startTime;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(picked)
        {
            startTime = Time.time;
            Drug();

            if(Time.time - startTime < duration)
            {
                picked = false;
                Clean();
            }
        }

    }

    void Drug()
    {
        if(health != 0)
        {
            fps.playerHealth += health;
        }
        
        if(shield != 0)
        {
            fps.playerShield += shield;
        }

        if(moveSpeed != 0 && runSpeed != 0)
        {
            fps.walkingSpeed = moveSpeed;
            fps.runningSpeed = runSpeed;
        }

        if(bleed != 0)
        {
            if(Time.time - startTimeBleed > speeedBleed)
            {
                fps.playerHealth -= bleed;
                startTimeBleed = Time.time;
            }
        }
    }

    void Clean()
    {

    }
}
