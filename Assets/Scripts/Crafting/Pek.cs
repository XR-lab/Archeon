using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pek : MonoBehaviour
{
    private Collider _col;

    private bool _heating = false;
    [SerializeField]
    private float _heatingTime = 0;

    public bool _usable = false;
    public bool _hardend = true;

    private void Start()
    {
        _col = GetComponent<Collider>();
    }

    private void Update()
    {
        if(!_heating)
        {
            Invoke("Cooling", 2);
        }

        if(_heatingTime < 30)
        {
            _usable = false;
            _col.isTrigger = false;
            _hardend = true;
        }
        else if(_heatingTime > 30 && _heatingTime < 70)
        {
            _usable = true;
            _col.isTrigger = true;
            _hardend = false;
        }
        else if(_heatingTime > 7)
        {
            _usable = false;
        }
        else if(Mathf.Floor(_heatingTime) == 30)
        {
            _usable = true;
            _hardend = true;
        }
    }

    private void OnCollision(Collision other)
    {
        if (other.gameObject.name == "Fire")
        {
            Heating();
            _heating = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Fire")
        {
            _heating = false;
        }
    }

    private void Heating()
    {
        if(_heatingTime < 100)
        {
            _heatingTime++;
        }
    }

    private void Cooling()
    {
        if (_heatingTime > 0)
        {
            _heatingTime -= .1f;
        }
    }
}
