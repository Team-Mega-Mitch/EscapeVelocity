using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PlanetGasGiant : MonoBehaviour, PlanetInterface {
    [Header("Transform")]
    [Range(0f, 2f)] public float PlanetSize = 1f;
    [Range(1f, 5f)] public float GravitySize = 2f;
    [Range(0f, 6.28f)] public float Rotation = 0f;
    [Range(-1f, 1f)] public float Speed = 0.5f;

    [Header("Colors")]
    public Gradient SurfaceColor;
    public Gradient Clouds1Color;
    public Gradient Clouds2Color;

    [Header("Seeds")]
    [Range(1, 100)] public int SurfaceSeed = 1;
    [Range(1, 100)] public int Clouds1Seed = 1;
    [Range(1, 100)] public int Clouds2Seed = 1;

    [Header("Misc")]
    public Vector2 LightOrigin = new Vector2(0.3f, 0.7f);
    [Range(0f, 256)] public int Pixels = 128;
    [Range(0f, 1f)] public float Clouds1Cover = 0.35f;
    [Range(0f, 1f)] public float Clouds2Cover = 0.5f;

    private PlanetLayer _Surface;
    private PlanetLayer _Clouds1;
    private PlanetLayer _Clouds2;
    private PlanetLayer _Gravity;

    private float _Timestamp = 0f;

    private void Awake() {
        Initialize();

        SetSeed(SurfaceSeed, Clouds1Seed, Clouds2Seed);
        SetCloudCover(Clouds1Cover, Clouds2Cover);
        SetGravity(GravitySize);
        SetColors(SurfaceColor, Clouds1Color, Clouds2Color);
        SetPixels(Pixels);
        SetSize(PlanetSize);
        SetRotate(Rotation);
        SetLight(LightOrigin);
        SetSpeed(Speed);
    }

    public void Initialize() {
        SpriteRenderer surfaceRenderer = transform.Find("Surface").GetComponent<SpriteRenderer>();
        SpriteRenderer clouds1Renderer = transform.Find("Clouds1").GetComponent<SpriteRenderer>();
        SpriteRenderer clouds2Renderer = transform.Find("Clouds2").GetComponent<SpriteRenderer>();
        SpriteRenderer gravityRenderer = transform.Find("Gravity").GetComponent<SpriteRenderer>();

        Material surfaceMaterial = new Material(surfaceRenderer.sharedMaterial);
        Material clouds1Material = new Material(clouds1Renderer.sharedMaterial);
        Material clouds2Material = new Material(clouds2Renderer.sharedMaterial);
        Material gravityMaterial = new Material(gravityRenderer.sharedMaterial);

        _Surface = new PlanetLayer(gameObject, surfaceRenderer, surfaceMaterial);
        _Clouds1 = new PlanetLayer(gameObject, clouds1Renderer, clouds1Material);
        _Clouds2 = new PlanetLayer(gameObject, clouds2Renderer, clouds2Material);
        _Gravity = new PlanetLayer(gameObject, gravityRenderer, gravityMaterial);
    }

    public void SetSeed(int surfaceSeed, int clouds1Seed, int clouds2Seed) {
        _Surface.SetMaterialProperty(ShaderProperties.Seed, surfaceSeed);
        _Clouds1.SetMaterialProperty(ShaderProperties.Seed, clouds1Seed);
        _Clouds2.SetMaterialProperty(ShaderProperties.Seed, clouds2Seed);

        SurfaceSeed = surfaceSeed;
        Clouds1Seed = clouds1Seed;
        Clouds2Seed = clouds2Seed;
    }

    public void SetCloudCover(float clouds1Cover, float clouds2Cover) {
        _Clouds1.SetMaterialProperty(ShaderProperties.CloudCover, clouds1Cover);
        _Clouds2.SetMaterialProperty(ShaderProperties.CloudCover, clouds2Cover);

        Clouds1Cover = clouds1Cover;
        Clouds2Cover = clouds2Cover;
    }

    public void SetGravity(float size) {
        Transform gravity = transform.Find("Gravity");
        gravity.transform.localScale = new Vector3(size, size, gravity.transform.localScale.z);

        GravitySize = size;
    }

    public void SetPixels(float ppu) {
        _Surface.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Clouds1.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Clouds2.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Gravity.SetMaterialProperty(ShaderProperties.Pixels, ppu * GravitySize);

        Pixels = (int)ppu;
    }

    public void SetLight(Vector2 position) {
        _Surface.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Clouds1.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Clouds2.SetMaterialProperty(ShaderProperties.LightOrigin, position);

        LightOrigin = position;
    }

    public void SetRotate(float rotation) {
        _Surface.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Clouds1.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Clouds2.SetMaterialProperty(ShaderProperties.Rotation, rotation);

        Rotation = rotation;
    }

    public void SetSize(float size) {
        transform.localScale = new Vector3(size, size, transform.localScale.z);

        // Scale for pixel size, without tampering "pixels" property
        _Surface.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Clouds1.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Clouds2.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Gravity.SetMaterialProperty(ShaderProperties.Pixels, size * GravitySize * Pixels);

        PlanetSize = size;
    }

    public void SetSpeed(float speed) {
        _Surface.SetMaterialProperty(ShaderProperties.Speed, speed);
        _Clouds1.SetMaterialProperty(ShaderProperties.Speed, speed);
        _Clouds2.SetMaterialProperty(ShaderProperties.Speed, speed);

        Speed = speed;
    }

    public void SetColors(Gradient surfaceColors, Gradient clouds1Colors, Gradient clouds2Colors) {
        Dictionary<string, float> colors;

        // Set surface colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color2, 0.5f },
            { ShaderProperties.Color3, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Surface.SetMaterialProperty(element.Key, surfaceColors.Evaluate(element.Value));
        }

        // Set clouds1 colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color3, 0.33f },
            { ShaderProperties.Color2, 0.66f },
            { ShaderProperties.Color4, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Clouds1.SetMaterialProperty(element.Key, clouds1Colors.Evaluate(element.Value));
        }

        // Set clouds2 colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color3, 0.33f },
            { ShaderProperties.Color2, 0.66f },
            { ShaderProperties.Color4, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Clouds2.SetMaterialProperty(element.Key, clouds2Colors.Evaluate(element.Value));
        }

        SurfaceColor = surfaceColors;
        Clouds1Color = clouds1Colors;
        Clouds2Color = clouds2Colors;
    }

    public void UpdateTime(float time) {
        _Surface.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
        _Clouds1.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
        _Clouds2.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
    }

    private void Update() {
        if (Application.IsPlaying(gameObject)) {
            _Timestamp += Time.deltaTime;
            UpdateTime(_Timestamp);
        }
    }
}