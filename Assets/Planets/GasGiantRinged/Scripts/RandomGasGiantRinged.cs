using UnityEngine;

public class RandomGasGiantRinged : MonoBehaviour, RandomPlanetsInterface {
    public AnimationCurve Rotation;
    public AnimationCurve Speed;
    public AnimationCurve CloudCover;
    public AnimationCurve WaterFlow;

    private PlanetGasGiantRinged _Planet;

    private void Start() {
        _Planet = GetComponent<PlanetGasGiantRinged>();

        SetSeed();
        SetRotate();
        SetSpeed();
        SetMisc();
        SetColors();
    }

    public void SetSeed() {
        int planetSeed = Random.Range(1, 100);
        int ringSeed = Random.Range(1, 100);

        _Planet.SetSeed(planetSeed, ringSeed);
    }

    public void SetRotate() {
        float rotation = Rotation.Evaluate(Random.value);
        float ringRotation = Rotation.Evaluate(Random.value);

        _Planet.SetRotate(rotation, ringRotation);
    }

    public void SetSpeed() {
        float planetSpeed = Speed.Evaluate(Random.value);
        float ringSpeed = planetSpeed * Random.Range(.5f, 1.5f);

        _Planet.SetSpeed(planetSpeed, ringSpeed);
    }

    public void SetColors() {
        ColorHSV hsv = new ColorHSV();
        hsv.h = Random.Range(0f, 1f);
        hsv.s = Random.Range(0.4f, 0.6f);
        hsv.v = Random.Range(0.8f, 1f);

        Gradient generatedSurfaceGradient = PlanetColors.GenerateColorGradient(hsv, 0.45f, 0f, -0.55f + Random.Range(0f, 0.1f));

        _Planet.SetColors(generatedSurfaceGradient, generatedSurfaceGradient);
    }

    public void SetMisc() {
        
    }
}