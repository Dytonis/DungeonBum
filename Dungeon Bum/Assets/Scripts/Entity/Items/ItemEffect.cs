using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.Entity.Items
{
    public abstract class ItemEffect
    {
        public string Name;

        public abstract void Tick(Item item);
        public abstract void Init(Item item);
    }

    public class ItemEffectInfested : ItemEffect
    {
        public override void Init(Item item)
        {
            Name = "Infested";
        }

        public override void Tick(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
