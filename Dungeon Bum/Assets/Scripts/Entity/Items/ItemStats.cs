using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Entity.Items
{
    [System.Serializable]
    public class ItemStats
    {
        public float AttackSpeed;
        public float Damage;
        public float Range;
        public float ProjectileSpeed;
        public List<ProjectileEffect> ProjectileEffects;
        public int ItemLevel;

        public float Scale;

        public static ItemStats operator +(ItemStats left, ItemStats right)
        {
            if(right == null)
            {
                if (left != null)
                    return left;
                else return new ItemStats();
            }
            else if (left == null)
            {
                return right;
            }

            ItemStats returns = new ItemStats()
            {
                AttackSpeed = left.AttackSpeed + right.AttackSpeed,
                Damage = left.Damage + right.Damage,
                ProjectileSpeed = left.ProjectileSpeed + right.ProjectileSpeed,
                Range = left.Range + right.Range,
                Scale = left.Scale + right.Scale,
                ItemLevel = left.ItemLevel + right.ItemLevel
            };

            returns.ProjectileEffects = new List<ProjectileEffect>();

            if (left.ProjectileEffects != null)
            {
                foreach (ProjectileEffect e in left.ProjectileEffects)
                {
                    returns.ProjectileEffects.Add(e);
                }
            }
            if (right.ProjectileEffects != null)
            {
                foreach (ProjectileEffect e in right.ProjectileEffects)
                {
                    returns.ProjectileEffects.Add(e);
                }
            }

            return returns;
        }
    }
}
