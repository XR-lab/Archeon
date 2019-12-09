using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple
{
    public int x
    {
        get { return _x; }
        private set { _x = x; }
    }
    private int _x;

    public int y
    {
        get { return _y; }
        private set { _y = y; }
    }
    private int _y;

    public float dimmingRate
    {
        private set { _dimmingRate = dimmingRate; }
        get { return _dimmingRate; }
    }
    private float _dimmingRate;

    public float volume
    {
        private set { _volume = volume; }
        get { _volume -= _dimmingRate; return _volume; }
    }
    private float _volume;

    public float radius
    {
        private set { _radius = radius; }
        get { return _radius; }
    }
    private float _radius;

    public float age
    {
        private set { _age = age; }
        get { return _age; }
    }
    private float _age;

    public Ripple(int cx, int cy, float volume, float dimmingRate, int radius, float age)
    {
        _x = cx;
        _y = cy;
        _dimmingRate = dimmingRate;
        _volume = volume;
        _radius = radius;
        _age = age;
    }

   
}
