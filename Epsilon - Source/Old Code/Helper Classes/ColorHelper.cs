namespace Epsilon
{ 
    public static class ColorHelper
    {
        public static Color EvenMix(Color a, Color b)
        {
            return new Color((a.r + b.r) / 2, (a.g + b.g) / 2, (a.b + b.b) / 2, 255);
        }
        public static Color Mix(Color back, Color front)
        {
            if (front.a == 0)
            {
                return back;
            }
            else if (front.a == 255)
            {
                return front;
            }
            else
            {
                return new Color((back.r + front.r) / 2, (back.g + front.g) / 2, (back.b + front.b) / 2, MathHelper.Clamp(back.a + front.a, 0, 255));
            }
        }
    }
}
