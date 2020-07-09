using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float viewRange = 15.0f;  // 캐릭터의 추적 사정 거리 범위
    [Range(0, 360)]
    public float viewAngle = 120.0f; // 시야각 

    Transform playerPos;
    int playerLayer;
    int obstacleLayer;
    int layerMask;


    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("PLAYER_RB_MOVEMENT").transform;
        playerLayer = LayerMask.NameToLayer("PLAYER");
        obstacleLayer = LayerMask.NameToLayer("OBSTACLE");
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }


    public Vector3 CirclePoint(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool isTargetNearby() // 근접 캐릭터
    {
        bool isNearby = false;

        Collider[] colls = Physics.OverlapSphere(transform.position, viewRange, 1 << playerLayer);

        if(colls.Length != 0)
        {
            Vector3 dir = (playerPos.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dir) < viewAngle * 0.5f)
            {
                isNearby = true;
            }
        }
        return isNearby;
    }

    public bool isTargetAimed() // 원거리 캐릭터
    {
        bool isAimed = false;
        Collider[] _target = Physics.OverlapSphere(transform.position, viewRange, layerMask);
        for(int i = 0; i < _target.Length; i++)
        {
            Transform _targetTr = _target[i].transform;
            if(_targetTr.name == "Player")
            {
                Vector3 _direction = (_targetTr.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if(_angle < viewAngle * 0.5f)
                {
                    RaycastHit hit;
                    if(Physics.Raycast(transform.position + transform.up, _direction, out hit, viewRange))
                    {
                        if(hit.transform.name == "Player")
                        {
                            isAimed = true;
                        }
                    }
                }
            }
        }
        return isAimed;
    }
}
