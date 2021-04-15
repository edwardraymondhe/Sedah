using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioController : MonoBehaviour
{
    public Animator animator;
    public List<AudioClip> audioClips;
    public AudioClip finalClip;
    private AudioSource audioSource;
    private AudioClip currentClip;
    private bool endFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    private void Update() {
        if(endFlag == false)
            PlayMusic();   
    }

    void PlayMusic()
    {
        if(audioSource.isPlaying == true)
        {
            if(PlayerPrefs.GetInt("Level") == PlayerPrefs.GetInt("MaxLevel") && audioSource.clip != finalClip)
            {
                SetCurrentClip(finalClip);
            }

            return;
        }

        if(audioClips.Count <= 0)
            return;
        
        if(PlayerPrefs.GetInt("Level") != PlayerPrefs.GetInt("MaxLevel"))
            SetCurrentClip(audioClips[Random.Range(0, audioClips.Count)]);
        else
            SetCurrentClip(finalClip);
    }

    void SetCurrentClip(AudioClip clip)
    {
        animator.SetBool("AudioChange", true);
        currentClip = clip;
    }

    void PlayCurrentClip()
    {
        animator.SetBool("AudioChange", false);
        audioSource.clip = currentClip;
        audioSource.Play();
    }
    public void StopMusic()
    {
        endFlag = true;
        audioSource.Stop();   
    }

}
