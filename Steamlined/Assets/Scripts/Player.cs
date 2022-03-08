using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody body;
    public bool playerFireState = false; //false = water, true = fire
    public float moveSpeedZ;
    public float moveSpeedX;
    public int previousZInput;
    public float extraGravity;
    public float jumpForce;
    public LaneState currentLane = LaneState.Center;
    bool isMoving;
    public bool isJumping;
    public static int currentScore;

    public AudioSource MainMusic;
    public AudioSource DeathMusic;

    public static bool isGameOver;

    public GameObject GameOverScreen;
    public TextMeshProUGUI FinalScoreText;
    public TextMeshProUGUI CurrentScoreText;
    public TextMeshProUGUI BestScoreText;

    //private RaycastHit raycastHit;

    private Vector3 changeOrigin = new Vector3(0, -2, 0);
    private Vector3 raycastDirection = new Vector3(0,-1,0);
    private float raycastDistance = 3.0f;
    private bool setGravityOnce = false;

    #region Elemental body

    public GameObject fireBody;
    public GameObject waterBody;
    public GameObject steam;
    public float bounceFactor;
    public float bounceFrequency;
    public float steamCountDown;
    private bool GameOverTriggered;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        this.fireBody.SetActive(false);
        SoundStore.playedDeathSFX = false;
    }

    // Update is called once per frame
    void Update()
    {

        Ray raycast = new Ray(transform.position - changeOrigin, raycastDirection);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit, raycastDistance))
        {

            Debug.DrawLine(raycast.origin, raycastHit.point, Color.red);
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")))
            {
                this.body.AddForce(Vector3.up * this.jumpForce, ForceMode.Impulse);
            }

            switch (setGravityOnce) {

                case false:
                    this.body.useGravity = false;
                    
                    this.body.velocity = new Vector3(this.body.velocity.x, 0, 0);
                    this.setGravityOnce = true;
                    break;

                case true:

                    break;
            }





        } else
        {
            this.setGravityOnce = false;
            this.body.useGravity = true;
            Debug.DrawLine(raycast.origin, raycastHit.point, Color.blue);
            if (this.transform.position.y > 0.5f)
            {
                this.body.AddForce(Vector3.down * this.extraGravity * Time.deltaTime, ForceMode.Acceleration);
            }
        }
        


        switch (Player.isGameOver) 
        {
            case false:

                this.steamCountDown = 1.5f;
                Player.currentScore = (int)this.body.transform.position.x;
                this.CurrentScoreText.text = "Distance: " + Player.currentScore.ToString() + "m";
                this.FinalScoreText.text = "Final Distance:\n" + Player.currentScore.ToString() + "m";
                if (PlayerPrefs.GetInt("bestDistance") > 0)
                    this.BestScoreText.text = "Best Distance: " + PlayerPrefs.GetInt("bestDistance") + "m";


                if (this.moveSpeedX < 125)
                {
                    this.moveSpeedX += (this.moveSpeedX * 0.005f) * Time.deltaTime;
                    //this.moveSpeedX = 125;
                }
                int zInput;

                if (Input.GetAxis("Horizontal") * 100 > 10)
                    zInput = 1;
                else if (Input.GetAxis("Horizontal") * 100 < -10)
                    zInput = -1;
                else
                    zInput = 0;



                if (zInput != 0 && !this.isMoving && this.previousZInput != zInput)
                {
                    this.isMoving = true;
                    switch (this.currentLane)
                    {
                        case LaneState.Left:
                            if (zInput == 1)
                            {
                                this.currentLane = LaneState.Center;
                            }
                            break;
                        case LaneState.Right:
                            if (zInput == -1)
                            {
                                this.currentLane = LaneState.Center;
                            }
                            break;
                        case LaneState.Center:
                            if (zInput == -1)
                            {
                                this.currentLane = LaneState.Left;
                            }
                            else if (zInput == 1)
                            {
                                this.currentLane = LaneState.Right;
                            }
                            break;
                    }
                }
                if ((Math.Abs(this.transform.position.z - (int)this.currentLane) < 0.5f))
                    this.isMoving = false;
                this.previousZInput = zInput;

                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, (int)this.currentLane), this.moveSpeedZ * Time.deltaTime);

                this.body.velocity = new Vector3(this.moveSpeedX, this.body.velocity.y, this.body.velocity.z);

//               if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && this.transform.position.y < -0.40f && !this.isJumping)
//               {
//                   this.body.AddForce(Vector3.up * this.jumpForce, ForceMode.Impulse);
//                   this.isJumping = true;
//               }
//               if (this.body.velocity.y < 0.05f)
//               {
//                   this.isJumping = false;
//               }


                //if (this.transform.position.y > 0.5f)
                //{
                //    this.body.AddForce(Vector3.down * this.extraGravity * Time.deltaTime, ForceMode.Acceleration);
                //}

                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("ChangeState"))
                {
                    switch (this.playerFireState)
                    {
                        case false:
                            this.playerFireState = true;
                            break;
                        case true:
                            this.playerFireState = false;
                            break;
                    }
                }

                switch (this.playerFireState)
                {
                    case false:
                        this.fireBody.SetActive(false);
                        this.waterBody.SetActive(true);
                        break;
                    case true:
                        this.fireBody.SetActive(true);
                        this.waterBody.SetActive(false);
                        break;
                }

                float fireBounceFactor = (float)(this.bounceFactor + 0.05);

                this.fireBody.transform.localScale = new Vector3((float)(1 - fireBounceFactor + (fireBounceFactor * Math.Sin(Time.realtimeSinceStartup * this.bounceFrequency))), (float)(1 - this.bounceFactor + (this.bounceFactor * Math.Sin(Time.realtimeSinceStartup * (this.bounceFrequency + 5)))), 1);
                this.waterBody.transform.localScale = new Vector3(1, (float)(1 - this.bounceFactor + (this.bounceFactor * Math.Sin(Time.realtimeSinceStartup * this.bounceFrequency))), 1);
                break;

            case true:

                this.GameOverScreen.SetActive(true);
                this.CurrentScoreText.text = null;
                this.BestScoreText.text = null;

                if (!this.GameOverTriggered)
                {
                    this.GameOverTriggers();
                }

                this.steamCountDown -= Time.deltaTime;

                if (this.steamCountDown <= 0)
                    this.steam.GetComponent<ParticleSystem>().Stop();
                break;
        
        }
       
    }


    public static void GameOver()
    {

        Player.isGameOver = true;
    }

    private void GameOverTriggers()
    {
        this.GameOverTriggered = true;
        this.steam.GetComponent<ParticleSystem>().Play();
        this.fireBody.SetActive(false);
        this.waterBody.SetActive(false);
        this.DeathMusic.Play();

        if (Player.currentScore > PlayerPrefs.GetInt("bestDistance"))
        {
            //ScoreStore.bestDistance = Player.currentScore;
            PlayerPrefs.SetInt("bestDistance", Player.currentScore);
        }

        this.MainMusic.Stop();
    }
}
