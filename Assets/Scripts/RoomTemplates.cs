using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedRoomType
{
    public RoomType roomType;
    [Range(0f, 10f)]
    public float weight;
}

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms, topRooms, leftRooms, rightRooms;
    public GameObject closedRoom;
    public List<GameObject> rooms;

    [Header("Room Types")]
    public WeightedRoomType[] roomTypes; //Normal, Treasure, etc.
    public RoomType bossRoomType; //Siempre para la última sala

    private const float SpawnDelay = 1.5f;


    void Start()
    {
        Invoke(nameof(SpawnEnemies), SpawnDelay);
    }

    private void SpawnEnemies()
    {
        //Última sala siempre boss
        SpawnInRoom(rooms[rooms.Count - 1], bossRoomType, rooms.Count - 1);

        //Resto de salas
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            AddRoomToList roomData = rooms[i].GetComponent<AddRoomToList>();
            SpawnInRoom(rooms[i], roomData.AssignedType, i);
        }
    }

    public RoomType GetRandomRoomType()
    {
        float total = 0f;
        foreach (WeightedRoomType wrt in roomTypes)
            total += wrt.weight;

        float roll = Random.Range(0f, total);
        float cumulative = 0f;
        foreach (WeightedRoomType wrt in roomTypes)
        {
            cumulative += wrt.weight;
            if (roll <= cumulative)
                return wrt.roomType;
        }
        return roomTypes[roomTypes.Length - 1].roomType;
    }

    private void SpawnInRoom(GameObject room, RoomType type, int roomIndex)
    {
        if (type == null || type.possibleEnemies.Length == 0) return;

        AddRoomToList roomData = room.GetComponent<AddRoomToList>();
        List<Transform> spawnPoints = roomData != null && roomData.EnemySpawnPoints.Count > 0
            ? roomData.EnemySpawnPoints
            : null;

        float difficulty = 1f + (roomIndex / (float)rooms.Count);
        int enemyCount = Mathf.RoundToInt(
            Random.Range(type.minEnemies, type.maxEnemies + 1) * difficulty);

        for (int j = 0; j < enemyCount; j++)
        {
            // Posición: EnemySpawnPoint aleatorio, o centro si no hay ninguno
            Vector3 spawnPos = spawnPoints != null
                ? spawnPoints[Random.Range(0, spawnPoints.Count)].position
                : room.transform.position;

            GameObject prefab = type.possibleEnemies[Random.Range(0, type.possibleEnemies.Length)];
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }

        // Loot siempre en el centro de la sala
        if (type.possibleLoot.Length > 0 && Random.value < type.lootSpawnChance)
        {
            GameObject loot = type.possibleLoot[Random.Range(0, type.possibleLoot.Length)];
            Instantiate(loot, room.transform.position, Quaternion.identity);
        }
    }


}
