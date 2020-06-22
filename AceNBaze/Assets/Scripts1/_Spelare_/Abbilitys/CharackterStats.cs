﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharackterStats : MonoBehaviour
{
    /// <summary>
    /// Temporary unit scriptable object system has ben implemented
    /// have puted in a bunche of things that could be need in the finel
    /// </summary>
    [System.Serializable]
    public struct Weapon
    {
        [Header("Stats variables")]
        public int   weaponSpeed;   //Attack cooldown (using 1/attack)

        public int   weaponDamage;
        public float weaponRange;
        [Space]
        public int weaponWeight;  // later affect the speed, mabey remove
        public int weaponType;    // axe, sword, club
        public int weaponStatEffekt;     //ice, bleed, fire, water, 
        public int weaponStatEffetkPower; //how string is effekt
        
        [Space]
        [Space]
        [Header("Opperation variables")]
        [SerializeField] private float _attackStamina;
        [SerializeField] private bool  _notRecovering;
                         private float _recoveringSpeed;
        public float attakcStamina
        {
            get
            {
                return _attackStamina;
            }
            set
            {
                _attackStamina    = value;
                _notRecovering = (_attackStamina == 1f);
            }
        }
        public bool NotColldown => _notRecovering;



        public float collDownSpeed => _recoveringSpeed;

        public Weapon( int speed, int damage, float range)
        {
            weaponType            = -1;
            weaponWeight          = -1;
            weaponSpeed           = speed;
            _recoveringSpeed      = 1.0f / speed;
            weaponDamage          = damage;
            weaponRange           = range;

            weaponStatEffekt      = -1;
            weaponStatEffetkPower = -1;


            _attackStamina    = 1;
            _notRecovering = true;
        }

    }





        [System.Serializable]
    public struct Stats
    {
        public int     level;
        public int     movmentSpeed;
        public int     maxHP     , currentHP;
        public int     maxStamina, currentStamina;
        public Weapon  weapon;            // for now redo to scriptable objet 
        public Vector3 armor;            // Vec3(type          , weight, protection)                   for now redo to scriptable objet 
        public Vector3[] statusEffekts;  // vec3(what to effekt, type of effekt, how mutch to effekt)  for now redo to scriptable objet 

        public void loadDefultStas(CharacterInfo BaseStats)
        {
            //TODO: Addera så att alla stas här går att läsa från character info
            level          = 0;
            movmentSpeed   = BaseStats.movementSpeed;
            maxHP          = BaseStats.HP;
            currentHP      = maxHP;
            maxStamina     = BaseStats.dashCooldown;
            currentStamina = maxStamina;        
            armor          = new Vector3(-1, 10, 200);
            statusEffekts  = new Vector3[] { };
        }

        /// <summary>
        /// Set wat weapon to use
        /// </summary>
        /// <param name="weapon"></param>
        public void SetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
        }

    }


    [SerializeField] private Stats         _characterStats;
    [SerializeField] private CharacterInfo _baseStats;


    public Stats cStats { get { return _characterStats; }
                                  set { _characterStats = value; } }


    private void Start()
    {
        _characterStats.loadDefultStas(_baseStats);
        _characterStats.SetWeapon(new Weapon(_baseStats.attackSpeed, _baseStats.dmg, _baseStats.attackRange));
    }


    /// <summary>
    /// Returns current movmentSpeed with status effekts 
    /// Example: base speed 10 
    ///          status effekts: -10% base speed and 5 speed
    ///          return speed = 10 - 10*0.1 +5 = 14 speed
    /// </summary>
    /// <returns></returns>
    public int GetMovmentSpeed()
    {
        //TODO: Add so status effekts effekts the speed;
        int currentSpeed = cStats.movmentSpeed;
        return currentSpeed;
    }
    /// <summary>
    /// Getter for base speed
    /// </summary>
    /// <returns></returns>
    public int GetBaseMovmentSpeed()
    {
        return cStats.movmentSpeed;
    }




    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.root.position, Vector3.forward * _characterStats.weapon.weaponRange);
    }


}
