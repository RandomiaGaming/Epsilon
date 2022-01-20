using Microsoft.Xna.Framework;
namespace Epsilon
{ 
    public static class ColorHelper
    {
        public static Color FlattenMix(Color a, Color b)
        {
            return new Color((byte)MathHelper.Lerp(b.A / 255.0, a.R, b.R), (byte)MathHelper.Lerp(b.A / 255.0, a.G, b.G), (byte)MathHelper.Lerp(b.A / 255.0, a.B, b.B), (byte)(a.A + b.A));
        }
        public static Color EvenMix(Color a, Color b)
        {
            return new Color((byte)((a.R + b.R) / 2), (byte)((a.G + b.G) / 2), (byte)((a.B + b.B) / 2), (byte)(a.A + b.A));
        }
        public static Color Mix(Color a, Color b)
        {
            return new Color((byte)MathHelper.Lerp(b.A / 255.0, a.R, b.R), (byte)MathHelper.Lerp(b.A / 255.0, a.G, b.G), (byte)MathHelper.Lerp(b.A / 255.0, a.B, b.B), (byte)((1 - ((255.0 - b.A) * (255.0 - a.A))) * 255));
        }
        public static Color SampleHueGradient(double t, byte brightness)
        {
            t = MathHelper.LoopClamp(t, 0, 1);
            if (t * 6 < 1)
            {
                double localSample = MathHelper.InverseLerp(t, 0.0 / 6.0, 1.0 / 6.0);
                return SampleGradient(localSample, new Color(255, brightness, brightness), new Color(255, 255, brightness));
            }
            else if (t * 6 < 2)
            {
                double localSample = MathHelper.InverseLerp(t, 1.0 / 6.0, 2.0 / 6.0);
                return SampleGradient(localSample, new Color(255, 255, brightness), new Color(brightness, 255, brightness));
            }
            else if (t * 6 < 3)
            {
                double localSample = MathHelper.InverseLerp(t, 2.0 / 6.0, 3.0 / 6.0);
                return SampleGradient(localSample, new Color(brightness, 255, brightness), new Color(brightness, 255, 255));
            }
            else if (t * 6 < 4)
            {
                double localSample = MathHelper.InverseLerp(t, 3.0 / 6.0, 4.0 / 6.0);
                return SampleGradient(localSample, new Color(brightness, 255, 255), new Color(brightness, brightness, 255));
            }
            else if (t * 6 < 5)
            {
                double localSample = MathHelper.InverseLerp(t, 4.0 / 6.0, 5.0 / 6.0);
                return SampleGradient(localSample, new Color(brightness, brightness, 255), new Color(255, brightness, 255));
            }
            else
            {
                double localSample = MathHelper.InverseLerp(t, 5.0 / 6.0, 6.0 / 6.0);
                return SampleGradient(localSample, new Color(255, brightness, 255), new Color(255, brightness, brightness));
            }
        }
        public static Color SampleHueGradient(double t)
        {
            t = MathHelper.LoopClamp(t, 0, 1);
            if (t * 6 < 1)
            {
                double localSample = MathHelper.InverseLerp(t, 0.0 / 6.0, 1.0 / 6.0);
                return SampleGradient(localSample, new Color(255, 0, 0), new Color(255, 255, 0));
            }
            else if (t * 6 < 2)
            {
                double localSample = MathHelper.InverseLerp(t, 1.0 / 6.0, 2.0 / 6.0);
                return SampleGradient(localSample, new Color(255, 255, 0), new Color(0, 255, 0));
            }
            else if (t * 6 < 3)
            {
                double localSample = MathHelper.InverseLerp(t, 2.0 / 6.0, 3.0 / 6.0);
                return SampleGradient(localSample, new Color(0, 255, 0), new Color(0, 255, 255));
            }
            else if (t * 6 < 4)
            {
                double localSample = MathHelper.InverseLerp(t, 3.0 / 6.0, 4.0 / 6.0);
                return SampleGradient(localSample, new Color(0, 255, 255), new Color(0, 0, 255));
            }
            else if (t * 6 < 5)
            {
                double localSample = MathHelper.InverseLerp(t, 4.0 / 6.0, 5.0 / 6.0);
                return SampleGradient(localSample, new Color(0, 0, 255), new Color(255, 0, 255));
            }
            else
            {
                double localSample = MathHelper.InverseLerp(t, 5.0 / 6.0, 6.0 / 6.0);
                return SampleGradient(localSample, new Color(255, 0, 255), new Color(255, 0, 0));
            }
        }
        public static Color SampleGradient(double t, Color a, Color b)
        {
            t = MathHelper.LoopClamp(t, 0, 1);
            double _r = MathHelper.Lerp(t, a.R, b.R);
            double _g = MathHelper.Lerp(t, a.G, b.G);
            double _b = MathHelper.Lerp(t, a.B, b.B);
            return new Color((byte)_r, (byte)_g, (byte)_b);
        }
    }
}
