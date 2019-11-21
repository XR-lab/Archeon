using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum GrabState {Toggle, Hold};

public class Hand : MonoBehaviour
{
    public GrabState grabState;

    public SteamVR_Action_Boolean grabAction = null;

    private SteamVR_Behaviour_Pose _pose = null;
    private FixedJoint _joint = null;

    private Interactable _currentInteractable = null;
    private List<Interactable> _interactables = new List<Interactable>();



    // Start is called before the first frame update
    void Awake()
    {
        _pose = GetComponent<SteamVR_Behaviour_Pose>();
        _joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAction.GetStateDown(_pose.inputSource))
        {
            print(_pose.inputSource + " Trigger Down");
            switch (grabState)
            {
                case GrabState.Hold:
                    PickUp();
                    break;
                case GrabState.Toggle:
                    if (_currentInteractable != null)
                    {
                        Drop();
                    }
                    break;
            }
            
        }

        if (grabAction.GetStateUp(_pose.inputSource))
        {
            print(_pose.inputSource + " Trigger Up");
            switch (grabState)
            {
                case GrabState.Toggle:
                    Drop();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            _interactables.Add(other.gameObject.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            _interactables.Remove(other.gameObject.GetComponent<Interactable>());
        }
    }

    public void PickUp()
    {
        Interactable interactable = GetNearestInteractable();
        if (!interactable)
            return;

        if (_currentInteractable.activeHand)
            _currentInteractable.activeHand.Drop();

        _currentInteractable.transform.position = transform.position;

        Rigidbody targetBody = _currentInteractable.GetComponent<Rigidbody>();

        _joint.connectedBody = targetBody;

        _currentInteractable.activeHand = this;

    }

    public void Drop()
    {
        if (_currentInteractable)
            return;

        Rigidbody targetBody = _currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = _pose.GetVelocity();
        targetBody.angularVelocity = _pose.GetAngularVelocity();

        _joint.connectedBody = null;

        _currentInteractable.activeHand = null;
        _currentInteractable = null;
        
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach(Interactable interactable in _interactables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }
}
