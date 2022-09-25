using System;
using UnityEngine;

static class EightDirectionUtil
{
    public static EightDirection GetEightDirection(Vector2 direction)
    {
        var angle = Vector2.SignedAngle(Vector2.up, direction);
        if (angle < 0)
        {
            angle += 360;
        }

        return angle switch
        {
            < 22.5f => EightDirection.Up,
            < 67.5f => EightDirection.UpLeft,
            < 112.5f => EightDirection.Left,
            < 157.5f => EightDirection.DownLeft,
            < 202.5f => EightDirection.Down,
            < 247.5f => EightDirection.DownRight,
            < 292.5f => EightDirection.Right,
            < 337.5f => EightDirection.UpRight,
            _ => EightDirection.Up
        };
    }

    public static Vector2 DirectionToVector(EightDirection direction)
    {
        switch (direction)
        {
            case EightDirection.Right:
                return Vector2.right;
            case EightDirection.Left:
                return Vector2.left;
            case EightDirection.Up:
                return Vector2.up;
            case EightDirection.Down:
                return Vector2.down;
            case EightDirection.UpRight:
                return new Vector2(0.71f, 0.71f);
            case EightDirection.UpLeft:
                return new Vector2(-0.71f, 0.71f);
            case EightDirection.DownRight:
                return new Vector2(0.71f, -0.71f);
            case EightDirection.DownLeft:
                return new Vector2(-0.71f, -0.71f);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}

public enum EightDirection
{
    Right,
    Left,
    Up,
    Down,
    UpRight,
    UpLeft,
    DownRight,
    DownLeft
}