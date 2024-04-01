using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingBall : MonoBehaviour
{
    public int eventActive;
    public SpriteRenderer spriteRenderer;
    public float decreaseRate = 0.5f; // Rate at which opacity decreases per second
    private bool isCollided = false;

    void Start()
    {
        eventActive = 0;    
    }
    // Update is called once per frame
    void Update()
    {
        if (eventActive == 1)
        {
            if (!isCollided)
            {
                // Gradually decrease the opacity
                Color color = spriteRenderer.color;
                color.a -= decreaseRate * Time.deltaTime;
                color.a = Mathf.Clamp01(color.a); // Ensure alpha is between 0 and 1
                spriteRenderer.color = color;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }

    public void resetVanishingBall()
    {
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }

    public void activateVanishingBall()
    {
        if(eventActive == 0)
        {
            eventActive = 1;
        }
        else
        {
            resetVanishingBall();
            eventActive = 0;
        }
    }
}
