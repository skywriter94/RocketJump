using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bull : MonoBehaviour
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
        hp = 500;
        dmg = 2;
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("PLAYER_RB_MOVEMENT").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        enemyFOV = GetComponent<EnemyFOV>();
    }

    private void Update()
    {
        Death();
        Move();
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

                    if (anim.GetInteger("AttackType") != 0)
                        anim.SetInteger("AttackType", 0);

                    if (anim.GetBool("IsAttack"))
                        anim.SetBool("IsAttack", false);

                    if (!anim.GetBool("IsMove"))
                        anim.SetBool("IsMove", true);
                    agent.destination = playerPos.position;
                    Quaternion rot = Quaternion.LookRotation(Vector3.Normalize(offset));
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10.0f);
                }
                else
                {
                    if (!agent.isStopped)
                        agent.isStopped = true;

                    if (anim.GetInteger("AttackType") == 0)
                    {
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack_01") &&
                            !anim.GetCurrentAnimatorStateInfo(0).IsName("attack_02") &&
                            !anim.GetCurrentAnimatorStateInfo(0).IsName("attack_03"))
                        {

                            int attackT = Random.Range(1, 4);
                            anim.SetInteger("AttackType", attackT);

                            switch (attackT)
                            {
                                case 1:
                                    anim.CrossFade("attack_01", 0.3f);
                                    break;

                                case 2:
                                    anim.CrossFade("attack_02", 0.3f);
                                    break;

                                case 3:
                                    anim.CrossFade("attack_03", 0.3f);
                                    break;
                            }
                        }
                    }

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