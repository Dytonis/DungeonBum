using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Actor;

namespace Assets.Scripts.Entity.Items
{
    public abstract class ProjectileEffect
    {
        public string Name;
        public string Description;

        public abstract void Hit(Item item, Actor.ActorController actor);
        public abstract void Init(Item item);
    }

    public class ProjectileEffectInfested : ProjectileEffect
    {
        public override void Init(Item item)
        {
            Name = "Infested";
            Description = "Apply a DoT for 8% of damage (#CY" + item.Stats.Damage + "#CD) over 3 seconds.";
        }

        public override void Hit(Item item, Actor.ActorController actor)
        {
            actor.Stats.ApplyDoT(item.Stats.Damage * 0.08f, 3);
        }
    }

    public class ProjectileEffectExplosive : ProjectileEffect
    {
        public override void Hit(Item item, ActorController actor)
        {
            throw new NotImplementedException();
        }

        public override void Init(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
