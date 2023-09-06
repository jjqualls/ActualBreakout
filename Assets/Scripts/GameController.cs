using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    //SerializeField = can be visible in unity but it stays private
    [SerializeField]private PlayerInput playerInput;
    private InputAction move;
    private InputAction restart;
    private InputAction quit;

    //flags when this happens
    private bool isPaddleMoving;

    //reference to paddle
    [SerializeField]private GameObject paddle;

    //speed var
    [SerializeField]private float paddleSpeed;

    //in charge of directions
    private float moveDirection;

    //the bricks
    [SerializeField] private GameObject brick;

    //the score
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int score;

    //end screen
    [SerializeField] private TMP_Text endGameText;

    private BallControl ball;

    // Start is called before the first frame update
    void Start()
    {

        DefinePlayerInput();
        CreateAllBricks();

        endGameText.gameObject.SetActive(false);

        ball = GameObject.FindObjectOfType<BallControl>();
    }

    public void UpdateScore()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();

        if(score >= 4000)
        {
            endGameText.text = "YOU WIN!!!";
            endGameText.gameObject.SetActive(true);
            ball.ResetBall();
        }
    }
    private void CreateAllBricks()
    {
        Vector2 brickPos = new Vector2(-9, 4.5f);

        for (int j = 0; j < 4; j++)
        {
            brickPos.y -= 1;
            brickPos.x = -9;
            for (int i = 0; i < 10; i++)
            {
                brickPos.x += 1.6f;
                Instantiate(brick, brickPos, Quaternion.identity);
            }
        }
    }

    private void DefinePlayerInput()
    {
        //activating the action map
        playerInput.currentActionMap.Enable();

        //initialized input actions
        move = playerInput.currentActionMap.FindAction("MovePaddle");
        restart = playerInput.currentActionMap.FindAction("RestartGame");
        quit = playerInput.currentActionMap.FindAction("QuitGame");

        //listener buttons
        move.started += Move_started;

        //stop when let go of button
        move.canceled += Move_canceled;

        restart.started += Restart_started;
        quit.started += Quit_started;

        //start off false
        isPaddleMoving = false;

    }
    private void Move_started(InputAction.CallbackContext obj)
    {
        isPaddleMoving = true;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isPaddleMoving = false;
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {
        
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        
    }

    private void FixedUpdate()
    {
        if (isPaddleMoving)
        {
            //move paddle (pos = right, neg = left)
            paddle.GetComponent<Rigidbody2D>().velocity = new Vector2(paddleSpeed * moveDirection, 0);
        }
        else
        {
            //stop paddle (no velocity)
            paddle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaddleMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
    }
}
