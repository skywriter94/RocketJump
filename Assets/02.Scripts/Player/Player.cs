using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 이동 관련
    public float baseJumpPower = 10.0f;
    int hp;
    int shield;
    float moveSpeed = 5f;
    float rotSpeed = 120.0f;
    float airR = 2.0f;                     // 점프 시 저항수치
    float dragLimit = 2.5f;                // 점프 시 Rigidbody.drag에 적용할 drag값의 제한
    float currentDrag;                     // 점프 시 현재 Rigidbody.drag에 적용할 값

    float r = 0.0f;
    float h = 0.0f;
    float v = 0.0f;

    Rigidbody rb;

    protected Joystick joystick;

    // 플래아어 애니메이션 & 동작 관련
    Animator anim;
    bool checkRocketJumpState = false;
    bool canRocketJump = false;
    Vector3 rocketEffectPos;

    // 플레이어 부위 관련
    public GameObject rocket;
    public GameObject projectileRocket;
    public GameObject rocketSkillA;
    public GameObject rocketSkillB;

    // 버튼들

    // 기타 변수
    private Shake shake; // 카메라 효과


    private void Awake()
    {
        hp = PlayerDmg.instance.hp + PlayerDmg.instance.addedHp;
        shield = PlayerDmg.instance.shield + PlayerDmg.instance.addedShield;
        joystick = FindObjectOfType<Joystick>();
        rb = GameObject.FindGameObjectWithTag("PLAYER_RB_MOVEMENT").GetComponent<Rigidbody>();
        anim = GameObject.FindGameObjectWithTag("PLAYER_RB_MOVEMENT").GetComponent<Animator>();
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    private void FixedUpdate()
    {
        MoveInput();
        CheckRocketJumpState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("GROUND") && anim.GetBool("IsJump"))
        {
            anim.SetBool("IsJump", false);
            rocket.SetActive(true);
            rb.drag = currentDrag = 0;  // 저항 값 초기화
        }
    }


    void MoveInput()
    {
        r = Input.GetAxis("Mouse X");
        if (!anim.GetBool("IsFire")) // 발사 중엔 이동 불가
        {
            h = joystick.Horizontal;
            v = joystick.Vertical;

            if (anim.GetBool("IsJump")) // 점프 시에 -v 입력 값을 받을 시 적용할 저항 수치 
            {
                if (v < 0)
                {
                    if (dragLimit > currentDrag)
                        currentDrag += airR * Time.fixedDeltaTime;
                    else
                        currentDrag = dragLimit;
                }
                rb.drag = currentDrag;
            }
            else
            {
                // 전후좌우 이동 방향 벡터 계산
                //Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
                //tr.Translate(moveDir.normalized * moveSpeed * Time.fixedDeltaTime, Space.Self);
                //tr.Rotate(Vector3.up * rotSpeed * Time.fixedDeltaTime * r);
                Vector3 moveDir = moveSpeed * Time.fixedDeltaTime * Vector3.Normalize((transform.forward * v) + (transform.right * h));
                rb.MovePosition(transform.position + moveDir);
            }
        }

        Quaternion moveRot = Quaternion.Euler(Vector3.up * rotSpeed * Time.fixedDeltaTime * r);
        rb.MoveRotation(rb.rotation * moveRot);

        if (!anim.GetBool("IsJump") && !anim.GetBool("IsFire"))
        {
            anim.SetFloat("h", joystick.Horizontal);
            anim.SetFloat("v", joystick.Vertical);
        }
    }

    void ActiveRocket()
    {
        rocket.SetActive(true);
    }
    void DeactiveRocket()
    {
        rocket.SetActive(false);
    }

    void CheckRocketJumpState()
    {
        if (checkRocketJumpState)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(-transform.up), out hit, 0.5f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(-transform.up) * hit.distance, Color.yellow);
                canRocketJump = true;
                rocketEffectPos = transform.position + transform.TransformDirection(-transform.up) * hit.distance;
            }
            else
            {
                canRocketJump = false;
            }
        }
    }
    public void FootJump()
    {
        Vector3 jumpDir = transform.forward + transform.up;
        rb.AddForce(jumpDir * 3f, ForceMode.Impulse);
    }
    public void RocketJump()
    {
        if (canRocketJump)
        {
            StartCoroutine(shake.ShakeCamera(0.25f, 0.35f, 0.35f));
            rocket.SetActive(false);
            EffectManager.instance.Create_explosion(rocketEffectPos);
            Vector3 jumpDir = transform.forward + transform.up;
            rb.AddForce(jumpDir * baseJumpPower, ForceMode.Impulse);
        }
        checkRocketJumpState = canRocketJump = false;
    }

    void Fire(int WeaponType)
    {
        Vector3 firePos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        UsefulTools.instance.ActionAfterCountdown(ActiveRocket, 1.25f);
        switch(WeaponType)
        {
            case 1:
                Instantiate(projectileRocket, firePos, transform.rotation);
                break;
            case 2:
                Instantiate(rocketSkillA, firePos, transform.rotation);
                break;
            case 3:
                Instantiate(rocketSkillB, firePos, transform.rotation);
                break;
            default:
                Debug.Log("등록되지 않은 무기 타입!");
                break;
        }
    }

    // 이 아래로는 버튼 전용 함수들
    public void Jump()
    {
        if (!anim.GetBool("IsJump") && !anim.GetBool("IsFire")) // 점프 상태와 발사 상태가 아닌 경우에 점프 가능
        {
            checkRocketJumpState = true;
            anim.Play("Rocket Jump");
            anim.SetBool("IsJump", true);
            FootJump();
            UsefulTools.instance.ActionAfterCountdown(RocketJump, 0.42f);
        }
    }

    public void Attack()
    {
        if(!anim.GetBool("IsJump") && !anim.GetBool("IsFire")) // 점프 상태와 발사 상태가 아닌 경우에 발사 가능
        {
            anim.SetBool("IsFire", true);
            UsefulTools.instance.ActionAfterCountdown(DeactiveRocket, 0.4f);
            UsefulTools.instance.ActionAfterCountdown(Fire, 1, 0.5f);
        }
    }

    public void SkillA()
    {
        if (!anim.GetBool("IsJump") && !anim.GetBool("IsFire")) // 점프 상태와 발사 상태가 아닌 경우에 발사 가능
        {
            anim.SetBool("IsFire", true);
            UsefulTools.instance.ActionAfterCountdown(DeactiveRocket, 0.4f);
            UsefulTools.instance.ActionAfterCountdown(Fire, 2, 0.5f);
        }
    }

    public void SkillB()
    {
        if (!anim.GetBool("IsJump") && !anim.GetBool("IsFire")) // 점프 상태와 발사 상태가 아닌 경우에 발사 가능
        {
            anim.SetBool("IsFire", true);
            UsefulTools.instance.ActionAfterCountdown(DeactiveRocket, 0.4f);
            UsefulTools.instance.ActionAfterCountdown(Fire, 3, 0.5f);
        }
    }
}