using UnityEditor;

[CustomEditor(typeof(PlanetContinents))]
public class PlanetContinentsEditor : Editor {
    private PlanetContinents Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetContinents)target;
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
