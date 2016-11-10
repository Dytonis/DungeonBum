using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Actor.CollisionDetection;

namespace Assets.Scripts.Entity.CollisionDetection
{
    public class EntityCollider : Entity
    {
        [HideInInspector]
        public List<ActorCollider> OtherActorCollidersNear = new List<ActorCollider>();
        public BasicMaterial Material;

        public void Start()
        {
            if (!AssignCustomSize)
            {
                Size.x = transform.localScale.x;
                Size.y = transform.localScale.y;
            }
        }

        public void UpdateBroadCollision(Matrix2 Position, float distance)
        {
            OtherActorCollidersNear.Clear();
            foreach (Transform T in GameObject.FindGameObjectWithTag("World").transform)
            {
                Vector2 conversion = new Vector2(Position.x, Position.y);
                if (Vector2.Distance(conversion, T.position) < distance)
                {
                    if (T.GetComponent<ActorCollider>())
                    {
                        OtherActorCollidersNear.Add(T.GetComponent<ActorCollider>());
                    }
                }
            }
        }

        public Matrix2 BottomLeftPoint
        {
            get
            {
                return new Matrix2(transform.position.x - (Size.x / 2), transform.position.y - (Size.y / 2));
            }
            set
            {
                transform.position = new Vector2(value.x + (Size.x / 2), value.y + (Size.y / 2));
            }
        }

        public Matrix2 BottomRightPoint
        {
            get
            {
                return new Matrix2(transform.position.x + (Size.x / 2), transform.position.y - (Size.y / 2));
            }
        }

        public Matrix2 TopLeftPoint
        {
            get
            {
                return new Matrix2(transform.position.x - (Size.x / 2), transform.position.y + (Size.y / 2));
            }
        }

        public Matrix2 TopRightPoint
        {
            get
            {
                return new Matrix2(transform.position.x + (Size.x / 2), transform.position.y + (Size.y / 2));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
