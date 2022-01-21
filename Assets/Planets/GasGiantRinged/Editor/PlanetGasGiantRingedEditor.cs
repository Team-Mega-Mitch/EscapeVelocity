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

        Planet.SetSeed(Planet.PlanetSeed, Planet.RingSeed);
        Planet.SetGravity(Planet.GravitySize);
        Planet.SetColors(Planet.PlanetColor, Planet.RingColor);
        Planet.SetPixels(Planet.Pixels);
        Planet.SetSize(Planet.PlanetSize);
        Planet.SetRotate(Planet.PlanetRotation, Planet.RingRotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed(Planet.PlanetSpeed, Planet.RingSpeed);
        Planet.EnableRing(Planet.RingEnabled);
    }
}