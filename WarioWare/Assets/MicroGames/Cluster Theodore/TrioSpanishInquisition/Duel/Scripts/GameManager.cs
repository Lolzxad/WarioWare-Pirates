﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace SpanishInquisition
{

    namespace Duel
    {

        /// <summary>
        /// Adel Ahmed-Yahia
        /// </summary>

        public class GameManager : TimedBehaviour
        {
            private static GameManager _instance;
            public static GameManager instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<GameManager>();
                    }
                    return _instance;
                }
            }

            public GameObject[] scoreDisplays;
            public GameObject parryButton1;
            public GameObject parryButton2;
            public GameObject parryButton3;
            public GameObject victoryFeedback;
            public GameObject defeatFeedback;

            private int numberOfParries;
            public int parriesNeeded = 2;
            public int currentParryButton;
            public int neutralTime;
            public int tickBeforeNextParry;
            public float tickTimer;
            public float cooldownTime;
            public bool gameIsWon;
            public bool gameIsFinished;
            public bool endFeedbackPlayed;
            public float speed;
            public float cooldown;


            private SoundManager soundManager;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                speed = bpm / 5;
                soundManager = GetComponentInChildren<SoundManager>();


                switch (bpm)
                {
                    case 60:
                        soundManager.PlayFlagMusicSlow();
                        break;

                    case 90:
                        soundManager.PlayFlagMusicMedium();
                        break;

                    case 120:
                        soundManager.PlayFlagMusicFast();
                        break;

                    case 140:
                        soundManager.PlayFlagMusicSuperFast();
                        break;
                }

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:

                        neutralTime = 2;
                        parriesNeeded = 2;
                        break;

                    case Difficulty.MEDIUM:

                        neutralTime = 1;
                        parriesNeeded = 3;
                        break;

                    case Difficulty.HARD:

                        neutralTime = 0;
                        parriesNeeded = 6;
                        break;
                }
                tickBeforeNextParry = neutralTime;
                //DisplayScore();               
            }

            public void Update()
            {

                UpdateParry();

                if (numberOfParries >= parriesNeeded && !gameIsFinished)
                {
                    gameIsFinished = true;
                    gameIsWon = true;
                    if (!endFeedbackPlayed)
                    {
                        EndOfGameFeedback();
                        endFeedbackPlayed = true;
                    }                   
                }
            }

            //TimedUpdate is called once every tick.

            public override void TimedUpdate()
            {
                base.TimedUpdate();

                Debug.Log(Tick);

                if ((Tick < 8 && !gameIsWon) && !gameIsFinished)
                {
                    if (currentParryButton != 0)
                    {
                        Fail();
                        currentParryButton = 0;
                    }
                    else
                    {
                        if (tickBeforeNextParry > 0)
                        {
                            tickBeforeNextParry--;
                        }
                        else
                        {
                            ParryRandomizer();
                        }
                    }
                }

                if (Tick == 8)
                {
                    Manager.Instance.Result (gameIsWon);
                }

                /*if ((Tick == 8 && !gameIsWon) || gameIsFinished)
                {
                    
                }*/
            }

            private void UpdateParry()
            {
                switch (currentParryButton)
                {
                    case 1:
                        if (Input.GetButtonDown("X_Button"))
                        {
                            Parry();
                        }

                        if (Input.GetButtonDown("Y_Button") || Input.GetButtonDown("B_Button"))
                        {
                            Fail();
                        }
                        break;

                    case 2:
                        if (Input.GetButtonDown("Y_Button"))
                        {
                            Parry();
                        }

                        if (Input.GetButtonDown("X_Button") || Input.GetButtonDown("B_Button"))
                        {
                            Fail();

                        }
                        break;

                    case 3:
                        if (Input.GetButtonDown("B_Button"))
                        {
                            Parry();
                        }

                        if (Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                        {
                            Fail();
                        }
                        break;

                    default:
                        break;
                }

                if(currentParryButton >= 1 && currentParryButton <= 3 && Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button") || Input.GetButtonDown("B_Button"))
                {
                    parryButton1.SetActive(false);
                    parryButton2.SetActive(false);
                    parryButton3.SetActive(false);
                    currentParryButton = 0;
                    tickBeforeNextParry = neutralTime;
                }
            }

            private void Parry()
            {
                Debug.Log("Parry !");
                numberOfParries++;
            }

            private void Fail()
            {
                Debug.Log("Fail !");
                gameIsFinished = true;
                EndOfGameFeedback();
            }

            private void ParryRandomizer()
            {
                currentParryButton = Random.Range(1, 4);

                if (currentParryButton == 1)
                {
                    parryButton1.SetActive(true);
                }

                if (currentParryButton == 2)
                {
                    parryButton2.SetActive(true);
                }

                if (currentParryButton == 3)
                {
                    parryButton3.SetActive(true);
                }
            }


            public void EndOfGameFeedback()
            {
                parryButton1.SetActive(false);
                parryButton2.SetActive(false);
                parryButton3.SetActive(false);

                if (gameIsWon)
                {
                    //Win feedback
                    soundManager.PlayVictory();
                    //victoryFeedback.SetActive(true);
                }
                else
                {
                    //Loss feedback
                    soundManager.PlayDefeat();
                    //defeatFeedback.SetActive(true);
                }
            }

            private IEnumerator StartCooldown()
            {
                yield return new WaitForSeconds(cooldownTime);
            }

            /*public bool HasWon()
            {
                bool allObjectsPassed = true;
                bool allObjectsOutOfZone = true;

                foreach (ObjectMovement objMovement in activeObjects)
                {
                    if (!objMovement.hasBeenInZone)
                    {
                        allObjectsPassed = false;
                    }
                }
                foreach (ObjectMovement objMovement in activeObjects)
                {
                    if (objMovement.InZone())
                    {
                        allObjectsOutOfZone = false;
                    }
                }
                Debug.Log(allObjectsOutOfZone);
                return allObjectsPassed && allObjectsOutOfZone && objectSpawned >= objectsNumber && fruitsRemaining == 0;
            }

            /*public void DisplayScore()
            {
                // From int
                foreach (GameObject display in scoreDisplays)
                {
                    display.SetActive(false);
                }

                scoreDisplays[fruitsRemaining].SetActive(true);
            }

            private IEnumerator StartCooldown()
            {
                canCut = false;
                yield return new WaitForSeconds(cooldownTime);
                canCut = true;
            }*/
        }      
    }
}