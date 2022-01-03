using UnityEngine;

[ExecuteInEditMode]
public class PlanetStar : MonoBehaviour, PlanetInterface {
    [Header("Transform")]
    [Range(0f, 5f)] public float Size = 1.0f;
    [Range(0f, 6.28f)] public float Rotation = 0f;
    [Range(-1f, 1f)] public float Speed = 0.5f;

    [Header("Colors")]
    public Gradient SurfaceColor;
    public Gradient FlaresColor;
    public Color EmissionColor;

    [Header("Seeds")]
    [Range(1, 100)] public int SurfaceSeed = 1;
    [Range(1, 100)] public int FlaresSeed = 1;
    [Range(1, 100)] public int EmissionSeed = 1;

    [Header("Misc")]
    [Range(0f, 256)] public int Pixels = 128;

    private PlanetLayer _Surface;
    private PlanetLayer _Flares;
    private PlanetLayer _Emission;

    private float _Timestamp = 0f;

    private void Awake() {
        Initialize();

        SetSeed(SurfaceSeed, FlaresSeed, EmissionSeed);
        SetColors(SurfaceColor, FlaresColor, EmissionColor);
        SetPixels(Pixels);
        SetSize(Size);
        SetRotate(Rotation);
        SetSpeed(Speed);
    }

    public void Initialize() {
        SpriteRenderer surfaceRenderer = transform.Find("Surface").GetComponent<SpriteRenderer>();
        SpriteRenderer flaresRenderer = transform.Find("Flares").GetComponent<SpriteRenderer>();
        SpriteRenderer emissionRenderer = transform.Find("Emission").GetComponent<SpriteRenderer>();

        Material surfaceMaterial = new Material(surfaceRenderer.sharedMaterial);
        Material flaresMaterial = new Material(flaresRenderer.sharedMaterial);
        Material emissionMaterial = new Material(emissionRenderer.sharedMaterial);

        _Surface = new PlanetLayer(gameObject, surfaceRenderer, surfaceMaterial);
        _Flares = new PlanetLayer(gameObject, flaresRenderer, flaresMaterial);
        _Emission = new PlanetLayer(gameObject, emissionRenderer, emissionMaterial);
    }

    public void SetSeed(int surfaceSeed, int flaresSeed, int emissionSeed) {
        _Surface.SetMaterialProperty(ShaderProperties.Seed, surfaceSeed);
        _Flares.SetMaterialProperty(ShaderProperties.Seed, flaresSeed);
        _Emission.SetMaterialProperty(ShaderProperties.Seed, emissionSeed);

        SurfaceSeed = surfaceSeed;
        FlaresSeed = flaresSeed;
        EmissionSeed = emissionSeed;
    }

    public void SetPixels(float ppu) {
        _Surface.SetMaterialProperty(ShaderProperties.Pixels, ppu);
        _Emission.SetMaterialProperty(ShaderProperties.Pixels, ppu * 2);
        _Flares.SetMaterialProperty(ShaderProperties.Pixels, ppu * 2);

        Pixels = (int)ppu;
    }

    public void SetRotate(float rotation) {
        _Emission.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Flares.SetMaterialProperty(ShaderProperties.Rotation, rotation);
        _Surface.SetMaterialProperty(ShaderProperties.Rotation, rotation);

        Rotation = rotation;
    }

    public void SetSize(float size) {
        transform.localScale = new Vector3(size, size, transform.localScale.z);

        // Scale for pixel size, without tampering "pixels" property
        _Surface.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels);
        _Emission.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels * 2);
        _Flares.SetMaterialProperty(ShaderProperties.Pixels, size * Pixels * 2);

        Size = size;
    }

    public void SetSpeed(float speed) {
        _Surface.SetMaterialProperty(ShaderProperties.Speed, speed);
        _Flares.SetMaterialProperty(ShaderProperties.Speed, speed * 0.5f);
        _Emission.SetMaterialProperty(ShaderProperties.Speed, speed);

        Speed = speed;
    }

    public void SetColors(Gradient surfaceColors, Gradient flaresColors, Color emissionColors) {
        _Surface.SetMaterialProperty(ShaderProperties.GradientTex, PlanetUtil.GenTexture(surfaceColors));
        _Flares.SetMaterialProperty(ShaderProperties.GradientTex, PlanetUtil.GenTexture(flaresColors));
        _Emission.SetMaterialProperty(ShaderProperties.Color, emissionColors);

        SurfaceColor = surfaceColors;
        FlaresColor = flaresColors;
        EmissionColor = emissionColors;
    }

    public void UpdateTime(float time) {
        _Surface.SetMaterialProperty(ShaderProperties.Timestamp, time * 0.1f);
        _Flares.SetMaterialProperty(ShaderProperties.Timestamp, time);
        _Emission.SetMaterialProperty(ShaderProperties.Timestamp, time);
    }

    private void Update() {
        if (Application.IsPlaying(gameObject)) {
            _Timestamp += Time.deltaTime;
            UpdateTime(_Timestamp);
        }
    }
}
