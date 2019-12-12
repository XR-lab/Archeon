using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TranslateAction : DialogueAction
{
    [SerializeField]
    private List<string> _locationSpecifier;

    [SerializeField]
    private List<Vector3> _locations;

    [SerializeField]
    private float _translateRateDivide = 20f;

    [SerializeField]
    private float _callbackDelay;

    [SerializeField]
    private string _callbackString;

    [SerializeField]
    private List<DialogueAction> _callbacks; 

    public override IEnumerator Action(string index, Action callback)
    {
        yield return new WaitForSeconds(delays[_locationSpecifier.FindIndex((string match) => match == index)]);

        Vector3 location = new Vector3();
        for (int i = 0; i < _locationSpecifier.Count; i++)
        {
            if (index == _locationSpecifier[i])
            {
                location = _locations[i];
                break;
            }
        }

        if (location == new Vector3())
        {
            float distance = Vector3.Distance(transform.position,location);
            Vector3 normaliseddirection = Vector3.Normalize(transform.position-location);
            double translateRate = distance / _translateRateDivide;
            for (int i = 0; i < _translateRateDivide; i++)
            {
                transform.TransformPoint(transform.position);
                yield return new WaitForEndOfFrame();
            }
        }
        callback?.Invoke();
    }
}
