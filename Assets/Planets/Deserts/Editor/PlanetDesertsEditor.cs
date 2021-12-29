using UnityEditor;

[CustomEditor(typeof(PlanetDeserts))]
public class PlanetDesertsEditor : Editor {
    private PlanetDeserts Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetDeserts)target;
        Planet.Initialize();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Planet.SetSeed(Planet.SurfaceSeed);
        Planet.SetColors(Planet.SurfaceColor, Planet.AtmosphereColor);
        Planet.SetPixels(Planet.Pixels);
        Planet.SetSize(Planet.Size);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed(Planet.Speed);
    }
}
