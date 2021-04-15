using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> roarClips, attackClips, runClips, damageClips;
    public float audioRange = 10.0f;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource == null)
            audioSource = GetComponentInChildren<AudioSource>();
    }
    public void SetPlayer(Transform player)
    {
        this.player = player;
    }
    public void SetVolume()
    {
        if(transform.gameObject == null)
            Destroy(gameObject);
        
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        float distance = Vector3.Distance(player.position, transform.position);
        audioSource.volume = 1.0f - Mathf.Clamp(distance / audioRange, 0.0f, 1.0f);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
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

    public void Roar()
    {
        SetVolume();
        PlayClipList(roarClips, false);
    }

    public void Run()
    {
        SetVolume();
        PlayClipList(runClips, false);
    }

    public void Attack()
    {
        SetVolume(0.4f);
        PlayClipList(attackClips, true);
    }

    public void Damage()
    {
        SetVolume();
        PlayClipList(damageClips, true);
    }

    public void Destructor()
    {
        Destroy(this);
    }

}
