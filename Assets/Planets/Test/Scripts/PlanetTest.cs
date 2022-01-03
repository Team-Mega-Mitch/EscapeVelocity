using UnityEngine;

[ExecuteInEditMode]
public class PlanetTest : MonoBehaviour, PlanetInterface {
    [Header("Transform")]
    [Range(0f, 2f)] public float Size = 1.0f;

    [Header("Colors")]
    public Color Color = new Color(1, 0, 0, 1);

    private PlanetLayer _Circle;

    private void Awake() {
        Initialize();

        SetColors(Color);
        SetSize(Size);
    }

    public void Initialize() {
        SpriteRenderer renderer = transform.Find("Circle").GetComponent<SpriteRenderer>();

        Material material = new Material(renderer.sharedMaterial);

        _Circle = new PlanetLayer(gameObject, renderer, material);
    }

    public void SetSize(float size) {
        transform.localScale = new Vector3(size, size, transform.localScale.z);

        Size = size;
    }

    public void SetColors(Color color) {
        _Circle.SetMaterialProperty(ShaderProperties.Color, color);

        Color = color;
    }
}
