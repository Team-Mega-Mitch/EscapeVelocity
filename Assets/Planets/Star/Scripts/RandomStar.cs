using UnityEngine;

public class RandomStar : MonoBehaviour, RandomPlanetsInterface {
    public AnimationCurve Rotation;
    public AnimationCurve Speed;
    public AnimationCurve CloudCover;
    public AnimationCurve WaterFlow;

    private PlanetStar _Planet;

    private void Start() {
        _Planet = GetComponent<PlanetStar>();

        SetSeed();
        SetRotate();
        SetSpeed();
        SetMisc();
        SetColors();
    }

    public void SetSeed() {
        int landSeed = Random.Range(1, 100);
        int waterSeed = Random.Range(1, 100);
        int cloudSeed = Random.Range(1, 100);

        _Planet.SetSeed(landSeed, waterSeed, cloudSeed);
    }

    public void SetRotate() {
        float rotation = Rotation.Evaluate(Random.value);

        _Planet.SetRotate(rotation);
    }

    public void SetSpeed() {
        float planetSpeed = Speed.Evaluate(Random.value);
        float cloudSpeed = planetSpeed * Random.Range(.5f, 1.5f);

        _Planet.SetSpeed(planetSpeed);
    }

    public void SetColors() {
        ColorHSV hsv = new ColorHSV();
        hsv.h = Random.Range(0f, 1f);
        hsv.s = Random.Range(0.4f, 0.6f);
        hsv.v = 1f;

        float hue2 = Random.Range(0f, 1f);

        Gradient generatedSurfaceGradient = PlanetColors.GenerateColorGradient(hsv, hue2-hsv.h, 0.1f, -0.6f);

        hsv.s += 0.1f;

        Gradient generatedFlaresGradient = PlanetColors.TwoPointGradient(new Color(1f, 1f, 1f), hsv);



        _Planet.SetColors(generatedSurfaceGradient, generatedFlaresGradient, Color.HSVToRGB(hsv.h, 0.35f, 1f));
    }

    public void SetMisc() {
        
    }
}