using UnityEditor;

[CustomEditor(typeof(PlanetGasGiantRinged))]
public class PlanetGasGiantRingedEditor : Editor {
    private PlanetGasGiantRinged Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetGasGiantRinged)target;
        Planet.Initialize();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Planet.SetSeed();
        Planet.SetColors();
        Planet.SetSize(Planet.Size);
        Planet.SetRotate();
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed();
        Planet.EnableRing(Planet.RingEnabled);
    }
}