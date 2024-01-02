using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStaticData", menuName = "ScriptableObjects/PlayerStaticData")]
public class PlayerStaticData : ScriptableObject
{
    public float HP = 100f;
    public float Damage = 5f;
    public float DamageRadius = 0.5f;
}
