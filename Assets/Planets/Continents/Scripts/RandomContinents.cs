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

    }

    public void SetMisc() {
        float cloudCover = CloudCover.Evaluate(Random.value);
        float waterFlow = WaterFlow.Evaluate(Random.value);

        _Planet.SetCloudCover(cloudCover);
        _Planet.SetWaterFlow(waterFlow);
    }
}