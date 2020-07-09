using UnityEngine;

public class Rocket_SkillB : MonoBehaviour
{
    public GameObject spreadRound;
    int rounds;
    ContactPoint contact;

    Rigidbody rb;
    float radius = 2.0f;


    private void Start()
    {
        rounds = PlayerDmg.instance.skillB_rounds + PlayerDmg.instance.addedSkillB_rounds;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 8000, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShowExplosion(collision);
        IndirectRoundsKnockBack(contact.point);
        Destroy(gameObject);
    }


    void ShowExplosion(Collision col)
    {
        contact = col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(-transform.forward, contact.normal);
        EffectManager.instance.Create_fireworkTrail(contact.point, rot);

        for (int i = 0; i < rounds; i++)
        {
            Instantiate(spreadRound, contact.point, Quaternion.identity);
        }
    }

    void IndirectRoundsKnockBack(Vector3 pos)
    {
        Collider[] colls_Rounds = Physics.OverlapSphere(pos, radius, 1 << 12);
        foreach (var coll in colls_Rounds)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.velocity = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        }
    }
}