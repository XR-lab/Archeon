using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]

public class AudioAction : DialogueAction
{
    [SerializeField]
    private List<string> _audioSpecifiers;

    [SerializeField]
    private List<AudioClip> _audioclips;

    [SerializeField]
    private AudioSource _audioPlayer;

    void Start()
    {
        _audioPlayer = (_audioPlayer==null)?GetComponent<AudioSource>():_audioPlayer;    
    }

    public override IEnumerator Action(string index, Action callback)
    {
        int i = _audioSpecifiers.FindIndex((string match) => match == index);

        yield return new WaitForSeconds(delays[i]);

        if (_audioPlayer)
        {
            AudioClip audioclip = null;
       
            audioclip = _audioclips[i];
    
            yield return new WaitWhile(() => _audioPlayer.isPlaying);

            if (audioclip)
            {
                _audioPlayer.clip = audioclip;
                _audioPlayer.Play();
            }
            
            yield return new WaitForSeconds(audioclip.length);

            callback?.Invoke();
        }
    }
}
