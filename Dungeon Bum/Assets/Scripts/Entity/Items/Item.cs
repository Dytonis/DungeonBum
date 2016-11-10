using UnityEngine;
using System.Collections;
using Assets.Scripts.Entity.CollisionDetection;

namespace Assets.Scripts.Entity.Items
{
    public class Item : EntityCollider
    {
        public Actor.ActorController Holder;
        public ItemRarities Rarity;
        public string ImpericalName;
        public string[] PossibleNames;
        [Range(0, 1)]
        public float PrefixChance = 0;
        [Range(0, 1)]
        public float SuffixChance = 0;
        public float ItemLevelMin;
        public float ItemLevelMax;
        public ItemPrefix Prefix;
        public ItemSuffix Suffix;
        public ItemStats Stats;
        [Space]
        public string UIName;

        // Use this for initialization
        new void Start()
        {
            base.Start();
            Stats.ItemLevel = (int)Random.Range(ItemLevelMin, ItemLevelMax+1);
            Prefix = ItemPSTables.GetMeleePrefix();
            if(Prefix.Name != "" && Random.Range(0f, 1f) < PrefixChance)
            {
                UIName = Prefix.Name + " ";
                Stats += Prefix.StatChanges;
            }
            UIName += PossibleNames[UnityEngine.Random.Range(0, PossibleNames.Length)];

            if(Stats.ItemLevel <= 0)
            {
                Rarity = ItemRarities.Junk;
            }
            else if (Stats.ItemLevel < 10)
            {
                Rarity = ItemRarities.Common;
            }
            else if (Stats.ItemLevel < 35)
            {
                Rarity = ItemRarities.Uncommon;
            }
            else if (Stats.ItemLevel < 70)
            {
                Rarity = ItemRarities.Rare;
            }
            else if (Stats.ItemLevel < 90)
            {
                Rarity = ItemRarities.Epic;
            }
            else if (Stats.ItemLevel >= 90)
            {
                Rarity = ItemRarities.Legendary;
            }

            float effectiveItemLevel = Stats.ItemLevel - ((ItemLevelMin + ItemLevelMax) / 2);

            Stats.Damage += Stats.Damage * effectiveItemLevel * 0.05f;
            Stats.AttackSpeed += Stats.AttackSpeed * effectiveItemLevel * 0.002f;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
