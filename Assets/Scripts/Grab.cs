using Valve.VR;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public SteamVR_Action_Boolean _grabAction = null;

    private SteamVR_Behaviour_Pose _pose = null;
    private FixedJoint _joint = null;

    private Grabable _currentGrabable = null;
    private List<Grabable> _contactGrabable = new List<Grabable>();

    private void Awake()
    {
        _pose = GetComponent<SteamVR_Behaviour_Pose>();
        _joint = GetComponent<FixedJoint>();
    }

    void Update()
    {
        if (_grabAction.GetLastStateDown(_pose.inputSource))
        {
            print(_pose.inputSource + " Trigger Down");
            Pickup();
        }

        if (_grabAction.GetLastStateUp(_pose.inputSource))
        {
            print(_pose.inputSource + " Trigger up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer != 10)
            return;

        _contactGrabable.Add(_other.gameObject.GetComponent<Grabable>());
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer != 10)
            return;

        _contactGrabable.Remove(_other.gameObject.GetComponent<Grabable>());
    }

    private void Pickup()
    {
        _currentGrabable = GetNearestInteractable();

        if (!_currentGrabable)
            return;

        if (_currentGrabable._activeHand != null)
            _currentGrabable._activeHand.Drop();

        _currentGrabable.transform.position = transform.position;

        Rigidbody _targetBody = _currentGrabable.GetComponent<Rigidbody>();
        _joint.connectedBody = _targetBody;

        _currentGrabable._activeHand = this;
    }

    private void Drop()
    {
        if (!_currentGrabable)
            return;

        Rigidbody _targetBody = _currentGrabable.GetComponent<Rigidbody>();
        _targetBody.velocity = _pose.GetVelocity();
        _targetBody.angularVelocity = _pose.GetAngularVelocity();

        _joint.connectedBody = null;

        _currentGrabable._activeHand = null;
        _currentGrabable = null;
    }

    private Grabable GetNearestInteractable()
    {
        Grabable _nearest = null;
        float _minDistance = float.MaxValue;
        float _distance = 0.0f;

        foreach (Grabable _grabable in _contactGrabable)
        {
            _distance = (_grabable.transform.position - transform.position).sqrMagnitude;

            if(_distance < _minDistance)
            {
                _minDistance = _distance;
                _nearest = _grabable;
            }
        }
        return _nearest;
    }
}
