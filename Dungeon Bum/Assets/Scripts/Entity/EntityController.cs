using UnityEngine;
using System.Collections;
using Assets.Scripts.Entity.CollisionDetection;
using Assets.Scripts.Actor.CollisionDetection;
using CONST = Assets.Scripts.Game.Const;

namespace Assets.Scripts.Entity
{
    public class EntityController : MonoBehaviour
    {
        private new EntityCollider collider = null;
        public Matrix2 Velocity = Matrix2.zero; //matrices allow for editing of individual components without declaring new objects
        public Matrix2 Position = Matrix2.zero;
        public bool Sleeping = false;
        public Vector2 VelocityTruncation;
        public bool ApplyGravity = true;
        public bool Grounded = false;
        public float Gravity = CONST.UNIVERSAL_GRAVITY;
        public float AirResistance = 0.01f;
        private Matrix2 newPosition = Matrix2.zero;

        void Start()
        {
            //start init
            collider = GetComponent<EntityCollider>();
            newPosition = new Matrix2(transform.position.x, transform.position.y);
        }

        void Awake()
        {
            //QualitySettings.vSyncCount = 0;
            //Application.targetFrameRate = FPS;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            //set position
            transform.position = new Vector2(Position.x, Position.y);

            //change animations
            if (Velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (GetComponent<Animator>())
            {
                //assign animations
            }
        }

        void FixedUpdate()
        {
            //Input("r");
            //apply physics
            if (!Sleeping)
            {
                if (!Grounded)
                {
                    Velocity.y -= Gravity;
                }
                Velocity.y -= Velocity.y * (AirResistance / 100);
                Velocity.x -= Velocity.x * (AirResistance / 100);
            }
            //calculate new position
            Position = newPosition;
            newPosition = Position + (Velocity * 0.001f);

            if (Mathf.Abs(Velocity.x) < VelocityTruncation.x)
                Velocity.x = 0;
            if (Mathf.Abs(Velocity.y) < VelocityTruncation.y)
                Velocity.y = 0;

            if (Velocity.x == 0 && Velocity.y == 0 && Grounded)
            {
                Sleeping = true;
            }
            else
            {
                Sleeping = false;
            }

            //collision detection
            if (!Sleeping)
                CollisionDetection();
        }

        GameObject wallTouching = null;
        /// <summary>
        /// do not modify velocity by delta time here!
        /// </summary>
        void CollisionDetection()
        {
            Matrix2 bottomLeftNewPosition = new Matrix2(newPosition.x - (collider.Size.x / 2), newPosition.y - (collider.Size.y / 2));
            Matrix2 bottomRightNewPosition = new Matrix2(newPosition.x + (collider.Size.x / 2), newPosition.y - (collider.Size.y / 2));
            Matrix2 topLeftNewPosition = new Matrix2(newPosition.x - (collider.Size.x / 2), newPosition.y + (collider.Size.y / 2));
            Matrix2 topRightNewPosition = new Matrix2(newPosition.x + (collider.Size.x / 2), newPosition.y + (collider.Size.y / 2));

            collider.UpdateBroadCollision(newPosition, 15);
            bool foundFloor = false;
            GameObject floorTouching = null;
            GameObject ceilingTouching = null;
            wallTouching = null;
            bool foundWall = false;
            bool foundCeiling = false;
            //print(Velocity.y);
            foreach (Actor.CollisionDetection.ActorCollider c in collider.OtherActorCollidersNear)
            {
                //check for collision at new position
                ////right walls
                if (((topRightNewPosition.y > c.BottomLeftPoint.y && topRightNewPosition.y < c.TopLeftPoint.y) ||
                    (bottomRightNewPosition.y > c.BottomLeftPoint.y && bottomRightNewPosition.y < c.TopLeftPoint.y)) &&
                    (bottomRightNewPosition.x > c.TopLeftPoint.x) && (Position.x + (collider.Size.x / 2) <= c.TopLeftPoint.x + 0.01f))
                {
                    if (!foundWall)
                    {
                        if (Velocity.x > 0) //useless, refactor later
                        {
                            //going right, left wall
                            newPosition.x = c.TopLeftPoint.x - (collider.Size.x / 2);
                            Velocity.x = 0;
                        }
                        foundWall = true;
                        wallTouching = c.gameObject;
                    }
                }
                ////left walls
                if (((topLeftNewPosition.y > c.BottomRightPoint.y && topLeftNewPosition.y < c.TopRightPoint.y) ||
                    (bottomLeftNewPosition.y > c.BottomRightPoint.y && bottomLeftNewPosition.y < c.TopRightPoint.y)) &&
                    (bottomLeftNewPosition.x < c.TopRightPoint.x) && (Position.x - (collider.Size.x / 2) >= c.TopRightPoint.x - 0.01f))
                {
                    if (!foundWall)
                    {
                        if (Velocity.x < 0)
                        {
                            //going left, right wall
                            newPosition.x = c.TopRightPoint.x + (collider.Size.x / 2);
                            Velocity.x = 0;
                        }
                        foundWall = true;
                        wallTouching = c.gameObject;
                    }
                }

                ////floor
                if (((bottomLeftNewPosition.x - 0.08f > c.TopLeftPoint.x && bottomLeftNewPosition.x + 0.08f < c.TopRightPoint.x) ||
                    (bottomRightNewPosition.x + 0.08f < c.TopRightPoint.x && bottomRightNewPosition.x - 0.08f > c.TopLeftPoint.x)) &&
                     (bottomLeftNewPosition.y <= c.TopLeftPoint.y) && (Mathf.Abs((Position.y - (collider.Size.y / 2)) - c.TopLeftPoint.y) < 0.4f))
                {
                    if (!foundFloor)
                    {
                        if (Velocity.y < 0)
                        {
                            newPosition.y = c.TopLeftPoint.y + (collider.Size.y / 2);
                        }
                        foundFloor = true;
                        floorTouching = c.gameObject;
                    }
                }
                Grounded = foundFloor;

                ////ceiling
                if (((bottomLeftNewPosition.x > c.TopLeftPoint.x && bottomLeftNewPosition.x < c.TopRightPoint.x) ||
                    (bottomRightNewPosition.x < c.TopRightPoint.x && bottomRightNewPosition.x > c.TopLeftPoint.x)) &&
                     (topLeftNewPosition.y >= c.BottomLeftPoint.y) && (Position.y + (collider.Size.y / 2) <= c.BottomLeftPoint.y))
                {
                    if (!foundCeiling)
                    {
                        if (Velocity.y > 0)
                        {
                            newPosition.y = c.BottomLeftPoint.y - (collider.Size.y / 2);
                            Velocity.y = 0;
                        }
                        foundCeiling = true;
                        ceilingTouching = c.gameObject;
                    }
                }

            }
            //check for phys modifiers at new position (slope, ice, etc)
            BasicMaterial applying;

            ////floor, then modify new position
            if (floorTouching)
            {
                BasicMaterial mat = floorTouching.GetComponent<ActorCollider>().Material;
                if (mat != null && mat.Enabled == true)
                {
                    applying = mat;
                }
                else
                {
                    applying = CONST.UNIVERSIAL_MATERIAL_FALLBACK;
                }

                float bounce = (applying.Bouncyness + collider.Material.Bouncyness) > 0.5f ? 0.5f : (applying.Bouncyness + collider.Material.Bouncyness);
                float friction = (applying.SurfaceFriction + collider.Material.SurfaceFriction) > 1 ? 1 : (applying.SurfaceFriction + collider.Material.SurfaceFriction);

                if (Velocity.y < 0)
                {
                    Velocity.y = ((-Velocity.y - 1) * bounce); //this will set velocity to 0 if there is 0 bouncyness.
                }
                if (Velocity.x > 0)
                {
                    if (Velocity.x * friction > friction * 40)
                    {
                        Velocity.x -= friction * 40;
                    }
                    else
                    {
                        Velocity.x -= friction * Velocity.x;
                    }
                }
                else if (Velocity.x < 0)
                {
                    if (Velocity.x * friction < friction * -40)
                    {
                        Velocity.x += friction * 40;
                    }
                    else
                    {
                        Velocity.x -= friction * Velocity.x;
                    }
                }
            }

            ////walls, then modify new position
            ////ceiling, then modify new position
        }
    }
}