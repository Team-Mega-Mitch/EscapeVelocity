using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor {
    private Spawner Spawner;

    protected virtual void OnEnable() {
        Spawner = (Spawner)target;
    }

    protected virtual void OnSceneGUI() {
        Handles.color = Color.green;

        if (Spawner.PreviewChunkGrid) {
            GenerateGrid();
        }
    }

    private void GenerateGrid() {
        int colOffset = ((Spawner.ChunkGrid.x - 1) * Spawner.ChunkSize) / 2;

        for (int row = 0; row < Spawner.ChunkGrid.y; row++) {
            for (int col = 0; col < Spawner.ChunkGrid.x; col++) {
                Vector2 position = new Vector2((col * Spawner.ChunkSize) - colOffset, row * Spawner.ChunkSize);

                position.x += Spawner.transform.position.x;
                position.y += Spawner.transform.position.y;

                Handles.RectangleHandleCap(0, position, Quaternion.identity, Spawner.ChunkSize / 2, EventType.Repaint);
            }
        }
    }
}
