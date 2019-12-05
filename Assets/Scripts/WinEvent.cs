using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEvent : MonoBehaviour
{
    [SerializeField]
    private AudioClip _pointSoundEffect;
    [SerializeField]
    private AudioClip _winSoundEffect;

    private AudioSource _source;

    private void Start()
    {
        _source = gameObject.GetComponent<AudioSource>();
    }

    public void PointAdded()
    {
        _source.clip = _pointSoundEffect;
        _source.Play();
    }

    public void PointsAccomplished()
    {
        _source.clip = _winSoundEffect;
        _source.Play();
    }
}
