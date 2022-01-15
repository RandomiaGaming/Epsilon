namespace EpsilonEngine
{ 
    public static class ColorHelper
    {
        public static Color FlattenMix(Color a, Color b)
        {
            return new Color((byte)MathHelper.Lerp(b.a / 255.0, a.r, b.r), (byte)MathHelper.Lerp(b.a / 255.0, a.g, b.g), (byte)MathHelper.Lerp(b.a / 255.0, a.b, b.b), (byte)(a.a + b.a));
        }
        public static Color EvenMix(Color a, Color b)
        {
            return new Color((byte)((a.r + b.r) / 2), (byte)((a.g + b.g) / 2), (byte)((a.b + b.b) / 2), (byte)(a.a + b.a));
        }
        public static Color Mix(Color a, Color b)
        {
            return new Color((byte)MathHelper.Lerp(b.a / 255.0, a.r, b.r), (byte)MathHelper.Lerp(b.a / 255.0, a.g, b.g), (byte)MathHelper.Lerp(b.a / 255.0, a.b, b.b), (byte)((1 - ((255.0 - b.a) * (255.0 - a.a))) * 255));
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
            double _r = MathHelper.Lerp(t, a.r, b.r);
            double _g = MathHelper.Lerp(t, a.g, b.g);
            double _b = MathHelper.Lerp(t, a.b, b.b);
            return new Color((byte)_r, (byte)_g, (byte)_b);
        }
    }
}
