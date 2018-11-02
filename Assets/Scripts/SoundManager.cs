using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SoundManager : MonoBehaviour
{

    /*public AudioClip GameSound, StartSound, ExplosionSound, EnemySound, GrapeFruitJokerSound, RadishJokerSound, BroccoliJokerSound, BeerJokerSound, ResetSound, 
        ButtonSound, CoinSound, AlertSound;*/
    public bool isMuted = false;


    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;


    // Singleton instance.
    public static SoundManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


    // Play a single clip through the sound effects source.
    public void Play(string clip)
    {

        switch (clip)
        {
            /*
            case "Explosion": //explosion'un bir kere oynaması için.
                EffectsSource.clip = ExplosionSound;
                EffectsSource.Play();
                break;

            case "Enemy":
                EffectsSource.clip = EnemySound;
                EffectsSource.Play();
                break;

            case "GrapeFruitJoker":
                EffectsSource.clip = GrapeFruitJokerSound;
                EffectsSource.Play();
                break;

            case "BeerJoker":
                EffectsSource.clip = BeerJokerSound;
                EffectsSource.Play();
                break;

            case "BroccoliJoker":
                EffectsSource.clip = BroccoliJokerSound;
                EffectsSource.Play();
                break;

            case "Start":
                EffectsSource.clip = StartSound;
                EffectsSource.Play();
                break;

            case "Reset": 
                EffectsSource.clip = ResetSound;
                EffectsSource.Play();
                break;

            case "Button": 
                EffectsSource.clip = ButtonSound;
                EffectsSource.Play();
                break;

            case "Alert":
                EffectsSource.clip = AlertSound;
                EffectsSource.Play();
                break;
                */
        }
    }

    // Play a single clip through the music source.
    public void PlayMusic(string clip)
    {

        switch (clip)
        {
            /*
            case "GameSound":
                MusicSource.clip = GameSound;
                MusicSource.Play();
                break;

            case "RadishJoker": //Shield joker belirli bir süre oynayacağı için PlayOneShot olmaz.
                MusicSource.clip = RadishJokerSound;
                MusicSource.Play();
                break;

            case "CoinSound":
                MusicSource.clip = CoinSound;
                MusicSource.Play();
                break;
                */
        }
    }
}
