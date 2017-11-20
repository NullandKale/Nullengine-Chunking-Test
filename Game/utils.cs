using System;
public static class utils
{
    public static bool isInRange(int min, int max, int value)
    {
        return (value >= min && value <= max);
    }
}

public class Vector2
{
    public int x;
    public int y;

    public Vector2()
    {
        x = 0;
        y = 0;
    }

    public Vector2(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public Vector3 toVec3(int _z)
    {
        return new Vector3(_z, y, _z);
    }

    public override string ToString()
    {
        return string.Format("[ " + x + ", " + y + "] ");
    }

    public override bool Equals(object obj)
    {
        return (obj as Vector2).x == x && (obj as Vector2).y == y;
    }

    public override int GetHashCode()
    {
        return x << 16 & ((y << 16) >> 16);
    }

    public static Vector2 operator+(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }
}

public class Vector3
{
    public int x;
    public int y;
    public int z;

    public Vector3()
    {
        x = 0;
        y = 0;
        z = 0;
    }

    public Vector3(int _x, int _y, int _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    public Vector2 toVec2()
    {
        return new Vector2(x, y);
    }

    public override string ToString()
    {
        return string.Format("[ " + x + ", " + y + ", " + z +"]");
    }

    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static implicit operator Vector2(Vector3 input)
    {
        return input.toVec2();
    }
}