using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasicMaterial
{
    public bool Enabled = false;
    [Range(0, 1f)]
    public float SurfaceFriction = 0.08f;
    [Range(0,0.5f)]
    public float Bouncyness = 0;
    public byte EdgeHangable = 0;
}
