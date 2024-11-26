using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10; // Ancho del laberinto
    public int height = 10; // Alto del laberinto
    public GameObject wallPrefab; // Prefab para las paredes
    public GameObject entryPrefab; // Prefab para la entrada
    public GameObject exitPrefab; // Prefab para la salida
    public List<GameObject> roomPrefabs; // Lista de prefabs para las salas personalizadas

    private int[,] maze;

    void Start()
    {
        GenerateMaze();
        RenderMaze();

        transform.localScale = new Vector3(3, 3, 3);
    }

    void GenerateMaze()
    {
        // Inicializa el laberinto como todo paredes
        maze = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; // 1 representa una pared
            }
        }

        // Crea un camino directo entre entrada y salida
        CreatePathToExit();

        // Continúa generando el laberinto usando DFS
        CarvePath(1, 1);
    }

    void CreatePathToExit()
    {
        // Marca la entrada
        maze[1, 0] = 0; // Entrada

        // Crea un camino directo desde la entrada hasta la salida
        int x = 1;
        int y = 1;
        maze[x, y] = 0;

        while (x < width - 2 || y < height - 2)
        {
            // Decide aleatoriamente si mover en X o Y, respetando límites
            if (Random.value > 0.5f && x < width - 2)
            {
                x += 1;
            }
            else if (y < height - 2)
            {
                y += 1;
            }

            maze[x, y] = 0;
        }

        // Marca la salida
        maze[width - 2, height - 1] = 0; // Salida
    }

    void CarvePath(int x, int y)
    {
        maze[x, y] = 0; // Marca la celda como camino

        // Direcciones aleatorias
        int[] directions = { 0, 1, 2, 3 };
        directions = directions.OrderBy(d => Random.value).ToArray();

        foreach (int dir in directions)
        {
            int nx = x, ny = y;

            switch (dir)
            {
                case 0: nx = x + 2; break; // Derecha
                case 1: nx = x - 2; break; // Izquierda
                case 2: ny = y + 2; break; // Abajo
                case 3: ny = y - 2; break; // Arriba
            }

            // Verifica si la celda es válida
            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[nx, ny] == 1)
            {
                maze[(x + nx) / 2, (y + ny) / 2] = 0; // Elimina la pared intermedia
                CarvePath(nx, ny); // Continúa excavando
            }
        }
    }

    void RenderMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 1)
                {
                    // Renderizar paredes
                    Instantiate(wallPrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
                }
                else if (x == 1 && y == 0)
                {
                    // Renderizar entrada
                    Instantiate(entryPrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
                }
                else if (x == width - 2 && y == height - 1)
                {
                    // Renderizar salida
                    Instantiate(exitPrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
                }
                else
                {
                    // Renderizar una sala personalizada aleatoria con orientación aleatoria
                    if (roomPrefabs != null && roomPrefabs.Count > 0)
                    {
                        GameObject randomRoom = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

                        // Generar rotación aleatoria en múltiplos de 90 grados
                        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);

                        // Instanciar la sala con la rotación aleatoria y establecerla como hija
                        Instantiate(randomRoom, new Vector3(x, 0, y), randomRotation, transform);
                    }
                }
            }
        }
    }
}
