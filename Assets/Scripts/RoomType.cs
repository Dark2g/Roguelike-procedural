using UnityEngine;

[CreateAssetMenu(menuName = "Roguelike/RoomType")]
public class RoomType : ScriptableObject
{
    public string typeName;
    public GameObject[] possibleEnemies;
    [Range(0, 10)]
    public int minEnemies;
    [Range(0, 10)]
    public int maxEnemies;
    public GameObject[] possibleLoot; // cofres, items, etc.
    [Range(0f, 1f)]
    public float lootSpawnChance;
}
