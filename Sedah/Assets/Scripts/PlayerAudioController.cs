using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> attackClips, runClips, damageClips;
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClipList(List<AudioClip> audioClips, bool immediately)
    {
        if(audioClips.Count == 0)
            return;
        if(audioSource.isPlaying == false)
        {
            int randomIndex = Random.Range(0, audioClips.Count);
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
        }
    }
    public void Run()
    {
        PlayClipList(runClips, false);
    }

    public void Attack()
    {
        PlayClipList(attackClips, true);
    }

    public void Damage()
    {
        PlayClipList(damageClips, true);
    }
}
