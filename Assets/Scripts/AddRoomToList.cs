using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoomToList : MonoBehaviour
{
    public RoomType AssignedType { get; private set; }
    public List<Transform> EnemySpawnPoints { get; private set; } = new List<Transform>();
    private const string EnemySpawnPointName = "EnemySpawnPoint";

    private RoomTemplates templates;


    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        AssignedType = templates.GetRandomRoomType();

        //Recoge todos los EnemySpawnPoints hijos del prefab
        foreach (Transform child in transform)
        {
            if (child.name == EnemySpawnPointName)
                EnemySpawnPoints.Add(child);
        }

        templates.rooms.Add(this.gameObject);
    }
}
