using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAction : DialogueAction
{

    [SerializeField]
    private List<string> _triggerSpecifiers;

    [SerializeField]
    private List<string> _triggers;

    [SerializeField]
    private List<string> _callbackSpecifiers;

    [SerializeField]
    private List<DialogueAction> _callbacks;

    [SerializeField]
    private int _animatorLayer;

    [SerializeField]
    private Animator _animator;

    void Start()
    {
        _animator = (_animator == null) ? GetComponent<Animator>() : _animator;
    }

    public override IEnumerator Action(string index, Action callback)
    {
        int i = _triggerSpecifiers.FindIndex((string match) => match == index);

        yield return new WaitForSeconds(delays[i]);

        if (_animator)
        {
            _animator.SetTrigger(_triggers[i]);

            yield return new WaitForSeconds(_animator.GetAnimatorTransitionInfo(_animatorLayer).duration);

            //The callback doesn't work along the chronology of the dialogue script.
            if (_callbacks[i] && _callbackSpecifiers[i] != "" && _callbackSpecifiers != null)
            {
                StartCoroutine(_callbacks[i].Action(_callbackSpecifiers[i], null));
            }

            yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(_animatorLayer)[_animatorLayer].clip.length);

            _animator.ResetTrigger(_triggers[i]);
        }

        callback?.Invoke();
    }

}
