using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float forceMagnitude = 5f;
    public float rotationSpeed = 45f;

    public SpriteRenderer spriteRenderer;
    public GameObject soundManager;
    private SoundManager soundEffect;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI eventTitle;
    public TextMeshProUGUI eventDesc;
    public TextMeshProUGUI eventCountdown;

    public float initialSpeed = 5f; // Initial speed of the ball
    public float speedIncreaseAmount = 1f; // Amount by which speed increases
    public float maxSpeed = 10f; // Maximum speed of the ball

    public int winCond;

    public Vector3 lastPosition;
    public GameObject explosion;
    public GameObject gameManager;
    public RandomEvent randEvent;
    public VanishingBall vanishingBall;

    // Start is called before the first frame update
    void Start()
    {
        winCond = 0;
        randEvent = gameManager.GetComponent<RandomEvent>();
        lastPosition = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        soundEffect = soundManager.GetComponent<SoundManager>();
        Invoke("GoBall", 2);
    }
    
    void GoBall()
    {
        randEvent.StartCountdown();
        Vector2 randomDirection;

        do
        {
            randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        while (randomDirection.x == 0 && randomDirection.y == 0);

        rb2d.velocity = randomDirection.normalized * initialSpeed;

    }

    public void ResetBall()
    {
        randEvent.PauseResetCountdown();
        randEvent.PauseResetEventCountdown();
        randEvent.resetEvent();
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }
    
    public void ballGoneWhenWin()
    {
        //prevent visual bug for winning while vanishing ball is active
        vanishingBall.eventActive = 0;
        vanishingBall.resetVanishingBall();
        //end of visual bug fix

        Color newColor = spriteRenderer.color;
        newColor.a = 0f;
        spriteRenderer.color = newColor;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        soundEffect.PlaySoundEffect();
        // Instantiate an object at the position of collision
        GameObject instantiatedObject = Instantiate(explosion, transform.position, Quaternion.identity);
        SpriteRenderer explosionLayerOrder = instantiatedObject.GetComponent<SpriteRenderer>();
        explosionLayerOrder.sortingOrder = 10;
        // Destroy the instantiated object after 1 second
        Destroy(instantiatedObject, 1f);

        if (coll.collider.CompareTag("Player")) //jika terkena player
        {

            // StartCoroutine(FireTriggger());
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3); //mengambil nilai velocity player
            rb2d.velocity = vel;

        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.magnitude > maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
        if (rb2d.velocity.magnitude < initialSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * initialSpeed;
        }

        if(winCond == 1)
        {
            countdownText.text = "";
            eventTitle.text = "";
            eventDesc.text = "";
            eventCountdown.text = "";
            ResetBall();
        }
    }

    public void IncreaseSpeed()
    {
        rb2d.velocity += rb2d.velocity.normalized * speedIncreaseAmount;
    }

    void FixedUpdate()
    {
        // Calculate the difference in positions
        Vector3 positionDifference = transform.position - lastPosition;

        // If the position difference is significant, calculate new target rotation
        if (positionDifference.magnitude > 0.001f)
        {
            // Calculate the rotation angle based on the position difference
            float angle = Mathf.Atan2(positionDifference.y, positionDifference.x) * Mathf.Rad2Deg;

            // Create a rotation quaternion based on the angle
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            // Smoothly rotate towards the target rotation using Slerp
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Update last position
        lastPosition = transform.position;
    }
}

