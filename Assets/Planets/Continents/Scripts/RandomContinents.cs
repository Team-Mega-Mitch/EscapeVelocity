using UnityEngine;

public class RandomContinents : MonoBehaviour, RandomPlanetsInterface {
    public AnimationCurve Rotation;
    public AnimationCurve Speed;
    public AnimationCurve CloudCover;
    public AnimationCurve WaterFlow;

    private PlanetContinents _Planet;

    private void Start() {
        _Planet = GetComponent<PlanetContinents>();

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

        _Planet.SetSpeed(planetSpeed, cloudSpeed);
    }

    public void SetColors() {
        ColorHSV hsv = new ColorHSV();
        hsv.h = Random.Range(0f, 1f);
        hsv.s = Random.Range(0.9f, 1f);
        hsv.v = Random.Range(0.5f, 1f);
        Debug.Log(hsv.h + " " + hsv.s + " " + hsv.v);
        Gradient generatedLandGradient = PlanetColors.GenerateLandColorGradient(hsv);
        ColorHSV waterHSV = new ColorHSV(hsv.h, hsv.s, hsv.v);
        waterHSV.h += .25f;
        if(waterHSV.h > 1f){
            waterHSV.h -= 1f;
        }
        waterHSV.v += 0.05f;
        if(waterHSV.v > 1f){
            waterHSV.v = 1f;
        }
        Gradient waterGradient = PlanetColors.GenerateWaterColorGradient(new ColorHSV(waterHSV.h, waterHSV.s, waterHSV.v));
        float cloudHue;
        cloudHue = waterHSV.h + .11f;
        if(cloudHue > 1f){
            cloudHue -= 1f;
        }
        Gradient cloudGradient = PlanetColors.GenerateCloudColorGradient(new ColorHSV(cloudHue, .05f, .9f));
        Color atmosphere = Color.HSVToRGB(waterHSV.h, 1f, 1f);

        _Planet.SetColors(generatedLandGradient, waterGradient, cloudGradient, atmosphere);
    }

    public void SetMisc() {
        float cloudCover = CloudCover.Evaluate(Random.value);
        float waterFlow = WaterFlow.Evaluate(Random.value);

        _Planet.SetCloudCover(cloudCover);
        _Planet.SetWaterFlow(waterFlow);
    }
}