using UnityEditor;

[CustomEditor(typeof(PlanetStar))]
public class PlanetStarEditor : Editor {
    private PlanetStar Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetStar)target;
        Planet.Initialize();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Planet.SetSeed(Planet.SurfaceSeed, Planet.FlaresSeed, Planet.EmissionSeed);
        Planet.SetColors(Planet.SurfaceColor, Planet.FlaresColor, Planet.EmissionColor);
        Planet.SetPixels(Planet.Pixels);
        Planet.SetSize(Planet.Size);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetSpeed(Planet.Speed);
    }
}
