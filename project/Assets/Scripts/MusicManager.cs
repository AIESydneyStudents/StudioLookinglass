using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public List<AudioClip> playlist;
    [SerializeField]
    private AudioClip nowPlaying;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            PlaySong();
        }
    }

    public void PlaySong()
    {
        source.Stop();
        int index = Random.Range(0, playlist.Count);
        nowPlaying = playlist[index];
        source.clip = nowPlaying;
        source.Play();
    }
}
