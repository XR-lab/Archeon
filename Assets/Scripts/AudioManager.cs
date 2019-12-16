using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioClip _defaultAudio;

    [SerializeField] private string[] _specialObjects;

    [SerializeField] private AudioClip[] _specialAudio;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision _col)
    {
        for (int i = 0; i < _specialObjects.Length; i++)
        {
            if (_col.gameObject.name == _specialObjects[i])
            {
                _audio.clip = _specialAudio[i];
                break;
            }

            if (i == _specialObjects.Length && _col.gameObject.name != _specialObjects[_specialObjects.Length])
            {
                _audio.clip = _defaultAudio;
            }
        }
    }
}
