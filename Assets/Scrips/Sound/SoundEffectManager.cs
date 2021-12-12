using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static AudioClip coinCollectedSound, kirbyDiedSound, kirbyJumpSound, kirbySwordSound;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        coinCollectedSound = Resources.Load<AudioClip>("SoundEffects/CoinCollected");
        kirbyDiedSound = Resources.Load<AudioClip>("SoundEffects/CollisionWIthEnemy");
        kirbyJumpSound = Resources.Load<AudioClip>("SoundEffects/KirbyJump");
        kirbySwordSound = Resources.Load<AudioClip>("SoundEffects/KirbyPunch");

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        }
    }
}
