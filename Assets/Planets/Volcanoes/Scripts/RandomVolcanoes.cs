using UnityEngine;

public class RandomVolcanoes : MonoBehaviour, RandomPlanetsInterface {
    public AnimationCurve Rotation;
    public AnimationCurve Speed;
    public AnimationCurve CloudCover;
    public AnimationCurve WaterFlow;

    private PlanetVolcanoes _Planet;

    private void Start() {
        _Planet = GetComponent<PlanetVolcanoes>();

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
        hsv.s = Random.Range(0.15f, 0.65f);
        hsv.v = Random.Range(0.55f, 1f);

        

        Gradient generatedLandGradient = PlanetColors.GenerateColorGradient(hsv, -0.05f, -0.2f, -0.3f);
        ColorHSV craterHSV = new ColorHSV(hsv.h, hsv.s, hsv.v);
        Gradient craterGradient = PlanetColors.TwoPointGradient(hsv, generatedLandGradient.colorKeys[1].color);
        
        ColorHSV lavaHSV = new ColorHSV(hsv.h, hsv.s, hsv.v);
        
        lavaHSV.h = Random.Range(0f, 0.333f) + 0.194f;
        if(lavaHSV.h > 1.0f){
            lavaHSV.h -= 1.0f;
        }
        lavaHSV.s = Random.Range(0.4f, 0.9f);
        lavaHSV.v = Random.Range(0.9f, 1f);
        
        Gradient lavaGradient = PlanetColors.GenerateColorGradient(lavaHSV, -0.05f, -0.2f, -0.3f);
        
        Color atmosphere = Color.HSVToRGB(hsv.h + 0.03f, hsv.s - 0.05f, hsv.v + 0.05f);

        _Planet.SetColors(generatedLandGradient, craterGradient, lavaGradient, atmosphere);
    }

    public void SetMisc() {
        
    }
}