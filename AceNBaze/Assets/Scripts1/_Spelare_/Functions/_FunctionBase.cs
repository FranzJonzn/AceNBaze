﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _FunctionBase : MonoBehaviour
{

    // will be used to lock actions
    protected string _keyName;
    protected int    _keyHash;

    //protected _FunctionBase(string Name)
    //{
    //    _keyName = this.name;
    //    _keyHash = Animator.StringToHash(_keyName);

    //}
    protected void Awake()
    {
        _keyName = GetType().Name;
        _keyHash = Animator.StringToHash(_keyName);

    }


    public abstract void Tick(CharacterBaseAbilitys baseAbilitys, LockManager modifier);




   


}
