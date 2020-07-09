using UnityEngine;

public class ProjectileRocket : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 1000, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShowExplosion(collision);
        Destroy(gameObject);
    }

    void ShowExplosion(Collision col)
    {
        ContactPoint contact = col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(-transform.forward, contact.normal);
        EffectManager.instance.Create_explosion(contact.point, rot);
    }
}