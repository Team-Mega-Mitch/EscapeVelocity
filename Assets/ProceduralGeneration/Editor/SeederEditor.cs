using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Seeder))]
public class SeederEditor : Editor {
    public override void OnInspectorGUI() {
        Seeder seeder = (Seeder)target;

        seeder.RandomSeedOverride = EditorGUILayout.ToggleLeft("Generate New Seed at Runtime", seeder.RandomSeedOverride);

        EditorGUI.BeginDisabledGroup(seeder.RandomSeedOverride);
        seeder.Seed = EditorGUILayout.TextField("Seed", seeder.Seed);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(EditorGUIUtility.labelWidth);
        if (GUILayout.Button("Generate Seed")) {
            seeder.Seed = seeder.GenSeed();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();
    }
}
