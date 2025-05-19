using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamagable> things = new List<IDamagable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            Debug.Log("캠프파이어 근처");
            things.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable))
        {
            Debug.Log("캠프파이어에서 멀어짐");
            things.Remove(damagable);
        }
    }
}