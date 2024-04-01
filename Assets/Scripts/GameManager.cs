using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PlayerScoreL = 0;
    public int PlayerScoreR = 0;

    public TMP_Text txtPlayerScoreL;
    public TMP_Text txtPlayerScoreR;
    public TMP_Text winText;

    public Color32 blueCol;
    public Color32 orangeCol;

    public GameObject ball;
    private BallControl ballControl;

    public static GameManager instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        winText.text = "";

        ballControl = ball.GetComponent<BallControl>();
        txtPlayerScoreL.text = PlayerScoreL.ToString();
        txtPlayerScoreR.text = PlayerScoreR.ToString();
    }

    public void Score(string wallID)
    {
        if (wallID == "Line L")
        {
            PlayerScoreR = PlayerScoreR + 10;
            txtPlayerScoreR.text = PlayerScoreR.ToString();
            ScoreCheck();
        }
        if (wallID =="Line R")
        {
            PlayerScoreL = PlayerScoreL + 10;
            txtPlayerScoreL.text = PlayerScoreL.ToString();
            ScoreCheck();
        }
    }

    public void ScoreCheck()
    {
        if (PlayerScoreL == 20)
        {
            ballControl.ballGoneWhenWin();
            ballControl.winCond = 1;
            ballControl.ResetBall();
            txtPlayerScoreL.text = "";
            txtPlayerScoreR.text = "";
            winText.color = blueCol;
            winText.text = "Blue witch wins!";
            Invoke("backToMenu", 5);
        }
        else if (PlayerScoreR == 20)
        {
            ballControl.ballGoneWhenWin();
            ballControl.winCond = 1;
            ballControl.ResetBall();
            txtPlayerScoreL.text = "";
            txtPlayerScoreR.text = "";
            winText.color = orangeCol;
            winText.text = "Orange witch wins!";
            Invoke("backToMenu", 5);
        }
    }

    public void backToMenu()
    {
        this.gameObject.SendMessage("ChangeScene", "MainMenu");
    }
}
