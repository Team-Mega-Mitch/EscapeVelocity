using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PlanetDead : MonoBehaviour {
    [Header("Transform")]
    [Range(0f, 2f)] public float Size = 1.0f;
    [Range(0f, 6.28f)] public float Rotation = 0f;
    [Range(-1f, 1f)] public float Speed = 0.5f;

    [Header("Colors")]
    public Gradient SurfaceColor;
    public Gradient CraterColor;

    [Header("Seeds")]
    [Range(1, 100)] public int SurfaceSeed = 1;
    [Range(1, 100)] public int CraterSeed = 1;

    [Header("Misc")]
    public Vector2 LightOrigin = new Vector2(0.3f, 0.7f);
    [Range(0f, 256)] public int Pixels = 128;

    private PlanetLayer _Surface;
    private PlanetLayer _Craters;

    private float _Timestamp = 0f;

    private void Awake() {
        Initialize();

        SetSeed(SurfaceSeed, CraterSeed);
        SetColors(SurfaceColor, CraterColor);
        SetPixels(Pixels);
        SetSize(Size);
        SetRotate(Rotation);
        SetLight(LightOrigin);
        SetSpeed(Speed);
    }

    public void Initialize() {
        SpriteRenderer surfaceRenderer = transform.Find("Surface").GetComponent<SpriteRenderer>();
        SpriteRenderer cratersRenderer = transform.Find("Craters").GetComponent<SpriteRenderer>();

        Material surfaceMaterial = new Material(surfaceRenderer.sharedMaterial);
        Material cratersMaterial = new Material(cratersRenderer.sharedMaterial);

        _Surface = new PlanetLayer(gameObject, surfaceRenderer, surfaceMaterial);
        _Craters = new PlanetLayer(gameObject, cratersRenderer, cratersMaterial);
    }

    public void SetSeed(int surfaceSeed, int craterSeed) {
        _Surface.SetMaterialProperty(ShaderProperties.Seed, surfaceSeed);
        _Craters.SetMaterialProperty(ShaderProperties.Seed, craterSeed);

        SurfaceSeed = surfaceSeed;
        CraterSeed = craterSeed;
    }

    public void SetPixels(float ppu) {
        _Surface.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Craters.SetMaterialProperty(ShaderProperties.Pixels, ppu);

        Pixels = (int)ppu;
    }

    public void SetLight(Vector2 position) {
        _Surface.SetMaterialProperty(ShaderProperties.LightOrigin, position);
        _Craters.SetMaterialProperty(ShaderProperties.LightOrigin, position);

        LightOrigin = position;
    }

    public void SetRotate(float rotation) {
        _Surface.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Craters.SetMaterialProperty(ShaderProperties.Rotation, rotation);

        Rotation = rotation;
    }

    public void SetSize(float size) {
        transform.localScale = new Vector3(size, size, transform.transform.localScale.z);

        // Scale for pixel size, without tampering "pixels" property
        _Surface.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Craters.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);

        Size = size;
    }

    public void SetSpeed(float speed) {
        _Surface.SetMaterialProperty(ShaderProperties.Speed, speed);
        _Craters.SetMaterialProperty(ShaderProperties.Speed, speed);

        Speed = speed;
    }

    public void SetColors(Gradient surfaceColors, Gradient craterColors) {
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

        // Set crater colors.
        colors = new Dictionary<string, float> {
            { ShaderProperties.Color, 0f },
            { ShaderProperties.Color2, 1f  }
        };

        foreach (KeyValuePair<string, float> element in colors) {
            _Craters.SetMaterialProperty(element.Key, craterColors.Evaluate(element.Value));
        }

        SurfaceColor = surfaceColors;
        CraterColor = craterColors;
    }

    public void UpdateTime(float time) {
        _Surface.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
        _Craters.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.5f);
    }

    private void Update() {
        if (Application.IsPlaying(transform)) {
            _Timestamp += Time.deltaTime;
            UpdateTime(_Timestamp);
        }
    }
}