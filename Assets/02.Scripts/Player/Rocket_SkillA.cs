using UnityEngine;

public class Rocket_SkillA : MonoBehaviour
{
    Rigidbody rb;
    float knockBackRadius;
    float knockBackPower;


    private void Start()
    {
        knockBackRadius = PlayerDmg.instance.skillA_knockbackRadius + PlayerDmg.instance.addedSkillA_knockbackRadius;
        knockBackPower = PlayerDmg.instance.skillA_knockbackPower + PlayerDmg.instance.addedSkillA_knockbackPower;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 2000, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShowExplosion(collision);
        IndirectKnockBack(transform.position);
        Destroy(gameObject);
    }


    void ShowExplosion(Collision col)
    {
        ContactPoint contact = col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(-transform.forward, contact.normal);
        EffectManager.instance.Create_smokePuff(contact.point, rot);
        EffectManager.instance.Create_magicPoof(contact.point, rot);
    }

    void IndirectKnockBack(Vector3 pos)
    {
        Collider[] colls_E = Physics.OverlapSphere(pos, knockBackRadius, 1 << 8);
        Collider[] colls_N = Physics.OverlapSphere(pos, knockBackRadius, 1 << 9);
        Collider[] colls_H = Physics.OverlapSphere(pos, knockBackRadius, 1 << 10);
        Collider[] colls_D = Physics.OverlapSphere(pos, knockBackRadius, 1 << 11);
        Collider[] col_player = Physics.OverlapSphere(pos, knockBackRadius, 1 << 13);

        foreach (var coll in colls_E)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.AddExplosionForce(400.0f * knockBackPower, pos, knockBackRadius, 0.0f, ForceMode.Acceleration);
        }

        foreach (var coll in colls_N)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.AddExplosionForce(600.0f * knockBackPower, pos, knockBackRadius, 0.0f, ForceMode.Acceleration);
        }

        foreach (var coll in colls_H)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.AddExplosionForce(800.0f * knockBackPower, pos, knockBackRadius, 0.0f, ForceMode.Acceleration);
        }

        foreach (var coll in colls_D)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.AddExplosionForce(1000.0f * knockBackPower, pos, knockBackRadius, 5.0f, ForceMode.Acceleration);
        }

        foreach (var coll in col_player)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.AddExplosionForce(1000.0f * knockBackPower, pos, knockBackRadius, 0.0f, ForceMode.Acceleration);
        }
    }
}