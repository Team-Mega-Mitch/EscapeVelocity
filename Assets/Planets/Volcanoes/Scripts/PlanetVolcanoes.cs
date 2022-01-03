using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PlanetVolcanoes : MonoBehaviour, PlanetInterface {
    [Header("Transform")]
    [Range(0f, 2f)] public float Size = 1.0f;
    [Range(0f, 6.28f)] public float Rotation = 0f;
    [Range(-1f, 1f)] public float Speed = 0.5f;

    [Header("Colors")]
    public Gradient LandColor;
    public Gradient CratersColor;
    public Gradient LavaColor;
    public Color AtmosphereColor;

    [Header("Seeds")]
    [Range(1, 100)] public int LandSeed = 1;
    [Range(1, 100)] public int CratersSeed = 1;
    [Range(1, 100)] public int LavaSeed = 1;

    [Header("Misc")]
    public bool CratersEnabled = true;
    public Vector2 LightOrigin = new Vector2(0.3f, 0.7f);
    [Range(0f, 256)] public int Pixels = 128;
    [Range(0f, 1f)] public float LavaFlow = 0.4f;

    private PlanetLayer _Land;
    private PlanetLayer _Craters;
    private PlanetLayer _Lava;
    private PlanetLayer _Atmosphere;

    private float _Timestamp = 0f;

    private void Awake() {
        Initialize();

        SetSeed(LandSeed, CratersSeed, LandSeed);
        SetLavaFlow(LavaFlow);
        SetColors(LandColor, CratersColor, LavaColor, AtmosphereColor);
        SetPixels(Pixels);
        SetSize(Size);
        SetRotate(Rotation);
        SetLight(LightOrigin);
        SetSpeed(Speed);
        EnableCraters(CratersEnabled);
    }

    public void Initialize() {
        SpriteRenderer landRenderer = transform.Find("Land").GetComponent<SpriteRenderer>();
        SpriteRenderer cratersRenderer = transform.Find("Craters").GetComponent<SpriteRenderer>();
        SpriteRenderer lavaRenderer = transform.Find("Lava").GetComponent<SpriteRenderer>();
        SpriteRenderer atmosphereRenderer = transform.Find("Atmosphere").GetComponent<SpriteRenderer>();

        Material landMaterial = new Material(landRenderer.sharedMaterial);
        Material cratersMaterial = new Material(cratersRenderer.sharedMaterial);
        Material lavaMaterial = new Material(lavaRenderer.sharedMaterial);
        Material atmosphereMaterial = new Material(atmosphereRenderer.sharedMaterial);

        _Land = new PlanetLayer(gameObject, landRenderer, landMaterial);
        _Craters = new PlanetLayer(gameObject, cratersRenderer, cratersMaterial);
        _Lava = new PlanetLayer(gameObject, lavaRenderer, lavaMaterial);
        _Atmosphere = new PlanetLayer(gameObject, atmosphereRenderer, atmosphereMaterial);
    }

    public void SetSeed(int landSeed, int cratersSeed, int lavaSeed) {
        _Land.SetMaterialProperty(ShaderProperties.Seed, landSeed);
        _Craters.SetMaterialProperty(ShaderProperties.Seed, cratersSeed);
        _Lava.SetMaterialProperty(ShaderProperties.Seed, lavaSeed);

        LandSeed = landSeed;
        CratersSeed = cratersSeed;
        LavaSeed = lavaSeed;
    }

    public void SetLavaFlow(float amount) {
        _Lava.SetMaterialProperty(ShaderProperties.FlowRate, amount);

        LavaFlow = amount;
    }

    public void SetPixels(float ppu) {
        _Land.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Craters.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Lava.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Atmosphere.SetMaterialProperty(ShaderProperties.Pixels, ppu);

        Pixels = (int)ppu;
    }

    public void SetLight(Vector2 position) {
        _Land.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Craters.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Lava.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Atmosphere.SetMaterialProperty(ShaderProperties.LightOrigin, position);

        LightOrigin = position;
    }

    public void SetRotate(float rotation) {
        _Land.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Craters.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Lava.SetMaterialProperty(ShaderProperties.Rotation, rotation);

        Rotation = rotation;
    }

    public void SetSize(float size) {
        transform.localScale = new Vector3(size, size, transform.localScale.z);

        _Land.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Craters.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Lava.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Atmosphere.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);

        Size = size;
    }

    public void SetSpeed(float speed) {
        _Land.SetMaterialProperty(ShaderProperties.Speed, speed);
        _Craters.SetMaterialProperty(ShaderProperties.Speed, speed);
        _Lava.SetMaterialProperty(ShaderProperties.Speed, speed);

        Speed = speed;
    }

    public void SetColors(Gradient landColors, Gradient cratersColors, Gradient lavaColors, Color atmosphereColor) {
        Dictionary<string, float> colors;

        // Set land colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color2, 0.5f },
            { ShaderProperties.Color3, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Land.SetMaterialProperty(element.Key, landColors.Evaluate(element.Value));
        }

        // Set craters colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color2, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Craters.SetMaterialProperty(element.Key, cratersColors.Evaluate(element.Value));
        }

        // Set lava colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color2, 0.5f },
            { ShaderProperties.Color3, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Lava.SetMaterialProperty(element.Key, lavaColors.Evaluate(element.Value));
        }

        // Set atmosphere color.
        _Atmosphere.SetMaterialProperty(ShaderProperties.Color, atmosphereColor);

        LandColor = landColors;
        CratersColor = cratersColors;
        LavaColor = lavaColors;
        AtmosphereColor = atmosphereColor;
    }

    public void EnableCraters(bool enabled) {
        _Craters.SetEnabled(enabled);

        CratersEnabled = enabled;
    }

    public void UpdateTime(float time) {
        _Land.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
        _Craters.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
        _Lava.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
    }

    private void Update() {
        if (Application.IsPlaying(gameObject)) {
            _Timestamp += Time.deltaTime;
            UpdateTime(_Timestamp);
        }
    }
}
