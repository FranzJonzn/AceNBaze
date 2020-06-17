﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{

    static string LockfreeName = "free";
    static int    Lockfreehash = 1294909896;

    public string LockName { get; }
    public int    LockHash { get; }


    private _ActionBase action;

    private string currentLockName; //most for debug
    private int    currentLockHash;

    public Lock(string name, _ActionBase action)
    {
        LockName = name;
        LockHash = Animator.StringToHash(name);

        currentLockName = LockfreeName;
        currentLockHash = Lockfreehash;
        this.action     = action;
    }

    public bool ControllKey(string keyName){ return currentLockHash == Animator.StringToHash(keyName);    }
    public bool ControllKey(int keyHash)   { return currentLockHash == keyHash;                           }
    public bool ControllKey()              { return currentLockHash == Lockfreehash;                      }


    /// <summary>
    /// Use the action that if its aviable 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyname"  > the name of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAction<T>(CharacterBaseAbilitys Character, T input, string keyname)
    {
        bool couldDoAction = ControllKey() || ControllKey(keyname);
        if(couldDoAction)
            action.ActionFunction<T>(Character, input);

        return couldDoAction;
    }
    /// <summary>
    /// Use the action that if its aviable 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyhash"> the key (hase of namen) of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAction<T>(CharacterBaseAbilitys Character, T input, int keyhash)
    {
        bool couldDoAction = ControllKey() || ControllKey(keyhash);
        if (couldDoAction)
            action.ActionFunction<T>(Character, input);

        return couldDoAction;
    }

    /// <summary>
    /// Use the action that if its aviable and locks it 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyname"  > the name of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAndLockAction<T>(CharacterBaseAbilitys Character, T input, string keyname)
    {
        bool couldDoAction = LockAction(keyname) || ControllKey(keyname);
        if (couldDoAction)
            action.ActionFunction<T>(Character, input);

        return couldDoAction;
    }
    /// <summary>
    /// Use the action that if its aviable and locks it 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyhash"> the key (hase of namen) of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAndLockAction<T>(CharacterBaseAbilitys Character, T input, int keyhash)
    {
        bool couldDoAction = LockAction(keyhash) || ControllKey(keyhash);
        if (couldDoAction)
            action.ActionFunction<T>(Character, input);

        return couldDoAction;
    }




//==================================================================================================
// Functions to lock and unlock 
//==================================================================================================

    /// <summary>
    /// Locks the action 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>true if the action could be claimed, fals if it already loked</returns>
    public bool LockAction(string keyName)
    {
        bool loked = ControllKey(keyName);

        if(loked)
            currentLockHash = Animator.StringToHash(keyName);

        return loked;
    }
    /// <summary>
    /// Locks the action 
    /// </summary>
    /// <param name="keyHash"></param>
    /// <returns>true if the action could be claimed, fals if it already loked</returns>
    public bool LockAction(int keyHash)
    {
        bool loked = ControllKey();

        if (loked)
            currentLockHash = keyHash;

        return loked;
    }

    /// <summary>
    /// Unlocks the action
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>True if current owner unlocks it, fals if any one else trys to unlock it</returns>
    public bool UnLockAction(string keyName)
    {
        bool loked = ControllKey(name);  
        if (loked)
        {
            currentLockName = LockfreeName;
            currentLockHash = Lockfreehash;
        }
        return loked;
    }
    /// <summary>
    /// Unlocks the action
    /// </summary>
    /// <param name="keyHash"></param>
    /// <returns>True if current owner unlocks it, fals if any one else trys to unlock it</returns>
    public bool UnLockAction(int keyHash)
    {
        bool loked = (currentLockHash == keyHash);
        if (loked)
        {
            currentLockName = LockfreeName;
            currentLockHash = Lockfreehash;
        }
        return loked;
    }











}