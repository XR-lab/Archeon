using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pek : MonoBehaviour
{
    private Collider _col;
    private FixedJoint _joint = null;

    private GameObject _attached;

    private bool _heating = false;
    private bool _stick = false;
    
    [SerializeField]
    private float _heatingTime = 0;

    public bool _usable = false;
    public bool _sticked = false;

    private void Start()
    {
        _col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (!_heating)
        {
            Cooling();
        }
        if (_heatingTime < 30)
        {
            _usable = false;
            if(!_sticked)
            {
                _stick = true;
                _sticked = true;
            } 
        }
        else if (_heatingTime > 30 && _heatingTime < 70)
        {
            _usable = true;
            _sticked = false;
            this.gameObject.transform.SetParent(null);
            if (_joint) 
            {
                Destroy(_joint);
                GetComponent<Rigidbody>().mass = 1;
            }
        }
        else if (_heatingTime > 70)
        {
            _usable = false;
            Physics.IgnoreCollision(_attached.GetComponent<Collider>(), _col, false);
            this.gameObject.transform.SetParent(null);
            //if (_joint) 
            //{
                Destroy(_joint);
                GetComponent<Rigidbody>().mass = 1;
                print("destory");
            //}
        }
    }

    private void OnCollisionStay(Collision _other)
    {
        GameObject _otherG = _other.gameObject;
        if (_otherG.name == "Fire")
        {
            Heating();
            _heating = true;
        }
        if (_stick && _otherG.layer == 10 && _otherG.tag == "Handles")
        {
            _stick = false;
            this.gameObject.transform.SetParent(_otherG.transform);
            _joint = _otherG.AddComponent<FixedJoint>();
            _joint.connectedBody = this.gameObject.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(_otherG.GetComponent<Collider>(), _col);
            _attached = _otherG;
        }
        else if(_stick && _otherG.layer == 10)
        {
            _otherG.transform.SetParent(this.gameObject.transform);
            _joint = this.gameObject.AddComponent<FixedJoint>();
            _joint.connectedBody = _otherG.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(_otherG.GetComponent<Collider>(), _col);
            _attached = _otherG;
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
        if (_heatingTime < 100)
        {
            _heatingTime += .2f;
        }
    }

    private void Cooling()
    {
        if (_heatingTime > 0)
        {
            _heatingTime -= .2f;
        }
    }
}
