using UnityEditor;

[CustomEditor(typeof(PlanetDead))]
public class PlanetDeadEditor : Editor {
    private PlanetDead Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetDead)target;
        Planet.Initialize();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Planet.SetSeed(Planet.SurfaceSeed, Planet.CraterSeed);
        Planet.SetColors(Planet.SurfaceColor, Planet.CraterColor);
        Planet.SetPixels(Planet.Pixels);
        Planet.SetSize(Planet.Size);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed(Planet.Speed);
    }
}
