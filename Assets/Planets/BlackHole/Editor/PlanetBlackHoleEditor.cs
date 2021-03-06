using UnityEditor;

[CustomEditor(typeof(PlanetBlackHole))]
public class PlanetBlackHoleEditor : Editor {
    private PlanetBlackHole Planet;

    protected virtual void OnEnable() {
        Planet = (PlanetBlackHole)target;
        Planet.Initialize();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Planet.SetSeed(Planet.DiskSeed);
        Planet.SetGravity(Planet.GravitySize);
        Planet.SetColors(Planet.HoleColor, Planet.DiskColor);
        Planet.SetPixels(Planet.Pixels);
        Planet.SetSize(Planet.PlanetSize);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetSpeed(Planet.Speed);
    }
}
