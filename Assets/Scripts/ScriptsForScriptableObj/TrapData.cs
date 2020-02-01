using UnityEngine;

[CreateAssetMenu (menuName = "Skills/Trap Data", fileName = "New Trap", order = 56)]
public class TrapData : ScriptableObject
{
    [SerializeField] private GameObject aimTrap;            //sprite that use for aim to spawn a trap

    [SerializeField] private float lifetime;

    public GameObject AimTrap => aimTrap;

    public float Lifetime => lifetime;
}