using UnityEngine;

public enum PlantRole
{
    Sunflower,
    Shooter,
    Wall
}

[CreateAssetMenu(fileName = "PlantData", menuName = "Scriptable Objects/PlantData")]
public class PlantData : ScriptableObject
{
    public float maxHealth;
    public Sprite sprite;
    public PlantRole role;
    
    public float actionCooldown;

    public float cost;
}
