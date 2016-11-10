using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct ActorStats
{
    public float Health;
    public float Mana;
    public float MaxHealth;
    public float Armor;
    public float WalkSpeed;
    public float JumpHeight;

    public List<DoT> Dots;

    public void ApplyDoT(float damage, float duration)
    {
        if (Dots == null)
            Dots = new List<DoT>();

        DoT dot = new DoT();
        dot.Damage = damage;
        dot.Duration = duration;

        Dots.Add(dot);
    }
}

public struct DoT
{
    public float Damage;
    public float Duration;
}
