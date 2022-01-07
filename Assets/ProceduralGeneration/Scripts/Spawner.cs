using UnityEngine;

public class Spawner : MonoBehaviour {
    [Header("Chunking")]
    public bool PreviewChunkGrid = true;
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

    private GameObject[,] _Chunks;
    private int _TotalChunkRows = 0;

    private void Start() {
        GenerateChunks();
    }

    private void UpdateChunks() {
        for (int row = 0; row < _Chunks.GetLength(1); row++) {
            Destroy(_Chunks[0, row]);

            for (int col = 0; col < _Chunks.GetUpperBound(0); col++) {
                _Chunks[col, row] = _Chunks[col + 1, row]; // Shift rows down by one.
            }

            int newCol = _Chunks.GetUpperBound(0);

            Vector2 position = new Vector2(_Chunks[newCol - 1, row].transform.position.x, _Chunks[newCol - 1, row].transform.position.y + ChunkSize);
            _Chunks[newCol, row] = NewChunk("Chunk" + _TotalChunkRows + row, position);

            SpawnPlanets(_Chunks[newCol, row]);

        }

        _TotalChunkRows++;
    }

    private void GenerateChunks() {
        _Chunks = new GameObject[ChunkGrid.y, ChunkGrid.x];

        for (int row = 0; row < ChunkGrid.y; row++) {
            for (int col = 0; col < ChunkGrid.x; col++) {
                // Can the math be optimized?
                int colOffset = ((ChunkGrid.x * ChunkSize) - ChunkSize); // Used to make sure x is centered.
                Vector2 position = new Vector2((col * (ChunkSize * 2)) - colOffset, row * (ChunkSize * 2));

                _Chunks[row, col] = NewChunk("Chunk" + _TotalChunkRows + col, position / 2);
                SpawnPlanets(_Chunks[row, col]);
            }

            _TotalChunkRows++;
        }
    }

    private GameObject NewChunk(string name, Vector2 position) {
        GameObject chunk = new GameObject(name);
        chunk.transform.position = position;

        return chunk;
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

    private Vector2 GetRandomLocationInChunk(GameObject chunk) {
        Vector3 location = new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f)) * ChunkSize;

        location.x += chunk.transform.position.x;
        location.y += chunk.transform.position.y;

        return location;
    }

    private void Update() {
        transform.position = new Vector2(transform.position.x, transform.position.y + 10 * Time.deltaTime);

        float distance = _Chunks[1, 1].transform.position.y - gameObject.transform.position.y;

        if (distance <= 0) {
            UpdateChunks();
        }
    }
}