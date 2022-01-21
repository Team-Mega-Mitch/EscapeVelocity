using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PlanetIcelands : MonoBehaviour, PlanetInterface {
    [Header("Transform")]
    [Range(0f, 2f)] public float PlanetSize = 1f;
    [Range(1f, 5f)] public float GravitySize = 2f;
    [Range(0f, 6.28f)] public float Rotation = 0f;
    [Range(-1f, 1f)] public float PlanetSpeed = 0.5f;
    [Range(-1f, 1f)] public float CloudSpeed = 0.5f;

    [Header("Colors")]
    public Gradient LandColor;
    public Gradient WaterColor;
    public Gradient CloudsColor;
    public Color AtmosphereColor;

    [Header("Seeds")]
    [Range(1, 100)] public int LandSeed = 100;
    [Range(1, 100)] public int WaterSeed = 100;
    [Range(1, 100)] public int CloudsSeed = 100;

    [Header("Misc")]
    public Vector2 LightOrigin = new Vector2(0.3f, 0.7f);
    [Range(0f, 256)] public int Pixels = 128;
    [Range(0f, 1f)] public float CloudCover = 0.5f;
    [Range(0f, 1f)] public float WaterFlow = 0.52f;

    private PlanetLayer _Land;
    private PlanetLayer _Water;
    private PlanetLayer _Clouds;
    private PlanetLayer _Atmosphere;
    private PlanetLayer _Gravity;

    private float _Timestamp = 0f;

    private void Awake() {
        Initialize();

        SetSeed(LandSeed, WaterSeed, CloudsSeed);
        SetWaterFlow(WaterFlow);
        SetCloudCover(CloudCover);
        SetGravity(GravitySize);
        SetColors(LandColor, WaterColor, CloudsColor, AtmosphereColor);
        SetPixels(Pixels);
        SetSize(PlanetSize);
        SetRotate(Rotation);
        SetLight(LightOrigin);
        SetSpeed(PlanetSpeed, CloudSpeed);
    }

    public void Initialize() {
        SpriteRenderer landRenderer = transform.Find("Land").GetComponent<SpriteRenderer>();
        SpriteRenderer waterRenderer = transform.Find("Water").GetComponent<SpriteRenderer>();
        SpriteRenderer cloudsRenderer = transform.Find("Clouds").GetComponent<SpriteRenderer>();
        SpriteRenderer atmosphereRenderer = transform.Find("Atmosphere").GetComponent<SpriteRenderer>();
        SpriteRenderer gravityRenderer = transform.Find("Gravity").GetComponent<SpriteRenderer>();

        Material landMaterial = new Material(landRenderer.sharedMaterial);
        Material waterMaterial = new Material(waterRenderer.sharedMaterial);
        Material cloudsMaterial = new Material(cloudsRenderer.sharedMaterial);
        Material atmosphereMaterial = new Material(atmosphereRenderer.sharedMaterial);
        Material gravityMaterial = new Material(gravityRenderer.sharedMaterial);

        _Land = new PlanetLayer(gameObject, landRenderer, landMaterial);
        _Water = new PlanetLayer(gameObject, waterRenderer, waterMaterial);
        _Clouds = new PlanetLayer(gameObject, cloudsRenderer, cloudsMaterial);
        _Atmosphere = new PlanetLayer(gameObject, atmosphereRenderer, atmosphereMaterial);
        _Gravity = new PlanetLayer(gameObject, gravityRenderer, gravityMaterial);
    }

    public void SetSeed(int landSeed, int waterSeed, int cloudsSeed) {
        _Land.SetMaterialProperty(ShaderProperties.Seed, landSeed);
        _Water.SetMaterialProperty(ShaderProperties.Seed, waterSeed);
        _Clouds.SetMaterialProperty(ShaderProperties.Seed, cloudsSeed);

        LandSeed = landSeed;
        WaterSeed = waterSeed;
        CloudsSeed = cloudsSeed;
    }

    public void SetGravity(float size) {
        Transform gravity = transform.Find("Gravity");
        gravity.transform.localScale = new Vector3(size, size, gravity.transform.localScale.z);

        GravitySize = size;
    }

    public void SetWaterFlow(float amount) {
        _Water.SetMaterialProperty(ShaderProperties.FlowRate, WaterFlow);

        WaterFlow = amount;
    }

    public void SetCloudCover(float amount) {
        _Clouds.SetMaterialProperty(ShaderProperties.CloudCover, CloudCover);

        CloudCover = amount;
    }

    public void SetPixels(float ppu) {
        _Land.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Water.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Clouds.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Atmosphere.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Gravity.SetMaterialProperty(ShaderProperties.Pixels, ppu * GravitySize);

        Pixels = (int)ppu;
    }

    public void SetLight(Vector2 position) {
        _Land.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Water.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Clouds.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Atmosphere.SetMaterialProperty(ShaderProperties.LightOrigin, position);

        LightOrigin = position;
    }

    public void SetRotate(float rotation) {
        _Land.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Water.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Clouds.SetMaterialProperty(ShaderProperties.Rotation, rotation);

        Rotation = rotation;
    }

    public void SetSize(float size) {
        transform.localScale = new Vector3(size, size, transform.localScale.z);

        // Scale for pixel size, without tampering "pixels" property
        _Land.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Water.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Clouds.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Atmosphere.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Gravity.SetMaterialProperty(ShaderProperties.Pixels, size * GravitySize * Pixels);

        PlanetSize = size;
    }

    public void SetSpeed(float planetSpeed, float cloudSpeed) {
        _Land.SetMaterialProperty(ShaderProperties.Speed, planetSpeed);
        _Water.SetMaterialProperty(ShaderProperties.Speed, planetSpeed);
        _Clouds.SetMaterialProperty(ShaderProperties.Speed, cloudSpeed);

        PlanetSpeed = planetSpeed;
        CloudSpeed = cloudSpeed;
    }

    public void SetColors(Gradient landColors, Gradient waterColors, Gradient cloudsColors, Color atmosphereColor) {
        Dictionary<string, float> colors;

        // Set Land colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color2, 0.5f },
            { ShaderProperties.Color3, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Land.SetMaterialProperty(element.Key, landColors.Evaluate(element.Value));
        }

        // Set water colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color2, 0.5f },
            { ShaderProperties.Color3, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Water.SetMaterialProperty(element.Key, waterColors.Evaluate(element.Value));
        }

        // Set clouds colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color3, 0.33f },
            { ShaderProperties.Color2, 0.66f },
            { ShaderProperties.Color4, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Clouds.SetMaterialProperty(element.Key, cloudsColors.Evaluate(element.Value));
        }

        // Set atmosphere colors.
        _Atmosphere.SetMaterialProperty(ShaderProperties.Color, atmosphereColor);

        LandColor = landColors;
        WaterColor = waterColors;
        CloudsColor = cloudsColors;
        AtmosphereColor = atmosphereColor;
    }

    public void UpdateTime(float time) {
        _Land.SetMaterialProperty(ShaderProperties.Timestamp, time);
        _Water.SetMaterialProperty(ShaderProperties.Timestamp, time);
        _Clouds.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
    }

    private void Update() {
        if (Application.IsPlaying(gameObject)) {
            _Timestamp += Time.deltaTime;
            UpdateTime(_Timestamp);
        }
    }
}
