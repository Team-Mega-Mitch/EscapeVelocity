using UnityEngine;

public class RandomDead : MonoBehaviour, RandomPlanetsInterface {
    public AnimationCurve Rotation;
    public AnimationCurve Speed;
    public AnimationCurve CloudCover;
    public AnimationCurve WaterFlow;

    private PlanetDead _Planet;

    private void Start() {
        _Planet = GetComponent<PlanetDead>();

        SetSeed();
        SetRotate();
        SetSpeed();
        SetMisc();
        SetColors();
    }

    public void SetSeed() {
        int surfaceSeed = Random.Range(1, 100);
        int craterSeed = Random.Range(1,100);

        _Planet.SetSeed(surfaceSeed,craterSeed);
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
        hsv.h = Random.Range(0f, 1f);
        hsv.s = Random.Range(0.15f, 0.5f);
        hsv.v = Random.Range(0.55f, 1f);

        Gradient generatedSurfaceGradient = PlanetColors.GenerateColorGradient(hsv, 0.03f, 0.2f, -0.4f);
        ColorHSV craterHSV = new ColorHSV(hsv.h, hsv.s, hsv.v);
        craterHSV.h -= .05f;
        if(craterHSV.h > 1f){
            craterHSV.h -= 1f;
        }
        craterHSV.s += .3f;
        craterHSV.v -= .3f;
        Gradient craterGradient = PlanetColors.GenerateColorGradient(craterHSV, 0.05f, -0.05f, -0.2f);

        _Planet.SetColors(generatedSurfaceGradient, craterGradient);
    }

    public void SetMisc() {
    }
}