using UnityEngine;

public static class PlanetColors{

    public static Gradient GenerateColorGradient(Color initialColor, float hueShift, float satShift, float valShift){
        return GenerateColorGradient(new ColorHSV(initialColor.r, initialColor.g, initialColor.b), hueShift, satShift, valShift);
    }
    public static Gradient GenerateColorGradient(ColorHSV initialColor, float hueShift, float satShift, float valShift){

        ColorHSV resultantColor = new ColorHSV();

        resultantColor.h = initialColor.h + hueShift;
        if(resultantColor.h > 1f){
            resultantColor.h -= 1f;
        }
        else if(resultantColor.h < 0){
            resultantColor.h += 1f;
        }

        resultantColor.s = initialColor.s + satShift;
        if(resultantColor.s > 1f){
            resultantColor.s = 1f;
        }
        else if(resultantColor.s < 0){
            resultantColor.s = 0f;
        }

        resultantColor.v = initialColor.v + valShift;
        if(resultantColor.v > 1f){
            resultantColor.v -= 1f;
        }
        else if(resultantColor.v < 0){
            resultantColor.v = 0f;
        }

        return TwoPointGradient(initialColor, resultantColor);
    }
public static Gradient TwoPointGradient(Color rgb, ColorHSV hsv){
        return TwoPointGradient(rgb, Color.HSVToRGB(hsv.h, hsv.s, hsv.v));
    }
    public static Gradient TwoPointGradient(ColorHSV hsv, Color rgb){
        return TwoPointGradient(Color.HSVToRGB(hsv.h, hsv.s, hsv.v), rgb);
    }
    public static Gradient TwoPointGradient(Color rgb1, Color rgb2){
        Color c0 = rgb1;
        Color c1 = rgb2;

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

    public static Gradient TwoPointGradient(ColorHSV hsv1, ColorHSV hsv2){
        return TwoPointGradient(Color.HSVToRGB(hsv1.h, hsv1.s, hsv1.v), Color.HSVToRGB(hsv2.h, hsv2.s, hsv2.v));
    }
}