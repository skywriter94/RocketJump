using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;

    public GameObject explosionWithTxt;
    public GameObject explosion;
    public GameObject smokePuff;
    public GameObject shortFlame;
    public GameObject fireworkTrail;
    public GameObject magicPoof;
    public GameObject sparksHit;
    public GameObject sparksExplosion;
    public GameObject vortexGround;
    public GameObject virus;
    public GameObject enemyDeathSkull;
    public GameObject wwexplosion;
    public GameObject skullExplosion;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Create_explosionWithTxt(Vector3 pos, Quaternion rot)
    {
        Instantiate(explosionWithTxt, pos, rot);
    }
    public void Create_explosionWithTxt(Vector3 pos)
    {
        Instantiate(explosionWithTxt, pos, Quaternion.identity);
    }

    public void Create_explosion(Vector3 pos, Quaternion rot)
    {
        Instantiate(explosion, pos, rot);
    }
    public void Create_explosion(Vector3 pos)
    {
        Instantiate(explosion, pos, Quaternion.identity);
    }

    public void Create_smokePuff(Vector3 pos, Quaternion rot)
    {
        Instantiate(smokePuff, pos, rot);
    }
    public void Create_smokePuff(Vector3 pos)
    {
        Instantiate(smokePuff, pos, Quaternion.identity);
    }

    public void Create_shortFlame(Vector3 pos, Quaternion rot)
    {
        Instantiate(shortFlame, pos, rot);
    }
    public void Create_shortFlame(Vector3 pos)
    {
        Instantiate(shortFlame, pos, Quaternion.identity);
    }

    public void Create_fireworkTrail(Vector3 pos, Quaternion rot)
    {
        Instantiate(fireworkTrail, pos, rot);
    }
    public void Create_fireworkTrail(Vector3 pos)
    {
        Instantiate(fireworkTrail, pos, Quaternion.identity);
    }

    public void Create_magicPoof(Vector3 pos, Quaternion rot)
    {
        Instantiate(magicPoof, pos, rot);
    }
    public void Create_magicPoof(Vector3 pos)
    {
        Instantiate(magicPoof, pos, Quaternion.identity);
    }

    public void Create_sparksHit(Vector3 pos, Quaternion rot)
    {
        Instantiate(sparksHit, pos, rot);
    }
    public void Create_sparksHit(Vector3 pos)
    {
        Instantiate(sparksHit, pos, Quaternion.identity);
    }

    public void Create_sparksExplosion(Vector3 pos, Quaternion rot)
    {
        Instantiate(sparksExplosion, pos, rot);
    }
    public void Create_sparksExplosion(Vector3 pos)
    {
        Instantiate(sparksExplosion, pos, Quaternion.identity);
    }

    public void Create_vortexGround(Vector3 pos, Quaternion rot)
    {
        Instantiate(vortexGround, pos, rot);
    }
    public void Create_vortexGround(Vector3 pos)
    {
        Instantiate(vortexGround, pos, Quaternion.identity);
    }

    public void Create_virus(Vector3 pos, Quaternion rot)
    {
        Instantiate(virus, pos, rot);
    }
    public void Create_virus(Vector3 pos)
    {
        Instantiate(virus, pos, Quaternion.identity);
    }

    public void Create_enemyDeathSkull(Vector3 pos, Quaternion rot)
    {
        Instantiate(enemyDeathSkull, pos, rot);
    }
    public void Create_enemyDeathSkull(Vector3 pos)
    {
        Instantiate(enemyDeathSkull, pos, Quaternion.identity);
    }

    public void Create_wwexplosion(Vector3 pos, Quaternion rot)
    {
        Instantiate(wwexplosion, pos, rot);
    }
    public void Create_wwexplosion(Vector3 pos)
    {
        Instantiate(wwexplosion, pos, Quaternion.identity);
    }

    public void Create_skullExplosion(Vector3 pos, Quaternion rot)
    {
        Instantiate(skullExplosion, pos, rot);
    }
    public void Create_skullExplosion(Vector3 pos)
    {
        Instantiate(skullExplosion, pos, Quaternion.identity);
    }
}