using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game
{
    public class Const
    {
        public const float UNIVERSAL_GRAVITY = 2.5f;
        public static BasicMaterial UNIVERSIAL_MATERIAL_FALLBACK
        {
            get
            {
                return new BasicMaterial()
                {
                    Bouncyness = 0,
                    SurfaceFriction = 0.2f,
                    EdgeHangable = 0
                };
            }
        }
    }
}
