using UnityEditor;

[CustomEditor(typeof(PlanetGasGiant))]
public class PlanetGasGiantEditor : Editor {
    private PlanetGasGiant Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetGasGiant)target;
        Planet.Initialize();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Planet.SetSeed();
        Planet.SetColors();
        Planet.SetSize(Planet.Size);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed();
    }
}