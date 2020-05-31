using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardInfo", menuName = "ScriptableObjects/CardInfo", order = 1)]
public class CardInfo : ScriptableObject
{
    public Sprite faceSprite;

    public byte id;
    
}
