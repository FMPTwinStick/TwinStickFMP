using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    public static AudioManager GetAudioManager()
    {
        return instance;
    }

    //AudioClips to be dragged in via the inspector:
    public AudioClip tankHitSound;
    public AudioClip tankFireSound;
    public AudioClip tankMoveSound;
    public AudioClip placeMineSound;

    public AudioClip music;

    //AudioSource:
    private AudioSource audioSource;

    //World pitch variable for audio sources to use:
    private float m_worldPitch;

    //Making the AudioManager a singleton:
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Initialising AudioSource:
        audioSource = gameObject.GetComponent<AudioSource>();

        //Initilising variables:
        m_worldPitch = 1f;

        //Play Music:

        audioSource.loop = true;
        audioSource.clip = music;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = m_worldPitch;
    }

    //Getter and setter for world pitch:
    public float GetWorldPitch()
    {
        return m_worldPitch;
    }

    public void SetWorldPitch(float newPitch)
    {
        m_worldPitch = newPitch;
    }

    //Play Sound Functions:
    public void PlayTankHitSound()
    {
        audioSource.PlayOneShot(tankHitSound);
    }

    public void PlayTankFireSound()
    {
        audioSource.PlayOneShot(tankFireSound);
    }

    public void PlayPlaceMineSound()
    {
        audioSource.PlayOneShot(placeMineSound);
    }
}
