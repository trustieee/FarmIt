using System;
using UnityEngine;

public class Vitals : MonoBehaviour
{
    public EventHandler Died;

    public int MaxHealth = 100;
    public int MaxHunger = 100;

    public int HungerDecayRate = 30;
    public float HungerDecayBaseAmount = 2f;
    public int HungerStarvingDamageAmount = 5;

    public float Health { get; private set; }
    public float Hunger { get; private set; }

    private float _nextHungerDecay;

    private void Start()
    {
        Health = MaxHealth;
        Hunger = MaxHunger;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Die();
        }

        if (Time.time > _nextHungerDecay)
        {
            DecayHunger();
            _nextHungerDecay = Time.time + HungerDecayRate;
        }
    }

    private void DecayHunger()
    {
        Hunger -= HungerDecayBaseAmount > Hunger ? Hunger : HungerDecayBaseAmount;
        if (Hunger <= 0)
        {
            TakeDamage(HungerStarvingDamageAmount);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage > Health ? Health : damage;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Died?.Invoke(this, null);
    }
}
