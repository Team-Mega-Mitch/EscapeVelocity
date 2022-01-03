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

        GenerateGrid();
    }

    private void GenerateGrid() {
        for (int row = 0; row < Spawner.ChunkGrid.y; row++) {
            for (int col = 0; col < Spawner.ChunkGrid.x; col++) {
                int colOffset = ((Spawner.ChunkGrid.x * Spawner.ChunkSize) - Spawner.ChunkSize); // Used to make sure x is centered.

                Vector2 position = new Vector2((col * (Spawner.ChunkSize * 2)) - colOffset, row * (Spawner.ChunkSize * 2));
                position.x += Spawner.transform.position.x;
                position.y += Spawner.transform.position.y;

                Handles.RectangleHandleCap(0, position / 2, Quaternion.identity, Spawner.ChunkSize / 2, EventType.Repaint);
            }
        }
    }
}
