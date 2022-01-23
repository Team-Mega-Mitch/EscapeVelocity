using UnityEngine;

public class RandomDeserts : MonoBehaviour, RandomPlanetsInterface {
    public AnimationCurve Rotation;
    public AnimationCurve Speed;
    public AnimationCurve CloudCover;
    public AnimationCurve WaterFlow;

    private PlanetDeserts _Planet;

    private void Start() {
        _Planet = GetComponent<PlanetDeserts>();

        SetSeed();
        SetRotate();
        SetSpeed();
        SetMisc();
        SetColors();
    }

    public void SetSeed() {
        int surfaceSeed = Random.Range(1, 100);

        _Planet.SetSeed(surfaceSeed);
    }

    public void SetRotate() {
        float rotation = Rotation.Evaluate(Random.value);

        _Planet.SetRotate(rotation);
    }

    public void SetSpeed() {
        float planetSpeed = Speed.Evaluate(Random.value);

        _Planet.SetSpeed(planetSpeed);
    }

    public void SetColors() {
        ColorHSV hsv = new ColorHSV();
        hsv.h = Random.Range(0f, 0.17f);
        hsv.s = Random.Range(0.7f, .85f);
        hsv.v = Random.Range(0.7f, 1f);

        Gradient generatedSurfaceGradient = PlanetColors.GenerateColorGradient(hsv, -0.05f, 0.15f, -0.55f);
        
        Color atmosphere = Color.HSVToRGB(hsv.h + 0.05f, hsv.s, hsv.v);

        _Planet.SetColors(generatedSurfaceGradient, atmosphere);
    }

    public void SetMisc() {
    }
}