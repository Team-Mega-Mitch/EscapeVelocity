using UnityEngine;
using System.Collections.Generic;

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
    private Dictionary<Vector2, int> _TrackedChunks = new Dictionary<Vector2, int>();

    private void Start() {
        GenerateChunks();
    }

    private void UpdateChunks(Vector2 direction) {
        if (direction.y == 1) {
            for (int row = 0; row < _Chunks.GetLength(1); row++) {
                Destroy(_Chunks[0, row]);

                for (int col = 0; col < _Chunks.GetUpperBound(0); col++) {
                    _Chunks[col, row] = _Chunks[col + 1, row]; // Shift rows down by one.
                }

                int newCol = _Chunks.GetUpperBound(0);
                Vector2 position = new Vector2(_Chunks[newCol - 1, row].transform.position.x, _Chunks[newCol - 1, row].transform.position.y + ChunkSize);

                _Chunks[newCol, row] = NewChunk(position);

                int rng = _TrackedChunks.ContainsKey(position) ? _TrackedChunks[position] : GenRandomNumber();
                SpawnPlanets(rng, _Chunks[newCol, row]);
            }

        } else if (direction.y == -1) {
            for (int row = _Chunks.GetUpperBound(1); row >= 0; row--) {
                Destroy(_Chunks[_Chunks.GetUpperBound(0), row]);

                for (int col = _Chunks.GetUpperBound(0) - 1; col >= 0; col--) {
                    _Chunks[col + 1, row] = _Chunks[col, row];
                }

                int newCol = _Chunks.GetLowerBound(1);
                Vector2 position = new Vector2(_Chunks[newCol + 1, row].transform.position.x, _Chunks[newCol + 1, row].transform.position.y - ChunkSize);

                _Chunks[newCol, row] = NewChunk(position);

                int rng = _TrackedChunks.ContainsKey(position) ? _TrackedChunks[position] : GenRandomNumber();
                SpawnPlanets(rng, _Chunks[newCol, row]);
            }

        } else if (direction.x == 1) {
            for (int col = 0; col < _Chunks.GetLength(0); col++) {
                Destroy(_Chunks[col, 0]);

                for (int row = 0; row < _Chunks.GetUpperBound(1); row++) {
                    _Chunks[col, row] = _Chunks[col, row + 1]; // Shift the cols left by one.
                }

                int newRow = _Chunks.GetUpperBound(1);
                Vector2 position = new Vector2(_Chunks[col, newRow - 1].transform.position.x + ChunkSize, _Chunks[col, newRow - 1].transform.position.y);

                _Chunks[col, newRow] = NewChunk(position);

                int rng = _TrackedChunks.ContainsKey(position) ? _TrackedChunks[position] : GenRandomNumber();
                SpawnPlanets(rng, _Chunks[col, newRow]);
            }
        } else if (direction.x == -1) {
            for (int col = 0; col < _Chunks.GetLength(0); col++) {
                Destroy(_Chunks[col, _Chunks.GetUpperBound(1)]);

                for (int row = _Chunks.GetUpperBound(1) - 1; row >= 0; row--) {
                    _Chunks[col, row + 1] = _Chunks[col, row];
                }

                int newRow = _Chunks.GetLowerBound(1);
                Vector2 position = new Vector2(_Chunks[col, newRow + 1].transform.position.x - ChunkSize, _Chunks[col, newRow + 1].transform.position.y);

                _Chunks[col, newRow] = NewChunk(position);

                int rng = _TrackedChunks.ContainsKey(position) ? _TrackedChunks[position] : GenRandomNumber();
                SpawnPlanets(rng, _Chunks[col, newRow]);
            }
        }
    }

    private void GenerateChunks() {
        _Chunks = new GameObject[ChunkGrid.y, ChunkGrid.x];
        int colOffset = ((ChunkGrid.x - 1) * ChunkSize) / 2;

        for (int row = 0; row < ChunkGrid.y; row++) {
            for (int col = 0; col < ChunkGrid.x; col++) {
                Vector2 position = new Vector2((col * ChunkSize) - colOffset, row * ChunkSize);
                position.x += transform.position.x;
                position.y += transform.position.y;

                _Chunks[row, col] = NewChunk(position);
                SpawnPlanets(_TrackedChunks[position], _Chunks[row, col]);
            }
        }

    }

    private GameObject NewChunk(Vector2 position) {
        GameObject chunk = new GameObject("Chunk" + Utils.GenUUID(2));
        chunk.transform.position = position;

        if (!_TrackedChunks.ContainsKey(position)) {
            _TrackedChunks.Add(chunk.transform.position, GenRandomNumber());
        }

        return chunk;
    }

    private void SpawnPlanets(int rng, GameObject chunk) {
        Random.State currentState = Random.state;
        Seeder.SetSeed(rng.ToString());

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

                    // planet.GetComponent<CircleCollider2D>().radius *= Padding;
                    AddPadding(planet);
                    planet.GetComponent<PlanetInterface>().SetSize(size);
                    planet.transform.parent = chunk.transform;

                    break;
                }
            }
        }

        Seeder.SetSeed(GetComponent<Seeder>().Seed);
        Random.state = currentState;
    }

    private void AddPadding(GameObject planet) {
        GameObject padding = new GameObject("Padding", typeof(CircleCollider2D));

        padding.transform.position = planet.transform.position;
        padding.GetComponent<CircleCollider2D>().radius = planet.transform.Find("Gravity").GetComponent<CircleCollider2D>().radius * Padding;

        padding.transform.parent = planet.transform;
    }

    private Vector2 GetRandomLocationInChunk(GameObject chunk) {
        Vector2 location = new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f)) * ChunkSize;

        location.x += chunk.transform.position.x;
        location.y += chunk.transform.position.y;

        return location;
    }

    private Vector2Int GetNewChunkDirection() {
        Vector2Int direction = Vector2Int.zero;
        float distance;
        int centerChunk = (ChunkGrid.x - 1) / 2;

        if (_Chunks[0, centerChunk + 1] != null) {
            distance = _Chunks[0, centerChunk + 1].transform.position.x - gameObject.transform.position.x;
            if (distance <= 0) {
                direction.x = 1;
            } else if (distance > ChunkSize * 2) {
                direction.x = -1;
            }
        }

        if (_Chunks[1, centerChunk] != null) {
            distance = _Chunks[1, centerChunk].transform.position.y - gameObject.transform.position.y;

            if (distance <= 0) {
                direction.y = 1;
            } else if (distance > ChunkSize * 2) {
                direction.y = -1;
            }

        }

        return direction;
    }

    private int GenRandomNumber() {
        int num = Random.Range(100000000, 999999999);

        return Random.Range(100000000, 999999999);
    }

    private void Update() {
        Vector2Int chunkDirection = GetNewChunkDirection();

        if (chunkDirection != Vector2Int.zero) {
            UpdateChunks(chunkDirection);
        }
    }
}