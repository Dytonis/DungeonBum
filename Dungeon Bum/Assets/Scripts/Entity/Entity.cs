using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Entity
{
    public class Entity : MonoBehaviour
    {
        public bool AssignCustomSize = false;
        public Vector2 Size;
        [HideInInspector]
        public List<Entity> OtherEntitiesNear = new List<Entity>();

        public void UpdateBroadEntities(Matrix2 Position, float distance)
        {
            OtherEntitiesNear.Clear();
            foreach (Transform T in GameObject.FindGameObjectWithTag("Ent").transform)
            {
                Vector2 conversion = new Vector2(Position.x, Position.y);
                if (Vector2.Distance(conversion, T.position) < distance)
                {
                    if (T.GetComponent<Entity>())
                    {
                        OtherEntitiesNear.Add(T.GetComponent<Entity>());
                    }
                }
            }
        }

        public virtual void Acitvate()
        {

        }
    }
}
