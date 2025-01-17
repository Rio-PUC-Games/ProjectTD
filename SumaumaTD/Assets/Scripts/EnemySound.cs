﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemySound : MonoBehaviour
    {
		[Tooltip("Som que toca quando o inimigo entra no mapa")]
		public AudioClip EnemyEntranceSound;
		public float EnemyEntranceSoundVolume = 0.75f;

        [Tooltip("Som que toca em loop enquanto o inimigo está no mapa")]
        public AudioClip EnemyLoopSound;
        public float EnemyLoopSoundVolume = 0.5f;
        [Tooltip("Som que toca quando o inimigo atinge a Sumauma")]
        public AudioClip EnemyDoesDamageSound;
        public float EnemyDoesDamageSoundVolume = 2;
        public AudioClip EnemyDiedSound;
        public float EnemyDiedSoundVolume = 1;

        private AudioSource _audioSource;
        private float _enemyLoopSoundLength;
        private float _timeLeftToPlayAgain = 0f;
        private bool _enemyReachedEnd = false;


        // Use this for initialization
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (EnemyLoopSound != null)
            {
                _enemyLoopSoundLength = EnemyLoopSound.length;
            }

            if(EnemyEntranceSound != null)
                _audioSource.PlayOneShot(EnemyEntranceSound, EnemyEntranceSoundVolume);

            GroupSound.GetInstance.EnemyEnter(EnemyLoopSound);
        }

        /*
        // Update is called once per frame
        void Update()
        {
            if (EnemyLoopSound != null && !GameManager.GameIsOver)
            {
                //Checa se o áudio do loop já terminou e, se já tiver, reinicia ele
                if (_timeLeftToPlayAgain <= 0 && !_enemyReachedEnd)
                {
                    _audioSource.PlayOneShot(EnemyLoopSound, EnemyLoopSoundVolume);
                    _timeLeftToPlayAgain = _enemyLoopSoundLength;
                }

                _timeLeftToPlayAgain -= Time.deltaTime;
            }
        }
        */

        public void EnemyReachedEnd()
        {
            _enemyReachedEnd = true;
            GroupSound.GetInstance.EnemyLeft(EnemyLoopSound);

            //Plays the last sound at the enemies position
            if (EnemyDoesDamageSound != null) AudioSource.PlayClipAtPoint(EnemyDoesDamageSound, Camera.current.transform.position, EnemyDoesDamageSoundVolume);
        }

        public void EnemyDied()
        {
            GroupSound.GetInstance.EnemyLeft(EnemyLoopSound);

            if (EnemyDiedSound != null)
            {
                AudioSource.PlayClipAtPoint(EnemyDiedSound, Camera.current.transform.position, EnemyDiedSoundVolume);
            }
        }
    }
}
