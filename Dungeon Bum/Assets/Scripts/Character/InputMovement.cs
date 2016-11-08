using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Character
{
    [RequireComponent(typeof(Actor.ActorController))]
    public class InputMovement : MonoBehaviour
    {
        private Actor.ActorController controller = null;
        private bool r = false;
        private bool l = false;
        private bool u = false;
        // Use this for initialization
        void Start()
        {
            controller = GetComponent<Actor.ActorController>();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                l = true;
            }
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                l = true;
            }
            else
            {
                l = false;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                r = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                r = true;
            }
            else
            {
                r = false;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                u = true;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                u = true;
            }
            else
            {
                u = false;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(r)
            {
                controller.Action("r");
            }
            if (l)
            {
                controller.Action("l");
            }
            if (u)
            {
                controller.Action("u");
            }
        }
    }
}
