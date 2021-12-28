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

        Planet.SetSeed();
        Planet.SetColors();
        Planet.SetSize(Planet.Size);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed();
    }
}
