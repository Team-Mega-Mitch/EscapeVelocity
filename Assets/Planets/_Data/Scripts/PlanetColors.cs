using UnityEngine;

public static class PlanetColors{

public static Gradient GenerateCloudColorGradient(Color initialHSVColor){
        ColorHSV hsv = new ColorHSV(initialHSVColor.r, initialHSVColor.g, initialHSVColor.b);
        return GenerateLandColorGradient(hsv);
    }
    public static Gradient GenerateCloudColorGradient(ColorHSV initialColor){
        ColorHSV darkerColor = new ColorHSV();

        darkerColor.h = initialColor.h + .05f;
        if(darkerColor.h > 1f){
            darkerColor.h -= 1f;
        }

        darkerColor.s = initialColor.s + .40f;

        darkerColor.v = initialColor.v - .45f;

        Color c0 = Color.HSVToRGB(initialColor.h, initialColor.s, initialColor.v);
        Color c1 = Color.HSVToRGB(darkerColor.h, darkerColor.s, darkerColor.v);

        Gradient g = new Gradient();

        GradientColorKey[] gckeys = new GradientColorKey[2];
        gckeys[0].color = c0;
        gckeys[0].time = 0.0f;
        gckeys[1].color = c1;
        gckeys[1].time = 1.0f;

        GradientAlphaKey[] gakeys = new GradientAlphaKey[2];
        gakeys[0].alpha = 1.0f;
        gakeys[0].time = 0.0f;
        gakeys[1].alpha = 1.0f;
        gakeys[1].time = 0.0f;

        g.SetKeys(gckeys, gakeys);
        return g;
    }
    public static Gradient GenerateWaterColorGradient(Color initialHSVColor){
        ColorHSV hsv = new ColorHSV(initialHSVColor.r, initialHSVColor.g, initialHSVColor.b);
        return GenerateLandColorGradient(hsv);
    }
    public static Gradient GenerateWaterColorGradient(ColorHSV initialColor){
        ColorHSV darkerColor = new ColorHSV();

        darkerColor.h = initialColor.h + .03f;
        if(darkerColor.h > 1f){
            darkerColor.h -= 1f;
        }

        darkerColor.s = initialColor.s - .10f;

        darkerColor.v = initialColor.v - .30f;

        Color c0 = Color.HSVToRGB(initialColor.h, initialColor.s, initialColor.v);
        Color c1 = Color.HSVToRGB(darkerColor.h, darkerColor.s, darkerColor.v);

        Gradient g = new Gradient();

        GradientColorKey[] gckeys = new GradientColorKey[2];
        gckeys[0].color = c0;
        gckeys[0].time = 0.0f;
        gckeys[1].color = c1;
        gckeys[1].time = 1.0f;

        GradientAlphaKey[] gakeys = new GradientAlphaKey[2];
        gakeys[0].alpha = 1.0f;
        gakeys[0].time = 0.0f;
        gakeys[1].alpha = 1.0f;
        gakeys[1].time = 0.0f;

        g.SetKeys(gckeys, gakeys);
        return g;
    }
    public static Gradient GenerateLandColorGradient(Color initialHSVColor){
        ColorHSV hsv = new ColorHSV(initialHSVColor.r, initialHSVColor.g, initialHSVColor.b);
        return GenerateLandColorGradient(hsv);
    }
    public static Gradient GenerateLandColorGradient(ColorHSV initialColor){
        float hueShift;
        float satShift;
        float valShift;

        ColorHSV darkerColor = new ColorHSV();

        darkerColor.h = initialColor.h + .3f;
        if(darkerColor.h > 1f){
            darkerColor.h -= 1f;
        }

        darkerColor.s = initialColor.s - .25f;

        darkerColor.v = initialColor.v - .40f;

        return twoPointGradient(initialColor, darkerColor);
    }

        public static Gradient GenerateColorGradient(ColorHSV initialColor){
        float hueShift;
        float satShift;
        float valShift;

        ColorHSV darkerColor = new ColorHSV();

        darkerColor.h = initialColor.h;
        if(darkerColor.h > 1f){
            darkerColor.h -= 1f;
        }

        darkerColor.s = initialColor.s - .25f;

        darkerColor.v = initialColor.v - .40f;

        return twoPointGradient(initialColor, darkerColor);
    }

    static Gradient twoPointGradient(ColorHSV hsv1, ColorHSV hsv2){
        Color c0 = Color.HSVToRGB(hsv1.h, hsv1.s, hsv1.v);
        Color c1 = Color.HSVToRGB(hsv2.h, hsv2.s, hsv2.v);

        Gradient g = new Gradient();

        GradientColorKey[] gckeys = new GradientColorKey[2];
        gckeys[0].color = c0;
        gckeys[0].time = 0.0f;
        gckeys[1].color = c1;
        gckeys[1].time = 1.0f;

        GradientAlphaKey[] gakeys = new GradientAlphaKey[2];
        gakeys[0].alpha = 1.0f;
        gakeys[0].time = 0.0f;
        gakeys[1].alpha = 1.0f;
        gakeys[1].time = 0.0f;

        g.SetKeys(gckeys, gakeys);
        return g;
    }
}