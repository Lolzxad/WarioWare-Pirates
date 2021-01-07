﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpanishInquisition
{
    namespace Duel
    {
        public class SoundManager : MonoBehaviour
        {

            private static SoundManager _instance;
            public static SoundManager instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<SoundManager>();
                    }
                    return _instance;
                }
            }

            private AudioSource[] gameSounds;

            [SerializeField]
            private AudioSource parry;
            [SerializeField]
            private AudioSource fail;
            [SerializeField]
            private AudioSource victorySound;
            [SerializeField]
            private AudioSource defeatSound;
            [SerializeField]
            private AudioSource enemyAttack;

            [SerializeField]
            private AudioSource musicSlow;
            [SerializeField]
            private AudioSource musicMedium;
            [SerializeField]
            private AudioSource musicFast;
            [SerializeField]
            private AudioSource musicSuperFast;


            // Start is called before the first frame update
            void Start()
            {
                gameSounds = GetComponents<AudioSource>();

                parry = gameSounds[0];
                fail = gameSounds[1];
                victorySound = gameSounds[2];
                defeatSound = gameSounds[3];
                enemyAttack = gameSounds[4];            
                musicSlow = gameSounds[5];
                musicMedium = gameSounds[6];
                musicFast = gameSounds[7];
                musicSuperFast = gameSounds[8];
            }

            public void Playparry()
            {
                parry.Play();
                Debug.Log("parry play.");
            }

            public void Playfail()
            {
                fail.Play();
                Debug.Log("fail play");
            }

            public void PlayKatana()
            {
                //katanaCut.Play();
                Debug.Log("Son Katana");
            }

            public void PlayVictory()
            {
                victorySound.Play();
                Debug.Log("Son Victoire");
            }

            public void PlayDefeat()
            {
                defeatSound.Play();
                Debug.Log("Son défaite");
            }

            public void PlayObjectThrown()
            {
                //objectThrown.Play();
                Debug.Log("Son objet lancé");
            }

            public void PlayFlagMusicSlow()
            {
                musicSlow.Play();
            }

            public void PlayFlagMusicMedium()
            {
                musicMedium.Play();
            }

            public void PlayFlagMusicFast()
            {
                musicFast.Play();
            }

            public void PlayFlagMusicSuperFast()
            {
                musicSuperFast.Play();
            }

            public void PlayBombNoise()
            {
                //bombNoise.Play();
                Debug.Log("Son bombe");
            }
        }
    }
}