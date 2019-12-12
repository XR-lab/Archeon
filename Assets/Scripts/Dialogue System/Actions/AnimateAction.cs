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

            yield return new WaitUntil(() => _animator.GetAnimatorTransitionInfo(_animatorLayer).IsName(_triggers[i]));

            yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(_animatorLayer)[_animatorLayer].clip.length);
        }

        callback?.Invoke();
    }

}
