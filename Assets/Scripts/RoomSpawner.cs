using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openSide; //Para saber cuál es el sitio abierto

    //1 Necesita puerta abajo
    //2 Necesita puerta arriba
    //3 Necesita puerta a izquierda
    //4 Necesita puerta a derecha

    private RoomTemplates templates;
    private int rnd;
    private bool spawned = false; //Para que el spawn lo haga solo una vez

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>(); //Etiqueta que tenemos que poner en cada
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (openSide == 1)
        {
            //Abajo
            rnd = Random.Range(0, templates.bottomRooms.Length); //Cogemos una sala aleatoria de todas las que tenemos
            Instantiate(templates.bottomRooms[rnd], transform.position, templates.bottomRooms[rnd].transform.rotation);
        }

        else if (openSide == 2)
        {
            //Arriba
            rnd = Random.Range(0, templates.topRooms.Length); //Cogemos una sala aleatoria de todas las que tenemos
            Instantiate(templates.topRooms[rnd], transform.position, templates.topRooms[rnd].transform.rotation);
        }

        else if (openSide == 3)
        {
            //Izquierda
            rnd = Random.Range(0, templates.leftRooms.Length); //Cogemos una sala aleatoria de todas las que tenemos
            Instantiate(templates.leftRooms[rnd], transform.position, templates.leftRooms[rnd].transform.rotation);
        }

        else if (openSide == 4)
        {
            //Derecha
            rnd = Random.Range(0, templates.rightRooms.Length); //Cogemos una sala aleatoria de todas las que tenemos
            Instantiate(templates.rightRooms[rnd], transform.position, templates.rightRooms[rnd].transform.rotation);
        }
        spawned = true;
    }

    //Método para no instanciar dos rooms en el mismo sitio utilizando colliders
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        spawned = true;
    }

}
