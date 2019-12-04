using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Pek : MonoBehaviour
{
<<<<<<< HEAD
    private Interactable _interact;
=======
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
    [SerializeField]
    private List<GameObject> _attachedGO = new List<GameObject>();
    private List<FixedJoint> _joints = new List<FixedJoint>();

    private bool _heating = false;
    private bool _stick = false;
    [SerializeField]
    private bool _hard = true;
    [SerializeField]
<<<<<<< HEAD
    private float _heatingTime = 0;
    private bool _sticked = false;

    private void Start()
    {
        _interact = GetComponent<Interactable>();
    }

=======
    private float _heatingTime = 69;
    private bool _sticked = false;

>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
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
<<<<<<< HEAD
        else if (_heatingTime > 30 && _heatingTime < 70)
=======
        else if (_heatingTime > 30 && _heatingTime < 100)
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
        {
            _hard = false;
            _sticked = false;
            UnStick();
<<<<<<< HEAD

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
=======
        }
        else if (_heatingTime > 100)
        {
            Destroy(this.gameObject);
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Fire"))
        {
            _heating = true;
        }

<<<<<<< HEAD
        print(_other.name);

=======
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
        if (_other.gameObject.layer != 10)
            return;

        if (!_hard)
        {
            _attachedGO.Add(_other.gameObject.transform.parent.gameObject);
<<<<<<< HEAD
            if(_other.gameObject.CompareTag("Craftable") || _other.gameObject.CompareTag("Handles")) 
=======
            if(_other.gameObject.CompareTag("Craftable")) 
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
            {
                Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>());
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("Fire"))
        {
            _heating = false;
        }

        if (_other.gameObject.layer != 10)
            return;

        _attachedGO.Remove(_other.gameObject.transform.parent.gameObject);
        Physics.IgnoreCollision(this.gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>(), _other.gameObject.GetComponent<Collider>(), false);
    }

    private void Heating()
    {
<<<<<<< HEAD
        if (_heatingTime < 100)
        {
            _heatingTime += .2f;
=======
        if (_heatingTime < 120)
        {
            _heatingTime += .15f;
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
        }
    }

    private void Cooling()
    {
        if (_heatingTime > 0)
        {
<<<<<<< HEAD
            _heatingTime -= .2f;
=======
            _heatingTime -= .15f;
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
        }
    }

    private void Stick()
    {
        foreach(GameObject _GO in _attachedGO)
        {
<<<<<<< HEAD
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
=======
            //FixedJoint _joint = this.gameObject.AddComponent<FixedJoint>();
            //_joint.connectedBody = _GO.GetComponent<Rigidbody>();
            //_joints.Add(_joint);
            _GO.GetComponent<Rigidbody>().isKinematic = true;
            _GO.transform.SetParent(this.transform);
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
        }
    }

    private void UnStick()
    {
<<<<<<< HEAD
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
=======
        /*foreach (FixedJoint _joint in _joints)
        {
            Destroy(_joint);
        }*/
        foreach (GameObject _GO in _attachedGO)
        {
            _GO.transform.SetParent(null);
            _GO.GetComponent<Rigidbody>().isKinematic = false;
            /*if (_GO.gameObject.GetComponent<Interactable>().enabled == false)
            {
                _GO.gameObject.GetComponent<Interactable>().enabled = true;
            }*/
>>>>>>> 12aac5fe86dda99f3f1df142130808c555129834
        }
    }
}
