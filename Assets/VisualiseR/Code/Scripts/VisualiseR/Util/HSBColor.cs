using System;
using UnityEngine;

namespace VisualiseR.Util
{
    /// <summary>
    ///  Provides a Hue/Saturation/Brightness/Alpha color model in addition to Unity's built in Red/Green/Blue/Alpha colors. 
    /// It is useful for interpolating between colors in a more pleasing fashion.
    /// Author: Jonathan Czeck (aarku)
    /// <see cref="http://wiki.unity3d.com/index.php?title=HSBColor"/>
    /// </summary>
    [Serializable]
    public struct HSBColor
    {
        public float H;
        public float S;
        public float B;
        public float A;

        private static readonly double TOLERANCE = 0.1f;

        public HSBColor(float h, float s, float b, float a)
        {
            H = h;
            S = s;
            B = b;
            A = a;
        }

        public HSBColor(float h, float s, float b)
        {
            H = h;
            S = s;
            B = b;
            A = 1f;
        }

        public HSBColor(Color col)
        {
            HSBColor temp = FromColor(col);
            H = temp.H;
            S = temp.S;
            B = temp.B;
            A = temp.A;
        }

        public static HSBColor FromColor(Color color)
        {
            HSBColor ret = new HSBColor(0f, 0f, 0f, color.a);

            float r = color.r;
            float g = color.g;
            float b = color.b;

            float max = Mathf.Max(r, Mathf.Max(g, b));

            if (max <= 0)
            {
                return ret;
            }

            float min = Mathf.Min(r, Mathf.Min(g, b));
            float dif = max - min;

            if (max > min)
            {
                if (Math.Abs(g - max) < TOLERANCE)
                {
                    ret.H = (b - r) / dif * 60f + 120f;
                }
                else if (Math.Abs(b - max) < TOLERANCE)
                {
                    ret.H = (r - g) / dif * 60f + 240f;
                }
                else if (b > g)
                {
                    ret.H = (g - b) / dif * 60f + 360f;
                }
                else
                {
                    ret.H = (g - b) / dif * 60f;
                }
                if (ret.H < 0)
                {
                    ret.H = ret.H + 360f;
                }
            }
            else
            {
                ret.H = 0;
            }

            ret.H *= 1f / 360f;
            ret.S = (dif / max) * 1f;
            ret.B = max;

            return ret;
        }
        
        public static Color ToColor(HSBColor hsbColor)
        {
            float r = hsbColor.B;
            float g = hsbColor.B;
            float b = hsbColor.B;
            if (Math.Abs(hsbColor.S) > TOLERANCE)
            {
                float max = hsbColor.B;
                float dif = hsbColor.B * hsbColor.S;
                float min = hsbColor.B - dif;

                float h = hsbColor.H * 360f;

                if (h < 60f)
                {
                    r = max;
                    g = h * dif / 60f + min;
                    b = min;
                }
                else if (h < 120f)
                {
                    r = -(h - 120f) * dif / 60f + min;
                    g = max;
                    b = min;
                }
                else if (h < 180f)
                {
                    r = min;
                    g = max;
                    b = (h - 120f) * dif / 60f + min;
                }
                else if (h < 240f)
                {
                    r = min;
                    g = -(h - 240f) * dif / 60f + min;
                    b = max;
                }
                else if (h < 300f)
                {
                    r = (h - 240f) * dif / 60f + min;
                    g = min;
                    b = max;
                }
                else if (h <= 360f)
                {
                    r = max;
                    g = min;
                    b = -(h - 360f) * dif / 60 + min;
                }
                else
                {
                    r = 0;
                    g = 0;
                    b = 0;
                }
            }

            return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), hsbColor.A);
        }

        public Color ToColor()
        {
            return ToColor(this);
        }

        public override string ToString()
        {
            return "H:" + H + " S:" + S + " B:" + B;
        }

        public static HSBColor Lerp(HSBColor a, HSBColor b, float t)
        {
            float h, s;

            //check special case black (color.b==0): interpolate neither hue nor saturation!
            //check special case grey (color.s==0): don't interpolate hue!
            if (Math.Abs(a.B) < TOLERANCE)
            {
                h = b.H;
                s = b.S;
            }
            else if (Math.Abs(b.B) < TOLERANCE)
            {
                h = a.H;
                s = a.S;
            }
            else
            {
                if (Math.Abs(a.S) < TOLERANCE)
                {
                    h = b.H;
                }
                else if (Math.Abs(b.S) < TOLERANCE)
                {
                    h = a.H;
                }
                else
                {
                    // works around bug with LerpAngle
                    float angle = Mathf.LerpAngle(a.H * 360f, b.H * 360f, t);
                    while (angle < 0f)
                        angle += 360f;
                    while (angle > 360f)
                        angle -= 360f;
                    h = angle / 360f;
                }
                s = Mathf.Lerp(a.S, b.S, t);
            }
            return new HSBColor(h, s, Mathf.Lerp(a.B, b.B, t), Mathf.Lerp(a.A, b.A, t));
        }

        public static void Test()
        {
            HSBColor color;

            color = new HSBColor(Color.red);
            Debug.Log("red: " + color);

            color = new HSBColor(Color.green);
            Debug.Log("green: " + color);

            color = new HSBColor(Color.blue);
            Debug.Log("blue: " + color);

            color = new HSBColor(Color.grey);
            Debug.Log("grey: " + color);

            color = new HSBColor(Color.white);
            Debug.Log("white: " + color);

            color = new HSBColor(new Color(0.4f, 1f, 0.84f, 1f));
            Debug.Log("0.4, 1f, 0.84: " + color);

            Debug.Log("164,82,84   .... 0.643137f, 0.321568f, 0.329411f  :" +
                      ToColor(new HSBColor(new Color(0.643137f, 0.321568f, 0.329411f))));
        }
    }
}