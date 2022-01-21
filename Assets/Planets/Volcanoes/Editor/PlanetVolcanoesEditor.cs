using UnityEditor;

[CustomEditor(typeof(PlanetVolcanoes))]
public class PlanetVolcanoesEditor : Editor {
    private PlanetVolcanoes Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetVolcanoes)target;
        Planet.Initialize();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Planet.SetSeed(Planet.LandSeed, Planet.CratersSeed, Planet.LavaSeed);
        Planet.SetLavaFlow(Planet.LavaFlow);
        Planet.SetGravity(Planet.GravitySize);
        Planet.SetColors(Planet.LandColor, Planet.CratersColor, Planet.LavaColor, Planet.AtmosphereColor);
        Planet.SetPixels(Planet.Pixels);
        Planet.SetSize(Planet.PlanetSize);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed(Planet.Speed);
        Planet.EnableCraters(Planet.CratersEnabled);
    }
}
