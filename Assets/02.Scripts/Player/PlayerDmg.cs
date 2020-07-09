using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmg : MonoBehaviour
{
    [HideInInspector] public int hp = 4;
    [HideInInspector] public int shield = 2;
    [HideInInspector] public int projectile_dmg = 20;
    [HideInInspector] public float skillA_knockbackPower = 1.0f;
    [HideInInspector] public float skillA_knockbackRadius = 2.0f;
    [HideInInspector] public int skillB_dmg = 10;
    [HideInInspector] public int skillB_rounds = 12;
    [HideInInspector] public int skillB_roundDmg = 5;

    [HideInInspector] public int addedHp;
    [HideInInspector] public int addedShield;
    [HideInInspector] public int addedProjectile_dmg;
    [HideInInspector] public float addedSkillA_knockbackPower;
    [HideInInspector] public float addedSkillA_knockbackRadius;
    [HideInInspector] public int addedSkillB_dmg;
    [HideInInspector] public int addedSkillB_rounds;
    [HideInInspector] public int addedSkillB_roundDmg;

    [HideInInspector] public int totalProjectile_dmg;
    [HideInInspector] public int totalSkillB_dmg;
    [HideInInspector] public int totalSkillB_roundDmg;

    public static PlayerDmg instance = null;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        totalProjectile_dmg = projectile_dmg + addedProjectile_dmg;
        totalSkillB_dmg = skillB_dmg + addedSkillB_dmg;
        totalSkillB_roundDmg = skillB_roundDmg + addedSkillB_roundDmg;
    }
}