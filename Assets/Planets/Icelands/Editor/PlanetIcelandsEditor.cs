using UnityEditor;

[CustomEditor(typeof(PlanetIcelands))]
public class PlanetIcelandsEditor : Editor {
    private PlanetIcelands Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetIcelands)target;
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
