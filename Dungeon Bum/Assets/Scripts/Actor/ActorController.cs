using UnityEngine;
using System.Collections;
using Assets.Scripts.Actor.CollisionDetection;
using CONST = Assets.Scripts.Game.Const;

namespace Assets.Scripts.Actor
{
    public class ActorController : MonoBehaviour
    {
        private new ActorCollider collider = null;
        public Matrix2 Velocity = Matrix2.zero; //matrices allow for editing of individual components without declaring new objects
        public Matrix2 Position = Matrix2.zero;
        public bool Sleeping = false;
        public Vector2 VelocityTruncation;
        public bool ApplyGravity = true;
        public bool CanEdgeGrab = false;
        public bool Grounded = false;
        public bool Hanging = false;
        public float Gravity = CONST.UNIVERSAL_GRAVITY;
        public float AirResistance = 0.01f;
        public float WalkSpeed = 4;
        [Range(0, 1f)]
        public float WalkPower = 0.25f;
        public float JumpHeight = 300;
        private Matrix2 newPosition = Matrix2.zero;
        private bool r = false;
        private bool l = false;
        private bool u = false;

        void Start()
        {
            //start init
            collider = GetComponent<ActorCollider>();
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
            if(Velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            //assign animations
            if(r || l)
            {
                GetComponent<Animator>().SetBool("HoldingHorz", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("HoldingHorz", false);
            }

            GetComponent<Animator>().SetFloat("HorzSpeed", Mathf.Abs(Velocity.x));
            GetComponent<Animator>().SetFloat("VertSpeed", Mathf.Abs(Velocity.y));
            GetComponent<Animator>().SetBool("Grounded", Grounded);
        }

        /// <summary>
        /// Must be fixed - framerate independant!
        /// </summary>
        /// <param name="dir"></param>
        public void Input(string dir)
        {
            if (dir == "r")
            {
                r = true;

                if (Velocity.x < WalkSpeed)
                    Velocity.x += WalkSpeed * WalkPower;
                else
                    Velocity.x = WalkSpeed;
            }
            else if (dir == "l")
            {
                l = true;

                if (Velocity.x > -WalkSpeed)
                    Velocity.x -= WalkSpeed * WalkPower;
                else
                    Velocity.x = -WalkSpeed;
            }
            else if (dir == "u")
            {
                if ((Grounded))
                {
                    Velocity.y = Mathf.Sqrt(2 * JumpHeight * Gravity);
                }
            }
        }

        void FixedUpdate()
        {
            //Input("r");
            //apply physics
            if (!Sleeping)
            {
                if (!Grounded && !Hanging)
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

            //reset inputs
            r = false;
            l = false;
            u = false;
        }

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
            GameObject wallTouching = null;
            GameObject ceilingTouching = null;
            bool foundWall = false;
            bool foundCeiling = false;
            //print(Velocity.y);
            foreach (ActorCollider c in collider.OtherCollidersNear)
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
                            if (topRightNewPosition.y < c.TopLeftPoint.y && Mathf.Abs(topRightNewPosition.y - c.TopLeftPoint.y) < 0.2 && collider.TopRightPoint.y >= c.TopLeftPoint.y && Velocity.y < 0.1 && CanEdgeGrab)
                            {
                                //edge grab possibility
                                if (c.Material.EdgeHangable == 1)
                                {
                                    //try to edge grab
                                    Hanging = true;
                                    newPosition.y = c.TopLeftPoint.y - (collider.Size.y / 2);
                                    Velocity.y = 0;
                                }
                            }
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
                            if (topLeftNewPosition.y < c.TopRightPoint.y && Mathf.Abs(topLeftNewPosition.y - c.TopRightPoint.y) < 0.2 && collider.TopLeftPoint.y >= c.TopRightPoint.y && Velocity.y < 0.1 && CanEdgeGrab)
                            {
                                //edge grab possibility
                                if (c.Material.EdgeHangable == 1)
                                {
                                    //try to edge grab
                                    Hanging = true;
                                    newPosition.y = c.TopLeftPoint.y - (collider.Size.y / 2);
                                    Velocity.y = 0;
                                }
                            }
                        }
                        foundWall = true;
                        wallTouching = c.gameObject;
                    }
                }

                ////floor
                if (((bottomLeftNewPosition.x - 0.08f > c.TopLeftPoint.x && bottomLeftNewPosition.x + 0.08f < c.TopRightPoint.x) ||
                    (bottomRightNewPosition.x + 0.08f < c.TopRightPoint.x && bottomRightNewPosition.x - 0.08f > c.TopLeftPoint.x)) &&
                     (bottomLeftNewPosition.y <= c.TopLeftPoint.y) && (Mathf.Abs((Position.y - (collider.Size.y / 2)) - c.TopLeftPoint.y) < 0.4f ))
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
                if(((bottomLeftNewPosition.x > c.TopLeftPoint.x && bottomLeftNewPosition.x < c.TopRightPoint.x) ||
                    (bottomRightNewPosition.x < c.TopRightPoint.x && bottomRightNewPosition.x > c.TopLeftPoint.x)) &&
                     (topLeftNewPosition.y >= c.BottomLeftPoint.y) && (Position.y + (collider.Size.y / 2) <= c.BottomLeftPoint.y))
                {
                    if(!foundCeiling)
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

                if (Velocity.y < 0)
                {
                    Velocity.y = ((-Velocity.y - 1) * applying.Bouncyness); //this will set velocity to 0 if there is 0 bouncyness.
                }
                if (Velocity.x > 0)
                {
                    if (Velocity.x * applying.SurfaceFriction > applying.SurfaceFriction * 40)
                    {
                        Velocity.x -= applying.SurfaceFriction * 40;
                    }
                    else
                    {
                        Velocity.x -= applying.SurfaceFriction * Velocity.x;
                    }
                }
                else if (Velocity.x < 0)
                {
                    if (Velocity.x * applying.SurfaceFriction < applying.SurfaceFriction * -40)
                    {
                        Velocity.x += applying.SurfaceFriction * 40;
                    }
                    else
                    {
                        Velocity.x -= applying.SurfaceFriction * Velocity.x;
                    }
                }
            }

            if(foundWall)
            {
                if(Hanging && u)
                {
                    //climb up if possible
                }
            }
            else
            {
                Hanging = false;
            }

            ////walls, then modify new position
            ////ceiling, then modify new position
        }
    }
}