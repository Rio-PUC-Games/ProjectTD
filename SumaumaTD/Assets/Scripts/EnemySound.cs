﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EnemySound : MonoBehaviour {
    
    [Tooltip("Som que toca em loop enquanto o inimigo está no mapa")] public AudioClip EnemyLoopSound;
    public float EnemyLoopSoundVolume = 0.5f;
    [Tooltip("Som que toca quando o inimigo atinge a Sumauma")] public AudioClip EnemyDoesDamageSound;
    public float EnemyDoesDamageSoundVolume = 2;

    private AudioSource _audioSource;
    private float _enemyLoopSoundLength;
    private float _timeLeftToPlayAgain = 0f;
    private bool _enemyReachedEnd = false;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
        if (EnemyLoopSound != null) _enemyLoopSoundLength = EnemyLoopSound.length;
	}
	
	// Update is called once per frame
	void Update () {
        if (EnemyLoopSound != null)
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

    public void EnemyReachedEnd()
    {
        _enemyReachedEnd = true;
        
        //Plays the last sound at the enemies position
        if(EnemyDoesDamageSound != null) AudioSource.PlayClipAtPoint(EnemyDoesDamageSound, transform.position, EnemyDoesDamageSoundVolume);
    }
}