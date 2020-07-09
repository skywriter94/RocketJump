using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootmanBoss : MonoBehaviour
{
    int hp;
    int dmg;

    NavMeshAgent agent;
    Transform playerPos;
    Animator anim;
    Rigidbody rb;
    CapsuleCollider cc;
    EnemyFOV enemyFOV;

    float nextBuffTime;
    float buffTime;

    float fixRotate;


    private void Start()
    {
        hp = 200;
        dmg = 1;
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("PLAYER_RB_MOVEMENT").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        enemyFOV = GetComponent<EnemyFOV>();
        buffTime = 10.0f;
    }

    private void Update()
    {
        Death();
        Move();
        Buff();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hp > 0)
        {
            if (!anim.GetBool("IsBuff"))
            {
                bool isDefend = Random.value > 0.5f ? true : false;

                switch (collision.transform.tag)
                {
                    case "PROJECTILE":
                        anim.SetBool("IsDefend", isDefend);
                        anim.Play("Choose_HitType");
                        if (!isDefend)
                            hp -= PlayerDmg.instance.totalProjectile_dmg;
                        break;

                    case "SKILL_A":
                        break;

                    case "SKILL_B":
                        anim.SetBool("IsDefend", isDefend);
                        anim.Play("Choose_HitType");
                        if (!isDefend)
                            hp -= PlayerDmg.instance.totalSkillB_dmg;
                        break;

                    case "SPREADROUND":
                        anim.SetBool("IsDefend", isDefend);
                        anim.Play("Choose_HitType");
                        if (!isDefend)
                            hp -= PlayerDmg.instance.totalSkillB_roundDmg;
                        break;
                }
            }
        }
        else
        {
            Death();
        }
    }


    private void Move()
    {
        if (agent.enabled)
        {
            Vector3 offset = playerPos.position - transform.position;

            if (!enemyFOV.isTargetNearby())
            {
                if (agent.isStopped)
                    agent.isStopped = false;

                if (anim.GetBool("IsAttack"))
                    anim.SetBool("IsAttack", false);

                if (!anim.GetBool("IsMove"))
                    anim.SetBool("IsMove", true);

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Buff"))
                    agent.destination = playerPos.position;

                Quaternion rot = Quaternion.LookRotation(Vector3.Normalize(offset));
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10.0f);
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
                UsefulTools.instance.CancleAction(BuffOn);
                UsefulTools.instance.CancleAction(BuffOff);
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                cc.isTrigger = true;
                agent.enabled = false;
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
    private void Buff()
    {
        if (!anim.GetBool("CountedNextBuffTime"))
        {
            nextBuffTime = Random.Range(10.0f, 20.0f);
            UsefulTools.instance.ActionAfterCountdown(BuffOn, nextBuffTime);
            UsefulTools.instance.ActionAfterCountdown(BuffOff, nextBuffTime + buffTime);
            anim.SetBool("CountedNextBuffTime", true);
        }
    }

    private void BuffOn()
    {
        anim.CrossFade("Buff", 0.3f);
        agent.speed = 5.5f;
        agent.acceleration = 14f;
        anim.SetBool("IsBuff", true);
    }
    
    private void BuffOff()
    {
        agent.speed = 3.5f;
        agent.acceleration = 8f;
        anim.SetBool("IsBuff", false);
        anim.SetBool("CountedNextBuffTime", false);
    }
}