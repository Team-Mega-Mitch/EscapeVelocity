using UnityEngine;

[ExecuteInEditMode]
public class PlanetGasGiantRinged : MonoBehaviour, PlanetInterface {
    [Header("Transform")]
    [Range(0f, 2f)] public float PlanetSize = 1f;
    [Range(1f, 5f)] public float GravitySize = 2f;
    [Range(0f, 6.28f)] public float PlanetRotation = 0f;
    [Range(0f, 6.28f)] public float RingRotation = 0f;
    [Range(-1f, 1f)] public float PlanetSpeed = 0.5f;
    [Range(-1f, 1f)] public float RingSpeed = 0.5f;

    [Header("Colors")]
    public Gradient PlanetColor;
    public Gradient RingColor;

    [Header("Seeds")]
    [Range(1, 100)] public int PlanetSeed = 100;
    [Range(1, 100)] public int RingSeed = 100;

    [Header("Misc")]
    public bool RingEnabled = true;
    public Vector2 LightOrigin = new Vector2(0.3f, 0.7f);
    [Range(0f, 256)] public int Pixels = 128;

    private PlanetLayer _Planet;
    private PlanetLayer _Ring;
    private PlanetLayer _Gravity;

    private float _Timestamp = 0f;

    private void Awake() {
        Initialize();

        SetSeed(PlanetSeed, RingSeed);
        SetGravity(GravitySize);
        SetColors(PlanetColor, RingColor);
        SetPixels(Pixels);
        SetSize(PlanetSize);
        SetRotate(PlanetRotation, RingRotation);
        SetLight(LightOrigin);
        SetSpeed(PlanetSpeed, RingSpeed);
        EnableRing(RingEnabled);
    }

    public void Initialize() {
        SpriteRenderer planetRenderer = transform.Find("Planet").GetComponent<SpriteRenderer>();
        SpriteRenderer ringRenderer = transform.Find("Ring").GetComponent<SpriteRenderer>();
        SpriteRenderer gravityRenderer = transform.Find("Gravity").GetComponent<SpriteRenderer>();

        Material planetMaterial = new Material(planetRenderer.sharedMaterial);
        Material ringMaterial = new Material(ringRenderer.sharedMaterial);
        Material gravityMaterial = new Material(gravityRenderer.sharedMaterial);

        _Planet = new PlanetLayer(gameObject, planetRenderer, planetMaterial);
        _Ring = new PlanetLayer(gameObject, ringRenderer, ringMaterial);
        _Gravity = new PlanetLayer(gameObject, gravityRenderer, gravityMaterial);
    }

    public void SetSeed(int planetSeed, int ringSeed) {
        _Planet.SetMaterialProperty(ShaderProperties.Seed, planetSeed);
        _Ring.SetMaterialProperty(ShaderProperties.Seed, ringSeed);

        PlanetSeed = planetSeed;
        RingSeed = ringSeed;
    }

    public void SetGravity(float size) {
        Transform gravity = transform.Find("Gravity");
        gravity.transform.localScale = new Vector3(size, size, gravity.transform.localScale.z);

        GravitySize = size;
    }

    public void SetPixels(float ppu) {
        _Planet.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Ring.SetMaterialProperty(ShaderProperties.Pixels, ppu * 3f);
        _Gravity.SetMaterialProperty(ShaderProperties.Pixels, ppu * GravitySize);

        Pixels = (int)ppu;
    }

    public void SetLight(Vector2 position) {
        _Planet.SetMaterialProperty(ShaderProperties.LightOrigin, position * 1.3f);
        _Ring.SetMaterialProperty(ShaderProperties.LightOrigin, position * 1.3f);

        LightOrigin = position;
    }

    public void SetRotate(float planetRotation, float ringRotation) {
        _Planet.SetMaterialProperty(ShaderProperties.Rotation, planetRotation);
        _Ring.SetMaterialProperty(ShaderProperties.Rotation, ringRotation);

        PlanetRotation = planetRotation;
        RingRotation = ringRotation;
    }

    public void SetSize(float size) {
        transform.localScale = new Vector3(size, size, transform.localScale.z);

        // Scale for pixel size, without tampering "pixels" property
        _Planet.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Ring.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels * 3f);
        _Gravity.SetMaterialProperty(ShaderProperties.Pixels, size * GravitySize * Pixels);

        PlanetSize = size;
    }

    public void SetSpeed(float planetSpeed, float ringSpeed) {
        _Planet.SetMaterialProperty(ShaderProperties.Speed, planetSpeed);
        _Ring.SetMaterialProperty(ShaderProperties.Speed, ringSpeed);

        PlanetSpeed = planetSpeed;
        RingSpeed = ringSpeed;
    }

    public void SetColors(Gradient planetColors, Gradient ringColors) {
        Gradient gradientLight = new Gradient();
        Gradient gradientDark = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[3];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[3];
        float[] newTimes = new float[3] { 0f, 0.5f, 1f };
        float[] oldTimes = new float[3];

        // Set the planet colors.
        oldTimes = new float[3] { 0f, 0.2f, .4f };
        for (int i = 0; i < newTimes.Length; i++) {
            colorKey[i].color = planetColors.Evaluate(oldTimes[i]);
            colorKey[i].time = newTimes[i];

            alphaKey[i].alpha = 1;
            alphaKey[i].time = newTimes[i];
        }
        gradientLight.SetKeys(colorKey, alphaKey);

        oldTimes = new float[3] { 0.6f, 0.8f, 1f };
        for (int i = 0; i < newTimes.Length; i++) {
            colorKey[i].color = planetColors.Evaluate(oldTimes[i]);
            colorKey[i].time = newTimes[i];

            alphaKey[i].alpha = 1;
            alphaKey[i].time = newTimes[i];
        }
        gradientDark.SetKeys(colorKey, alphaKey);

        _Planet.SetMaterialProperty(ShaderProperties.GradientTex2, PlanetUtil.GenTexture(gradientLight));
        _Planet.SetMaterialProperty(ShaderProperties.GradientTex3, PlanetUtil.GenTexture(gradientDark));

        // Set the ring colors.
        oldTimes = new float[3] { 0f, 0.2f, .4f };
        for (int i = 0; i < newTimes.Length; i++) {
            colorKey[i].color = ringColors.Evaluate(oldTimes[i]);
            colorKey[i].time = newTimes[i];

            alphaKey[i].alpha = 1;
            alphaKey[i].time = newTimes[i];
        }
        gradientLight.SetKeys(colorKey, alphaKey);

        oldTimes = new float[3] { 0.6f, 0.8f, 1f };
        for (int i = 0; i < newTimes.Length; i++) {
            colorKey[i].color = ringColors.Evaluate(oldTimes[i]);
            colorKey[i].time = newTimes[i];

            alphaKey[i].alpha = 1;
            alphaKey[i].time = newTimes[i];
        }
        gradientDark.SetKeys(colorKey, alphaKey);

        _Ring.SetMaterialProperty(ShaderProperties.GradientTex2, PlanetUtil.GenTexture(gradientLight));
        _Ring.SetMaterialProperty(ShaderProperties.GradientTex3, PlanetUtil.GenTexture(gradientDark));

        PlanetColor = planetColors;
        RingColor = ringColors;
    }

    public void EnableRing(bool enabled) {
        _Ring.SetEnabled(enabled);

        RingEnabled = enabled;
    }

    public void UpdateTime(float time) {
        _Planet.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
        _Ring.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f * -3f);
    }

    private void Update() {
        if (Application.IsPlaying(gameObject)) {
            _Timestamp += Time.deltaTime;
            UpdateTime(_Timestamp);
        }
    }
}