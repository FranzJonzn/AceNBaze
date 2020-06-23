﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetAutoAttack : _FunctionBase
{

    [SerializeField] CharacterBaseAbilitys targetAbilitis;

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

        if (Input.GetKey(Controlls.instanse.attack))
        {

            if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, baseAbilitys.maskes.EnemyMask))
                {


                    targetAbilitis = hit.transform.root.GetChild(FunctionTick.CharackterAbilityChildIndex).GetComponent<CharacterBaseAbilitys>();
                }
            }
        }
        else if (Input.anyKeyDown)
        {
            targetAbilitis = null;
        }



        if (targetAbilitis != null)
        {
           if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
                float dist = Vector3.Distance(baseAbilitys.agent.transform.position, targetAbilitis.transform.root.position);


                if (dist <= baseAbilitys.characterStats.cStats.weapon.weaponRange)
                {
                    StopMovment(baseAbilitys, modifier.lockManager);

                    if (!modifier.lockManager.ApplayDamage.UseAction(targetAbilitis, baseAbilitys.characterStats.cStats.weapon, _keyHash))
                        Debug.Log("Could not applay damage, " + modifier.lockManager.ApplayDamage.CurrentLockName + " has locked the action");
                    else
                    {
                        Debug.Log(targetAbilitis.transform.root.gameObject.name + " takes " + baseAbilitys.characterStats.cStats.weapon.weaponDamage + " dmg");
                        StartCoroutine(WaitForAttackSpeed(baseAbilitys, modifier));
                    }
                        
                }
                else
                {
          
                        if (modifier.lockManager.SetAgentMovingDestination.LockAction(_keyName))
                        {

                            modifier.lockManager.SetAgentMovingDestination.UseAction(baseAbilitys, targetAbilitis.transform.root.position, _keyHash);

                            modifier.lockManager.SetAgentMovingDestination.UnLockAction(_keyName);
                        }
                    
                }
 
            }
        }

    }



    /// <summary>
    /// Stops the movment of the player 
    /// </summary>
    /// <param name="baseAbilitys"></param>
    /// <param name="modifier"></param>
    private void StopMovment(CharacterBaseAbilitys baseAbilitys, LockManager modifier)
    {

        if (modifier.SetAgentIsStopped.LockAction(_keyName))
        {
            modifier.SetAgentIsStopped.UseAction(baseAbilitys, true, _keyHash);
            if (modifier.SetAgentMovingDestination.LockAction(keyName: _keyName))
            {
                modifier.SetAgentMovingDestination.UseAction(baseAbilitys, baseAbilitys.mainTransform.position, _keyHash);
                modifier.SetAgentMovingDestination.UnLockAction(_keyHash);
            }
            modifier.SetAgentIsStopped.UseAction(baseAbilitys, false, _keyHash);
            modifier.SetAgentIsStopped.UnLockAction(_keyHash);


        }
    }


    IEnumerator WaitForAttackSpeed(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        float colldown = 0f;
        modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, colldown, _keyHash);

        float colldownSpeed = baseAbilitys.characterStats.cStats.weapon.collDownSpeed;



        while (!baseAbilitys.characterStats.cStats.weapon.NotColldown)
        {

            yield return new WaitForSeconds(colldownSpeed);
            colldown = Mathf.MoveTowards(colldown, 1f, 0.1f);//  Mathf.Clamp01(colldown + colldownSpeed);
            Debug.Log(colldown);
            modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, colldown, _keyHash);


        }

    }

}
