using System;
using UnityEngine;

public class Vitals : MonoBehaviour
{
    public EventHandler Died;

    public int MaxHealth = 100;
    public int MaxHunger = 100;

    public int HungerDecayRate = 30;
    public int HungerDecayBaseAmount = 2;
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

    public void TakeDamage(int amount)
    {
        Debug.Log($"{amount} damage taken");
        Health -= amount > Health ? Health : amount;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Died?.Invoke(this, null);
    }

    public void RestoreHealth(int amount)
    {
        Debug.Log($"{amount} health restored");
        Health += amount + Health > MaxHealth ? MaxHealth - Health : amount;
    }

    public void RestoreHunger(int amount)
    {
        Debug.Log($"{amount} hunger restored");
        Hunger += amount + Hunger > MaxHunger ? MaxHunger - Hunger : amount;
    }
}
