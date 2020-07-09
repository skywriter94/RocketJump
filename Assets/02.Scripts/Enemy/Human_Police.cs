using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human_Police : MonoBehaviour
{
    int hp;
    int dmg;

    NavMeshAgent agent;
    Transform playerPos;
    Animator anim;
    Rigidbody rb;
    CapsuleCollider cc;
    EnemyFOV enemyFOV;

    float fixRotate;
    

    private void Start()
    {
        hp = 40;
        dmg = 1;
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("PLAYER_RB_MOVEMENT").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        enemyFOV = GetComponent<EnemyFOV>();
    }

    private void Update()
    {
        Move();
        Death();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hp > 0)
        {
            switch (collision.transform.tag)
            {
                case "PROJECTILE":
                    hp -= PlayerDmg.instance.totalProjectile_dmg;
                    break;

                case "SKILL_A":
                    break;

                case "SKILL_B":
                    hp -= PlayerDmg.instance.totalSkillB_dmg;
                    break;

                case "SPREADROUND":
                    hp -= PlayerDmg.instance.totalSkillB_roundDmg;
                    break;
            }
        }
    }


    private void Move()
    {
        if(agent.enabled)
        {
            Vector3 offset = playerPos.position - transform.position;

            if (!enemyFOV.isTargetAimed())
            {
                if (agent.isStopped)
                    agent.isStopped = false;

                if (anim.GetBool("IsAttack"))
                    anim.SetBool("IsAttack", false);

                if (!anim.GetBool("IsMove")) 
                    anim.SetBool("IsMove", true);

                Quaternion rot = Quaternion.LookRotation(Vector3.Normalize(offset));
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10.0f);
                agent.destination = playerPos.position;
            }
            else
            {
                if (!agent.isStopped)
                    agent.isStopped = true;

                if (!anim.GetBool("IsAttack"))
                    anim.SetBool("IsAttack", true);
            }

            if (fixRotate != transform.eulerAngles.y)
            {
                Quaternion rot = Quaternion.LookRotation(Vector3.Normalize(offset));
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10.0f);
                fixRotate = transform.eulerAngles.y;
            }
        }
    }

    private void Death()
    {
        if (hp <= 0)
        {
            if (!anim.GetBool("IsDead"))
            {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                cc.isTrigger = true;
                agent.enabled = false;
                anim.SetBool("IsMove", false);
                anim.SetBool("IsDead", true);
                anim.Play("Death");
                UsefulTools.instance.ActionAfterCountdown(CreateDeathEffect, 3.0f);
                Destroy(gameObject, 3.05f);
            }
        }
    }

    private void CreateDeathEffect()
    {
        EffectManager.instance.Create_enemyDeathSkull(transform.position);
    }
}