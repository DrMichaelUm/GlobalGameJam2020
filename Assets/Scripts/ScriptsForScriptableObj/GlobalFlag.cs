using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu (menuName = "GlobalFlag", fileName = "NewFlag", order = 57)]
public class GlobalFlag : ScriptableObject
{
   [ShowNativeProperty] public bool Flag { get; private set; }

   public void SetFlag(bool value)
   {
      Flag = value;
   }
}
