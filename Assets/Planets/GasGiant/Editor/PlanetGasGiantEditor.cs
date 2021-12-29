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

        Planet.SetSeed(Planet.SurfaceSeed, Planet.Clouds1Seed, Planet.Clouds2Seed);
        Planet.SetCloudCover(Planet.Clouds1Cover, Planet.Clouds2Cover);
        Planet.SetColors(Planet.SurfaceColor, Planet.Clouds1Color, Planet.Clouds2Color);
        Planet.SetPixels(Planet.Pixels);
        Planet.SetSize(Planet.Size);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed(Planet.Speed);
    }
}