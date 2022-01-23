using UnityEngine;

public class RandomBlackHole : MonoBehaviour, RandomPlanetsInterface {
    public AnimationCurve Rotation;
    public AnimationCurve Speed;
    public AnimationCurve CloudCover;
    public AnimationCurve WaterFlow;

    private PlanetBlackHole _Planet;

    private void Start() {
        _Planet = GetComponent<PlanetBlackHole>();

        SetSeed();
        SetRotate();
        SetSpeed();
        SetMisc();
        SetColors();
    }

    public void SetSeed() {
        int diskSeed = Random.Range(1, 100);

        _Planet.SetSeed(diskSeed);
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
        ColorHSV disk1 = new ColorHSV();
        disk1.h = Random.Range(-0.05f, .05f);
        disk1.s = Random.Range(0.75f, 1f);
        disk1.v = Random.Range(0.85f, 1f);

        ColorHSV disk2 = new ColorHSV();
        disk2.h = Random.Range(0f, 0.1f) + 0.1f;
        disk2.s = Random.Range(0.75f, 1f);
        disk2.v = 0f;
        
        Gradient generatedDiskGradient = PlanetColors.TwoPointGradient(disk2, disk1);

        Gradient generatedHoleGradient = PlanetColors.TwoPointGradient(new Color(0f, 0f, 0f), disk1);

        _Planet.SetColors(generatedHoleGradient, generatedDiskGradient);
    }

    public void SetMisc() {
        
    }
}