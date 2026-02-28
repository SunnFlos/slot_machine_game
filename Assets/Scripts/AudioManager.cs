using UnityEngine;

public class AudioManager : MonoBehaviour
{   public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip buttonClip, LossAudioClip, WinAudioClip;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buttonclick()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = buttonClip;
            audioSource.Play();
        }
    }
    public void WinSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = WinAudioClip;
            audioSource.Play();
        }
    }
    public void LossSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = LossAudioClip;
            audioSource.Play();
        }
    }
}
