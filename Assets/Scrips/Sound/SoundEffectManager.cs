//-------------------------SoundEffectManager.cs-----------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for playing background sounds
//  Revision History : 1.0 Added static function and variables

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sound effect manager class
public class SoundEffectManager : MonoBehaviour
{
    public static AudioClip coinCollectedSound, kirbyDiedSound, kirbyJumpSound, kirbySwordSound, landingSoundEffect, enemyKilledSoundEffect;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        coinCollectedSound = Resources.Load<AudioClip>("SoundEffects/CoinCollected");
        kirbyDiedSound = Resources.Load<AudioClip>("SoundEffects/CollisionWIthEnemy");
        kirbyJumpSound = Resources.Load<AudioClip>("SoundEffects/KirbyJump");
        kirbySwordSound = Resources.Load<AudioClip>("SoundEffects/KirbyPunch");
        landingSoundEffect = Resources.Load<AudioClip>("SoundEffects/LandingSoundEffect");
        enemyKilledSoundEffect = Resources.Load<AudioClip>("SoundEffects/EnemyKilledSoundEffect");

        audioSource = GetComponent<AudioSource>();

    }
    //Static function that can play background sounds
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Coin":
                audioSource.PlayOneShot(coinCollectedSound, 0.1f);
                break;
            case "Death":
                audioSource.PlayOneShot(kirbyDiedSound, 0.1f);
                break;
            case "Jump":
                audioSource.PlayOneShot(kirbyJumpSound, 0.1f);
                break;
            case "Hit":
                audioSource.PlayOneShot(kirbySwordSound, 0.1f);
                break;
            case "Landing":
                audioSource.PlayOneShot(landingSoundEffect, 0.05f);
                break;
            case "EnemyKilled":
                audioSource.PlayOneShot(enemyKilledSoundEffect, 0.05f);
                break;
        }
    }
}
