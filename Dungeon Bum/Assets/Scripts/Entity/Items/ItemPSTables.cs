using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Entity.Items
{
    public class ItemPSTables
    {
        public static ItemPrefix GetMeleePrefix()
        {
            int pick = UnityEngine.Random.Range(0, ItemMeleePrefixes.Length);
            return ItemMeleePrefixes[pick];
        }

        public static ItemPrefix[] ItemMeleePrefixes = new ItemPrefix[]
        {
            new ItemPrefix()
            {
                Name = "Intelligent",
                ActorChanges = new ActorStats()
                {
                    Mana = 13
                },
                StatChanges = new ItemStats()
                {
                    ItemLevel = 1
                }
            },
            new ItemPrefix()
            {
                Name = "Long",
                StatChanges = new ItemStats()
                {
                    Damage = 4,
                    ItemLevel = 1
                }
            },
            new ItemPrefix()
            {
                Name = "Broken",
                StatChanges = new ItemStats()
                {
                    Damage = -3,
                    AttackSpeed = -0.4f,
                    ItemLevel = -3
                }
            },
            new ItemPrefix()
            {
                Name = "Dull",
                StatChanges = new ItemStats()
                {
                    Damage = -3,
                    ItemLevel = -1
                }
            },
            new ItemPrefix()
            {
                Name = "Slow",
                StatChanges = new ItemStats()
                {
                    AttackSpeed = -0.4f,
                    ItemLevel = -1
                }
            },
            new ItemPrefix()
            {
                Name = "Pointy",
                StatChanges = new ItemStats()
                {
                    Damage = 2,
                    ItemLevel = 1
                }
            },
            new ItemPrefix()
            {
                Name = "Agile",
                StatChanges = new ItemStats()
                {
                    AttackSpeed = 0.4f,
                    ItemLevel = 1
                }
            },
            new ItemPrefix()
            {
                Name = "Wild",
                StatChanges = new ItemStats()
                {
                    AttackSpeed = 0.6f,
                    ItemLevel = 2
                }
            },
            new ItemPrefix()
            {
                Name = "Infested",
                StatChanges = new ItemStats()
                {
                    ItemLevel = 3,
                    ProjectileEffects = new System.Collections.Generic.List<ProjectileEffect>()
                    {
                        new ProjectileEffectInfested()
                    }
                }
            }
        };
    }

    public struct ItemPrefix
    {
        public string Name;
        public ItemStats StatChanges;
        public ActorStats ActorChanges;
    }

    public struct ItemSuffix
    {

    }

    public enum ItemRarities
    {
        Junk,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
}
