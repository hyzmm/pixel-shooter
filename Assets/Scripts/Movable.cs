using UnityEngine;

public class Movable : MonoBehaviour
{
    [HideInInspector] public Vector2 orientation;

    public EightDirection DirectionInEight => EightDirectionUtil.GetEightDirection(orientation);
}