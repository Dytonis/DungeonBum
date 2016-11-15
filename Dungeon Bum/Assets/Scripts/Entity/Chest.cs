using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Entity
{
    public class Chest : Entity
    {
        public Sprite[] SpriteList;
        public string[] PossibleChestContents;
        public byte State = 0;

        public override void ActivateWith(Actor.ActorController actor)
        {
            base.Activate();

            if (State == 0)
            {
                State++;
                int pick = Random.Range(0, PossibleChestContents.Length);
                UnityEngine.Object prefab = Resources.Load(PossibleChestContents[pick]);
                GameObject item = Instantiate(prefab) as GameObject;
                item.transform.position = transform.position;
                item.GetComponent<EntityController>().Velocity.x = Random.Range(-15, 15);
                item.GetComponent<EntityController>().Velocity.y = Random.Range(25, 35);
                item.transform.SetParent(GameObject.FindGameObjectWithTag("Ent").transform);
            }
            else
            {
                State = 0;
            }

            GetComponent<SpriteRenderer>().sprite = SpriteList[State];
        }
    }
}
