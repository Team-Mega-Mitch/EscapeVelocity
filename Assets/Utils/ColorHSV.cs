using UnityEngine;
public class ColorHSV{

    public ColorHSV(){
        
    }
    public ColorHSV(float hue, float saturation, float value)
    {
        h = hue;
        s = saturation;
        v = value;
        a = 1.0f;
    }
    public float h;
    public float s;
    public float v;
    public float a;
}