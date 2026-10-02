using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
public class ZombieData : ScriptableObject
{
    public float maxHealth;
    public float baseSpeed;
    public Sprite sprite;
    
    public float damage;
    public float attackCooldown;
}
