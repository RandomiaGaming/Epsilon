namespace EpsilonEngine
{ 
    public static class GradientHelper
    {
        #region Public Static Methods
        public static Color SampleHueGradient(double sample)
        {
            if (sample < 0)
            {
                throw new System.Exception("sample must be greater than or equal to 0.");
            }
            if (sample > 1)
            {
                throw new System.Exception("sample must be less than or equal to 1.");
            }

            sample = sample * 6.0;

            if (sample < 1)
            {
                byte _s = (byte)(byte.MaxValue * sample);

                return new Color(byte.MaxValue, _s, byte.MinValue, byte.MaxValue);
            }
            else if (sample < 2)
            {
                sample = sample - 1.0;

                byte _s = (byte)(byte.MaxValue - (byte.MaxValue * sample));

                return new Color(_s, byte.MaxValue, byte.MinValue, byte.MaxValue);
            }
            else if (sample < 3)
            {
                sample = sample - 2.0;

                byte _s = (byte)(byte.MaxValue * sample);

                return new Color(byte.MinValue, byte.MaxValue, _s, byte.MaxValue);
            }
            else if (sample < 4)
            {
                sample = sample - 3.0;

                byte _s = (byte)(byte.MaxValue - (byte.MaxValue * sample));

                return new Color(byte.MinValue, _s, byte.MaxValue, byte.MaxValue);
            }
            else if (sample < 5)
            {
                sample = sample - 4.0;

                byte _s = (byte)(byte.MaxValue * sample);

                return new Color(_s, byte.MinValue, byte.MaxValue, byte.MaxValue);
            }
            else
            {
                sample = sample - 5.0;

                byte _s = (byte)(byte.MaxValue - (byte.MaxValue * sample));

                return new Color(byte.MaxValue, byte.MinValue, _s, byte.MaxValue);
            }
        }
        public static Color SampleLightHueGradient(double sample, byte brightness)
        {
            if (sample < 0)
            {
                throw new System.Exception("sample must be greater than or equal to 0.");
            }
            if (sample > 1)
            {
                throw new System.Exception("sample must be less than or equal to 1.");
            }

            sample = sample * 6.0;

            if (sample < 1)
            {
                byte _s = (byte)(brightness + ((byte.MaxValue - brightness) * sample));

                return new Color(byte.MaxValue, _s, brightness, byte.MaxValue);
            }
            else if (sample < 2)
            {
                sample = sample - 1.0;

                byte _s = (byte)(byte.MaxValue - (brightness * sample));

                return new Color(_s, byte.MaxValue, brightness, byte.MaxValue);
            }
            else if (sample < 3)
            {
                sample = sample - 2.0;

                byte _s = (byte)(brightness + ((byte.MaxValue - brightness) * sample));

                return new Color(brightness, byte.MaxValue, _s, byte.MaxValue);
            }
            else if (sample < 4)
            {
                sample = sample - 3.0;

                byte _s = (byte)(byte.MaxValue - (brightness * sample));

                return new Color(brightness, _s, byte.MaxValue, byte.MaxValue);
            }
            else if (sample < 5)
            {
                sample = sample - 4.0;

                byte _s = (byte)(brightness + ((byte.MaxValue - brightness) * sample));

                return new Color(_s, brightness, byte.MaxValue, byte.MaxValue);
            }
            else
            {
                sample = sample - 5.0;

                byte _s = (byte)(byte.MaxValue - (brightness * sample));

                return new Color(byte.MaxValue, brightness, _s, byte.MaxValue);
            }
        }
        public static Color SampleDarkHueGradient(double sample, byte brightness)
        {
            if (sample < 0)
            {
                throw new System.Exception("sample must be greater than or equal to 0.");
            }
            if (sample > 1)
            {
                throw new System.Exception("sample must be less than or equal to 1.");
            }

            sample = sample * 6.0;

            if (sample < 1)
            {
                byte _s = (byte)(brightness * sample);

                return new Color(brightness, _s, byte.MinValue, byte.MaxValue);
            }
            else if (sample < 2)
            {
                sample = sample - 1.0;

                byte _s = (byte)(brightness - (brightness * sample));

                return new Color(_s, brightness, byte.MinValue, byte.MaxValue);
            }
            else if (sample < 3)
            {
                sample = sample - 2.0;

                byte _s = (byte)(brightness * sample);

                return new Color(byte.MinValue, brightness, _s, byte.MaxValue);
            }
            else if (sample < 4)
            {
                sample = sample - 3.0;

                byte _s = (byte)(brightness - (brightness * sample));

                return new Color(byte.MinValue, _s, brightness, byte.MaxValue);
            }
            else if (sample < 5)
            {
                sample = sample - 4.0;

                byte _s = (byte)(brightness * sample);

                return new Color(_s, byte.MinValue, brightness, byte.MaxValue);
            }
            else
            {
                sample = sample - 5.0;

                byte _s = (byte)(brightness - (brightness * sample));

                return new Color(brightness, byte.MinValue, _s, byte.MaxValue);
            }
        }
        public static Color SampleBrightnessGradient(double sample)
        {
            if (sample < 0)
            {
                throw new System.Exception("sample must be greater than or equal to 0.");
            }
            if (sample > 1)
            {
                throw new System.Exception("sample must be less than or equal to 1.");
            }

            byte _s = (byte)(byte.MaxValue * sample);

            return new Color(_s, _s, _s, byte.MaxValue);
        }
        public static Color SampleGradient(double sample, Color a, Color b)
        {
            if(sample < 0)
            {
                throw new System.Exception("sample must be greater than or equal to 0.");
            }
            if(sample > 1)
            {
                throw new System.Exception("sample must be less than or equal to 1.");
            }

            byte _r = (byte)(a.R + ((b.R - a.R) * sample));
            byte _g = (byte)(a.G + ((b.G - a.G) * sample));
            byte _b = (byte)(a.B + ((b.B - a.B) * sample));
            byte _a = (byte)(a.A + ((b.A - a.A) * sample));

            return new Color(_r, _g, _b, _a);
        }
        #endregion
    }
}