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

        Planet.SetSeed(Planet.LandSeed, Planet.WaterSeed, Planet.CloudsSeed);
        Planet.SetWaterFlow(Planet.WaterFlow);
        Planet.SetCloudCover(Planet.CloudCover);
        Planet.SetGravity(Planet.GravitySize);
        Planet.SetColors(Planet.LandColor, Planet.WaterColor, Planet.CloudsColor, Planet.AtmosphereColor);
        Planet.SetPixels(Planet.Pixels);
        Planet.SetSize(Planet.PlanetSize);
        Planet.SetRotate(Planet.Rotation);
        Planet.SetLight(Planet.LightOrigin);
        Planet.SetSpeed(Planet.PlanetSpeed, Planet.CloudSpeed);
    }
}
