using UnityEngine;
using UnityEngine.PlayerLoop;

public class DestroyerRotation : MonoBehaviour, IRotatible
{
    [SerializeField]
    private PlayerMovement playerMovement;

    private Vector2 _movement;
    
    void FixedUpdate()
    {
        _movement = playerMovement.movement;
        if (!(_movement.x == 0 && _movement.y == 0))
            RotateTo (GetDirection());
    }
    
    public float GetDirection()
    {
        if (_movement.x == 1 && _movement.y == 1)
            return -45;
        if (_movement.x == 0 && _movement.y == 1)
            return 0;
        if (_movement.x == -1 && _movement.y == 1)
            return 45;
        if (_movement.x == -1 && _movement.y == 0)
            return 90;
        if (_movement.x == -1 && _movement.y == -1)
            return 125;
        if (_movement.x == 0 && _movement.y == -1)
            return 180;
        if (_movement.x == 1 && _movement.y == -1)
            return -125;
        if (_movement.x == 1 && _movement.y == 0)
            return -90;
        return transform.rotation.z;
    }

    public void RotateTo (float rotation)
    {
        transform.rotation = Quaternion.Euler (0f, 0f, rotation);
    }
}
