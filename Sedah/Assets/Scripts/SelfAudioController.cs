using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAudioController : MonoBehaviour
{
    public List<AudioClip> audioclips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayClipList(audioclips);
    }

    public void PlayClipList(List<AudioClip> audioClips)
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
}
