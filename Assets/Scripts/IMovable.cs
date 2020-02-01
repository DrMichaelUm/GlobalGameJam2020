using UnityEngine;

interface IMovable
{
    void Move(Rigidbody2D rb, float speed, Vector2 movement);
}