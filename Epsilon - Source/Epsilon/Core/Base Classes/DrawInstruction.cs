using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Epsilon
{
    public struct DrawInstruction
    {
        public readonly Texture2D Texture;
        public readonly Point Offset;
        public DrawInstruction(Texture2D texture, Point offset)
        {
            Texture = texture;
            Offset = offset;
        }
    }
}
