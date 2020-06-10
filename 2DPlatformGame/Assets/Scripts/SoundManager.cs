using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip Jump, GameOver, Crouch, Background;
    private static AudioSource _AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        Background = Resources.Load<AudioClip>("background");
        Jump = Resources.Load<AudioClip>("jump");
        GameOver = Resources.Load<AudioClip>("gameOver");
        _AudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string State)
    {
        switch (State)
        {
            case "Background":
                _AudioSource.PlayOneShot(Background);
                break;
            case "Jump":
                _AudioSource.PlayOneShot(Jump);
                break;
            case "GameOver":
                _AudioSource.PlayOneShot(GameOver);
                break;
            default:
                break;
        }
    }
}
