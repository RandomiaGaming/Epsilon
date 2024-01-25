namespace Epsilon
{
    public static class GradientHelper
    {
        public static Color HueGradient(double sample, byte brightness)
        {
            sample = MathHelper.LoopClamp(sample, 0, 1);
            if (sample * 6 < 1)
            {
                double localSample = MathHelper.InverseLerp(sample, 0.0 / 6.0, 1.0 / 6.0);
                return Gradient(localSample, new Color(255, brightness, brightness), new Color(255, 255, brightness));
            }
            else if (sample * 6 < 2)
            {
                double localSample = MathHelper.InverseLerp(sample, 1.0 / 6.0, 2.0 / 6.0);
                return Gradient(localSample, new Color(255, 255, brightness), new Color(brightness, 255, brightness));
            }
            else if (sample * 6 < 3)
            {
                double localSample = MathHelper.InverseLerp(sample, 2.0 / 6.0, 3.0 / 6.0);
                return Gradient(localSample, new Color(brightness, 255, brightness), new Color(brightness, 255, 255));
            }
            else if (sample * 6 < 4)
            {
                double localSample = MathHelper.InverseLerp(sample, 3.0 / 6.0, 4.0 / 6.0);
                return Gradient(localSample, new Color(brightness, 255, 255), new Color(brightness, brightness, 255));
            }
            else if (sample * 6 < 5)
            {
                double localSample = MathHelper.InverseLerp(sample, 4.0 / 6.0, 5.0 / 6.0);
                return Gradient(localSample, new Color(brightness, brightness, 255), new Color(255, brightness, 255));
            }
            else
            {
                double localSample = MathHelper.InverseLerp(sample, 5.0 / 6.0, 6.0 / 6.0);
                return Gradient(localSample, new Color(255, brightness, 255), new Color(255, brightness, brightness));
            }
        }
        public static Color Gradient(double sample, Color start, Color end)
        {
            sample = MathHelper.Clamp(sample, 0, 1);
            double r = MathHelper.Lerp(sample, start.r, end.r);
            double g = MathHelper.Lerp(sample, start.g, end.g);
            double b = MathHelper.Lerp(sample, start.b, end.b);
            return new Color((byte)r, (byte)g, (byte)b);
        }
    }
}
