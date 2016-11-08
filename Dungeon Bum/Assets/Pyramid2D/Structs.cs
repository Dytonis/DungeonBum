using UnityEngine;
using System.Collections;

[SerializeField]
public struct Matrix2
{
    public float x;
    public float y;

    public Matrix2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Matrix2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Matrix2 operator +(Matrix2 left, Matrix2 right)
    {
        return new Matrix2(left.x + right.x, left.y + right.y);
    }

    public static Matrix2 operator *(Matrix2 left, float right)
    {
        return new Matrix2(left.x * right, left.y * right);
    }

    public static Matrix2 zero = new Matrix2(0, 0);
    public static Matrix2 one = new Matrix2(1, 1);
    public static Matrix2 up = new Matrix2(1, 0);
    public static Matrix2 down = new Matrix2(-1, 0);
    public static Matrix2 left = new Matrix2(0, -1);
    public static Matrix2 right = new Matrix2(0, 1);
}
