using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu (menuName = "Skills/Trap Data", fileName = "New Trap", order = 56)]
public class TrapData : ScriptableObject
{
    [SerializeField] private GameObject aimTrap;            //sprite that use for aim to spawn a trap

    [SerializeField] private float stunTime;

    [SerializeField] private float lifetime;

    [InfoBox("Must be greater than stunTime")]
    [SerializeField] private int reloadTime;

    public GameObject AimTrap => aimTrap;

    public float StunTime => stunTime;

    public float Lifetime => lifetime;

    public int ReloadTime => reloadTime;
}