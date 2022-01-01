using UnityEditor;

[CustomEditor(typeof(PlanetTest))]
public class PlanetTestEditor : Editor {
    private PlanetTest Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetTest)target;
        Planet.Initialize();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Planet.SetColors(Planet.Color);
        Planet.SetSize(Planet.Size);
    }
}
