using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationStateController : MonoBehaviour
{
    private Animator _animator;

    private int isRunHash => Animator.StringToHash("IsRun");
    private int speed => Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // our character will be able to change the states of the animation depending on the speed
    public void AnimatorRunFloat(float value)
    {
        _animator.SetFloat(speed, value);
    }

    // change animation view
    public void OnAnimatorMove()
    {
        if (_animator.GetFloat(speed) > 0.1f)
        {
            _animator.SetBool(isRunHash, true);
        }
        else
        {
            _animator.SetBool(isRunHash, false);
        }
    }
}
