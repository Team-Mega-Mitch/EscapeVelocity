using UnityEngine;

public class Spawner : MonoBehaviour {
    [Header("Chunking")]
    public Vector2Int ChunkGrid;
    [Range(10, 100)] public int ChunkSize;

    [Header("Spawning")]
    [Range(10, 500)] public int Amount;
    [Range(3, 20)] public float Padding;
    [Range(1, 100)] public int Sampling;
    public AnimationCurve SizeDistribution;

    [Header("Planets")]
    public GameObject[] CommonPlanets;
    public GameObject[] UncommonPlanets;
    public GameObject[] RarePlanets;
    public GameObject[] LegendaryPlanets;

    private void Start() {
        GenerateChunks();
    }

    private void GenerateChunks() {
        for (int row = 0; row < ChunkGrid.y; row++) {
            for (int col = 0; col < ChunkGrid.x; col++) {
                GameObject chunk = new GameObject();

                chunk.name = "Chunk";

                // Can the math be optimized?
                int colOffset = ((ChunkGrid.x * ChunkSize) - ChunkSize); // Used to make sure x is centered.
                Vector2 position = new Vector2((col * (ChunkSize * 2)) - colOffset, row * (ChunkSize * 2));
                chunk.transform.position = position / 2;

                SpawnPlanets(chunk);
            }
        }
    }

    private Vector2 GetRandomLocationInChunk(GameObject chunk) {
        Vector3 location = new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f)) * ChunkSize;

        location.x += chunk.transform.position.x;
        location.y += chunk.transform.position.y;

        return location;
    }

    private void SpawnPlanets(GameObject chunk) {
        for (int i = 0; i < Amount; i++) {
            for (int j = 0; j < Sampling; j++) {
                float size = SizeDistribution.Evaluate(Random.value);
                Vector2 location = GetRandomLocationInChunk(chunk);
                Collider2D overlap = Physics2D.OverlapCircle(location, size);

                if (overlap == null) {
                    GameObject[] planetSelection;
                    if (size >= SizeDistribution.keys[3].value) {
                        planetSelection = LegendaryPlanets;
                    } else if (size >= SizeDistribution.keys[2].value) {
                        planetSelection = RarePlanets;
                    } else if (size >= SizeDistribution.keys[1].value) {
                        planetSelection = UncommonPlanets;
                    } else {
                        planetSelection = CommonPlanets;
                    }

                    int index = Random.Range(0, planetSelection.Length - 1);
                    GameObject planet = Instantiate(planetSelection[index], location, Quaternion.identity);

                    planet.GetComponent<CircleCollider2D>().radius *= Padding;
                    planet.GetComponent<PlanetInterface>().SetSize(size);
                    planet.transform.parent = chunk.transform;

                    break;
                }
            }
        }
    }
}