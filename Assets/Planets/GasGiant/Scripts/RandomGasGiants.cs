using UnityEngine;

public class RandomGasGiants : MonoBehaviour, RandomPlanetsInterface {
    public AnimationCurve Rotation;
    public AnimationCurve Speed;
    public AnimationCurve CloudCover;
    public AnimationCurve WaterFlow;

    private PlanetGasGiant _Planet;

    private void Start() {
        _Planet = GetComponent<PlanetGasGiant>();

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
        hsv.s = Random.Range(0.6f, 0.9f);
        hsv.v = Random.Range(0.7f, 1f);

        Gradient generatedSurfaceGradient = PlanetColors.GenerateColorGradient(hsv, -0.05f, 0.1f, -0.6f);

        ColorHSV cloud1HSV = new ColorHSV();
        cloud1HSV.h = hsv.h = 0.16f;
        cloud1HSV.s = 0.5f;
        cloud1HSV.v = 0.2f;

        Gradient generatedCloud1Gradient = PlanetColors.TwoPointGradient(cloud1HSV, cloud1HSV);

        ColorHSV cloud2HSV = new ColorHSV();
        
        cloud2HSV.h = hsv.h + 0.05f;
        cloud2HSV.s = hsv.s - 0.1f;
        cloud2HSV.v = Random.Range(0.8f, 1f);

        Gradient generatedCloud2Gradient = PlanetColors.GenerateColorGradient(cloud2HSV, -0.1f, -0.15f, -0.4f);
        


        _Planet.SetColors(generatedSurfaceGradient, generatedCloud1Gradient, generatedCloud2Gradient);
    }

    public void SetMisc() {
        
    }
}