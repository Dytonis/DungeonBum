using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Entity
{
    public class Chest : Entity
    {
        public Sprite[] SpriteList;
        public byte State = 0;

        public override void Acitvate()
        {
            base.Acitvate();

            if (State == 0)
            {
                State++;
            }
            else
            {
                State = 0;
            }

            GetComponent<SpriteRenderer>().sprite = SpriteList[State];
        }
    }
}
