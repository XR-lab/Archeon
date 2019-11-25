using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Pek : MonoBehaviour
{
    private Interactable _interact;
    [SerializeField]
    private List<GameObject> _attachedGO = new List<GameObject>();
    private List<FixedJoint> _joints = new List<FixedJoint>();

    private bool _heating = false;
    private bool _stick = false;
    [SerializeField]
    private bool _hard = true;
    [SerializeField]
    private float _heatingTime = 0;
    private bool _sticked = false;

    private void Start()
    {
        _interact = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (_heating)
        {
            Heating();
        }
        else
        {
            Cooling();
        }

        if (_heatingTime < 30)
        {
            _hard = true;
            if(!_sticked)
            {
                Stick();
                _sticked = true;
            } 
        }
        else if (_heatingTime > 30 && _heatingTime < 70)
        {
            _hard = false;
            _sticked = false;
            UnStick();

            if (_attachedGO.Count != 0)
            {
                if(!_attachedGO[0].gameObject.CompareTag("Bowl"))
                {
                    float p = (_heatingTime - 30) / 50;
                    this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().velocity * p;
                }
            }
        }
        else if (_heatingTime > 70)
        {
            _hard = false;
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.layer != 10)
            return;

        if (!_hard)
        {
            _attachedGO.Add(_other.gameObject);
            if(_other.gameObject.CompareTag("Craftable") || _other.gameObject.CompareTag("Handles")) 
            {
                Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>());
                print("ignore");
            }
        }
    }

    private void OnCollisionExit(Collision _other)
    {
        if (_other.gameObject.layer != 10)
            return;

        print("igignore");
        //_attachedGO.Remove(_other.gameObject);
        //Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Fire"))
        {
            _heating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
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

    private void Stick()
    {
        foreach(GameObject _GO in _attachedGO)
        {
            FixedJoint _joint = this.gameObject.AddComponent<FixedJoint>();
            _joint.connectedBody = _GO.GetComponent<Rigidbody>();
            _joints.Add(_joint);
            _GO.transform.SetParent(this.transform);
/*
            if(_GO.CompareTag("handles"))
            {
                GetComponent<Interactable>().enabled = false;
                if (_GO.CompareTag("handles"))
                    return;
                _GO.GetComponent<Interactable>().enabled = false;
            }*/
        }
    }

    private void UnStick()
    {
        foreach (FixedJoint _joint in _joints)
        {
            Destroy(_joint);
        }
        foreach (GameObject _GO in _attachedGO)
        {
            _GO.transform.SetParent(null);
            if (_GO.gameObject.GetComponent<Interactable>().enabled == false)
            {
                _GO.gameObject.GetComponent<Interactable>().enabled = true;
            }
        }
    }
}
