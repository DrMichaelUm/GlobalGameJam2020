using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Texture Data", menuName = "Texture Data", order = 56)]
public class Texture2D_Data : ScriptableObject
{
    [SerializeField] private Texture2D texture;

    public Texture2D Texture
    {
        get
        {
            return texture;
        }
    }

}
