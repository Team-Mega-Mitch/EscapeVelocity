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

        Planet.SetSeed();
        Planet.SetColors();
        Planet.SetSize(Planet.Size);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed();
        Planet.EnableCraters(Planet.CratersEnabled);
    }
}
