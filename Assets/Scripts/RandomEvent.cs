using UnityEngine;
using TMPro;

public class RandomEvent : MonoBehaviour
{
    public float countdownDuration = 5f;
    public float countdownEventDuration = 3f;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI eventTitle;
    public TextMeshProUGUI eventDesc;
    public TextMeshProUGUI eventCountdown;
    public GameObject playerLeft;
    public GameObject playerRight;
    public GameObject ball;
    private PlayerControl controlLeft;
    private PlayerControl controlRight;
    private VanishingBall vanishBall;
    private BallControl controlBall;
    private Transform transformLeft;
    private Transform transformRight;

    private int counterFlipControl;
    private int counterLightningSpeed;
    private int counterVanishingBall;
    private int counterMiniYou;

    private float countdownTimer;
    private float countdownEventTimer;

    private int randomValue;
    
    private bool countdownActive = false;
    private bool countdownEventActive = false;

    // Start is called before the first frame update
    void Start()
    {
        counterFlipControl = 0;
        counterLightningSpeed = 0;
        counterVanishingBall = 0;
        counterMiniYou = 0;

        controlLeft = playerLeft.GetComponent<PlayerControl>();
        controlRight = playerRight.GetComponent<PlayerControl>();
        vanishBall = ball.GetComponent<VanishingBall>();
        controlBall = ball.GetComponent<BallControl>();
        transformLeft = playerLeft.GetComponent<Transform>();
        transformRight = playerRight.GetComponent<Transform>();

        resetText();
    }

    // Update is called once per frame
    void Update()
    {
        countdownEventGenerate();
        countdownEvent();
    }

    public void resetEvent()
    {
        if(counterFlipControl == 1)
        {
            FlipControl();
        }
        if(counterLightningSpeed == 1)
        {
            LightningSpeed();
        }
        if(counterVanishingBall == 1)
        {
            VanishingBall();
        }
        if(counterMiniYou == 1)
        {
            MiniYou();
        }
    }

    void resetText()
    {
        countdownText.text = "";
        eventTitle.text = "";
        eventDesc.text = "";
        eventCountdown.text = "";
    }
    void countdownEventGenerate()
    {
        if (countdownActive)
        {
            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0f)
            {
                resetText();
                GenerateRandomNumber();
                StartEventCountdown();
                PauseResetCountdown();
            }

            countdownText.text = "Next event in " + Mathf.CeilToInt(countdownTimer).ToString() + " ...";
            eventTitle.text = "";
            eventDesc.text = "";
            eventCountdown.text = "";
        }
    }

    void countdownEvent()
    {
        if (countdownEventActive)
        {
            countdownEventTimer -= Time.deltaTime;

            if (countdownEventTimer <= 0f)
            {
                resetText();
                BeginEvent();
                StartCountdown();
                PauseResetEventCountdown();
            }

            if(randomValue == 1)
            {
                if(counterFlipControl == 1)
                {
                    eventTitle.text = "Flip Control";
                    eventDesc.text = "This effect will be reverted in";
                }
                else
                {
                    eventTitle.text = "Flip Control";
                    eventDesc.text = "Both player will have their controls flipped in";
                }
            }else if (randomValue == 2)
            {
                if(counterLightningSpeed == 1)
                {
                    eventTitle.text = "Lightning Speed";
                    eventDesc.text = "This effect will be reverted in";
                }
                else
                {
                    eventTitle.text = "Lightning Speed";
                    eventDesc.text = "Both player will have their speed increased in";
                }
            }
            else if (randomValue == 3)
            {
                if(counterVanishingBall == 1)
                {
                    eventTitle.text = "Vanishing Ball";
                    eventDesc.text = "This effect will be reverted in";
                }
                else
                {
                    eventTitle.text = "Vanishing Ball";
                    eventDesc.text = "The ball will gradually vanish in";
                }
            }
            else if (randomValue == 4)
            {
                eventTitle.text = "Sonic Ball";
                eventDesc.text = "The ball speed will increase in";
            }
            else if (randomValue == 5)
            {
                if(counterMiniYou == 1)
                {
                    eventTitle.text = "Mini You";
                    eventDesc.text = "This effect will be reverted in";
                }
                else
                {
                    eventTitle.text = "Mini You";
                    eventDesc.text = "The player's size will decrease in";
                }
            }

            countdownText.text = "";
            eventCountdown.text = Mathf.CeilToInt(countdownEventTimer).ToString();

        }
    }

    // Function to start the countdown
    public void StartCountdown()
    {
        countdownActive = true;
        countdownTimer = countdownDuration;
    }

    public void StartEventCountdown()
    {
        countdownEventActive = true;
        countdownEventTimer = countdownEventDuration;
        
    }

    // Function to pause/reset the countdown
    public void PauseResetCountdown()
    {
        countdownActive = false;
        countdownTimer = countdownDuration;
    }

    public void PauseResetEventCountdown()
    {
        countdownEventActive = false;
        countdownEventTimer = countdownEventDuration;
    }

    void GenerateRandomNumber()
    {
        randomValue = Random.Range(1, 6);
    }

    void BeginEvent()
    {

        if (randomValue == 1)
        {
            FlipControl();
        }
        else if (randomValue == 2)
        {
            LightningSpeed();
        }
        else if (randomValue == 3)
        {
            VanishingBall();
        }
        else if (randomValue == 4)
        {
            SonicBall();
        }
        else if (randomValue == 5)
        {
            MiniYou();
        }
    }

    void FlipControl()
    {
        if (counterFlipControl == 0)
        {
            counterFlipControl = 1;
        }
        else
        {
            counterFlipControl = 0;
        }
        KeyCode lL = controlLeft.moveUp;
        KeyCode lR = controlLeft.moveDown;
        KeyCode rL = controlRight.moveUp;
        KeyCode rR = controlRight.moveDown;

        controlLeft.moveUp = lR;
        controlLeft.moveDown = lL;
        controlRight.moveUp = rR;
        controlRight.moveDown = rL;
    }

    void LightningSpeed()
    {
        if (counterLightningSpeed == 0)
        {
            controlLeft.speed = 20;
            controlRight.speed = 20;
            counterLightningSpeed = 1;
        }
        else
        {
            controlLeft.speed = 10;
            controlRight.speed = 10;
            counterLightningSpeed = 0;
        }
    }

    void VanishingBall()
    {
        if(counterVanishingBall == 0)
        {
            counterVanishingBall = 1;
        }
        else
        {
            counterVanishingBall = 0;
        }

        vanishBall.activateVanishingBall();
    }

    void SonicBall()
    {
        controlBall.IncreaseSpeed();
    }

    void MiniYou()
    {
        Vector3 originalScale = new Vector3(0.3545533f, 0.3545533f, 0.3545533f);
        Vector3 newScale = new Vector3(0.21f, 0.21f, 0.21f);
        
        if(counterMiniYou == 0)
        {
            counterMiniYou = 1;
            transformLeft.localScale = newScale;
            transformRight.localScale = newScale;
            controlLeft.boundY = 4.7f;
            controlRight.boundY = 4.7f;
            
        }
        else
        {
            counterMiniYou = 0;
            transformLeft.localScale = originalScale;
            transformRight.localScale = originalScale;
            controlLeft.boundY = 4.2f;
            controlRight.boundY = 4.2f;
        }
        
    }
}