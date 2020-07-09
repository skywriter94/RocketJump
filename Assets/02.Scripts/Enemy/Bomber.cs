using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bomber : MonoBehaviour
{
    int hp;
    int dmg;
    int splashDmg;

    NavMeshAgent agent;
    Transform playerPos;
    Animator anim;
    Rigidbody rb;
    CapsuleCollider cc;

    float stopDist;


    private void Start()
    {
        hp = 10;
        dmg = 2;
        splashDmg = 50;
        stopDist = 0.5f;
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("PLAYER_RB_MOVEMENT").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        Move();
        Dead();
    }

    private void OnCollisionEnter(Collision collision)
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


    void Move()
    {
        if (playerPos)
        {
            if (agent.enabled)
            {
                Vector3 offset = playerPos.position - transform.position;
                float sqrLen = offset.sqrMagnitude;

                if (sqrLen > stopDist)
                {
                    anim.SetBool("IsMove", true);
                    agent.destination = playerPos.position;
                }
                else
                {
                    rb.useGravity = false;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    cc.isTrigger = true;
                    anim.SetBool("IsMove", false);
                    anim.Play("mon00_attack01");
                    agent.enabled = false;
                }
                Quaternion rot = Quaternion.LookRotation(Vector3.Normalize(offset));
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10.0f);
            }
        }
    }

    void Dead()
    {
        if(anim.GetBool("IsDead"))
        {
            Destroy(gameObject);
        }
        else if (hp <= 0)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            cc.isTrigger = true;
            anim.SetBool("IsMove", false);
            anim.Play("mon00_attack01");
            agent.enabled = false;
        }
    }
}