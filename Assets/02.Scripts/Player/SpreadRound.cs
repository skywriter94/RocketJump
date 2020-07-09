using UnityEngine;

public class SpreadRound : MonoBehaviour
{
    GameObject firstHitObj = null;


    private void Start()
    {
        Destroy(gameObject, Random.Range(1.5f, 2.5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (firstHitObj == null)
        {
            if (!collision.transform.CompareTag("PROJECTILE"))
            {
                firstHitObj = collision.gameObject;
            }
        }
        else
        {
            if (firstHitObj != collision.gameObject)
            {
                if (collision.transform.CompareTag("ENEMY"))
                {
                    bool selectEffect = Random.value > 0.5f ? true : false;

                    if (selectEffect)
                    {
                        EffectManager.instance.Create_sparksExplosion(transform.position);
                    }
                    else
                    {
                        EffectManager.instance.Create_sparksHit(transform.position);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (firstHitObj == null)
        {
            if (!collision.transform.CompareTag("PROJECTILE"))
            {
                firstHitObj = collision.gameObject;
            }
        }
        else
        {
            if (firstHitObj != collision.gameObject)
            {
                if (collision.transform.CompareTag("ENEMY"))
                {
                    bool selectEffect = Random.value > 0.5f ? true : false;

                    if (selectEffect)
                    {
                        EffectManager.instance.Create_sparksExplosion(transform.position);
                    }
                    else
                    {
                        EffectManager.instance.Create_sparksHit(transform.position);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        bool selectEffect = Random.value > 0.5f ? true : false;

        if (selectEffect)
        {
            EffectManager.instance.Create_sparksExplosion(transform.position);
        }
        else
        {
            EffectManager.instance.Create_sparksHit(transform.position);
        }
    }
}