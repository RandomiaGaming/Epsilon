using EpsilonEngine;
namespace DMCCR
{
    public enum FacingDirection
    {
        Right,
        Left
    };
    public sealed class Player : PhysicsObject
    {
        private int respawnTimer = 0;
        private Stage stagePlayer;
        private TextureRenderer _textureRenderer = null;
        private Texture _playerTextureRight = null;
        private Texture _playerTextureLeft = null;
        public FacingDirection FacingDirection = FacingDirection.Left;
        public const double JumpForce = 15f * 16f / 60f;
        public const double WallJumpForce = 15f * 16f / 60f;
        public const double MoveForce = 10f * 16f / 60f;
        public const double GravityForce = 9.8f * 16f * 1.5f / 60f / 60f;
        private ParticleSystem deathParticles;
        public Player(Stage stagePlayer, PhysicsLayer physicsLayer) : base(stagePlayer, false)
        {
            this.stagePlayer = stagePlayer;
            _playerTextureRight = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.PlayerRight.png"));
            _playerTextureLeft = new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.PlayerLeft.png"));
            _textureRenderer = new TextureRenderer(this, 2);
            _textureRenderer.Texture = _playerTextureRight;
            deathParticles = new ParticleSystem(this, new Texture(Game, System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Epsilon.Textures.Death Particle Texture.png")), 3);
            deathParticles.EmissionRate = 0;
            deathParticles.particleLifeTime = 25;
            deathParticles.particleSpeed = 2;
            SetColliderShape(new Rect[1] { new Rect(0, 0, 11, 11) });
            LogCollisionsUp = true;
            LogCollisionsRight = true;
            LogCollisionsDown = true;
            LogCollisionsLeft = true;
            CollisionPhysicsLayers = physicsLayer;
        }
        private bool _leftMouseButtonPressedLastFrame = false;
        private bool _rightMouseButtonPressedLastFrame = false;
        private bool _middleMouseButtonPressedLastFrame = false;
        private bool _F11PressedLastFrame = false;
        private Point _reusablePoint = new Point(0, 0);
        private double GradientProgress = 0.0;
        private void CameraUpdate()
        {
            _reusablePoint.X = PositionX + 6 - (Scene.RenderWidth >> 1);
            _reusablePoint.Y = PositionY + 6 - (Scene.RenderHeight >> 1);
            Scene.CameraPosition = _reusablePoint;
        }
        protected override void Update()
        {
            CameraUpdate();
            Game.BackgroundColor = GradientHelper.SampleLightHueGradient(GradientProgress, 100);
            GradientProgress += 1.0 / (10.0 * 60.0 * 10.0);
            if (GradientProgress > 1.0)
            {
                GradientProgress = 0.0;
            }
            if (respawnTimer > 35)
            {
                _textureRenderer.Texture = null;
                deathParticles.EmissionRate = 15;
                Velocity = Vector.Zero;
                respawnTimer--;
            }
            else if (respawnTimer <= 35 && respawnTimer > 0)
            {
                _textureRenderer.Texture = null;
                deathParticles.EmissionRate = 0;
                Velocity = Vector.Zero;
                respawnTimer--;
            }
            else if (respawnTimer == 0)
            {
                _textureRenderer.Texture = null;
                deathParticles.EmissionRate = 0;
                Position = stagePlayer.CheckPointPos;
                Velocity = Vector.Zero;
                respawnTimer = -1;
            }
            else if (respawnTimer < 0)
            {
                Microsoft.Xna.Framework.Input.KeyboardState keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
                bool F11Pressed = keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F11);
                if (F11Pressed && !_F11PressedLastFrame)
                {
                    Game.IsFullScreen = !Game.IsFullScreen;
                }
                Microsoft.Xna.Framework.Input.MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
                bool leftMouseButtonPressed = mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                bool rightMouseButtonPressed = mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                bool middleMouseButtonPressed = mouseState.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                bool leftMouseButtonDown = !_leftMouseButtonPressedLastFrame && leftMouseButtonPressed;
                bool rightMouseButtonDown = !_rightMouseButtonPressedLastFrame && rightMouseButtonPressed;
                bool middleMouseButtonDown = !_middleMouseButtonPressedLastFrame && middleMouseButtonPressed;
                bool Grounded = false;
                bool Walled = false;
                foreach (PhysicsObject physicsObject in _collisionsUp)
                {
                    if (physicsObject.GetType() == typeof(Lava))
                    {
                        Kill();
                    }
                }
                foreach (PhysicsObject physicsObject in _collisionsRight)
                {
                    if (physicsObject.GetType() == typeof(Lava))
                    {
                        Kill();
                    }
                    if (physicsObject.GetType() == typeof(Ground))
                    {
                        Walled = true;
                    }
                }
                foreach (PhysicsObject physicsObject in _collisionsDown)
                {
                    if (physicsObject.GetType() == typeof(Lava))
                    {
                        Kill();
                    }
                    if (physicsObject.GetType() == typeof(Ground))
                    {
                        Grounded = true;
                    }
                }
                foreach (PhysicsObject physicsObject in _collisionsLeft)
                {
                    if (physicsObject.GetType() == typeof(Lava))
                    {
                        Kill();
                    }
                    if (physicsObject.GetType() == typeof(Ground))
                    {
                        Walled = true;
                    }
                }
                if (middleMouseButtonDown)
                {
                    Point mousePosition = new Point(mouseState.Position.X * Scene.RenderWidth / Game.ViewportWidth, (Game.ViewportHeight - mouseState.Position.Y) * Scene.RenderHeight / Game.ViewportHeight);
                    Position = mousePosition + Scene.CameraPosition;
                }
                if (rightMouseButtonDown)
                {
                    if (FacingDirection == FacingDirection.Right)
                    {
                        FacingDirection = FacingDirection.Left;
                    }
                    else
                    {
                        FacingDirection = FacingDirection.Right;
                    }
                }
                if (leftMouseButtonDown)
                {
                    if (Grounded)
                    {
                        VelocityY = JumpForce;
                    }
                    else if (Walled)
                    {
                        if (FacingDirection == FacingDirection.Right)
                        {
                            FacingDirection = FacingDirection.Left;
                        }
                        else
                        {
                            FacingDirection = FacingDirection.Right;
                        }
                        VelocityY = WallJumpForce;
                    }
                }
                if (FacingDirection == FacingDirection.Right)
                {
                    _textureRenderer.Texture = _playerTextureRight;
                    VelocityX = MoveForce;
                }
                else
                {
                    _textureRenderer.Texture = _playerTextureLeft;
                    VelocityX = MoveForce * -1;
                }
                VelocityY -= GravityForce;
                _leftMouseButtonPressedLastFrame = leftMouseButtonPressed;
                _rightMouseButtonPressedLastFrame = rightMouseButtonPressed;
                _middleMouseButtonPressedLastFrame = middleMouseButtonPressed;
                _F11PressedLastFrame = F11Pressed;
            }
        }
        public override string ToString()
        {
            return $"DMCCR.Player()";
        }
        public void Kill()
        {
            respawnTimer = 55;
            deathParticles.EmissionRate = 15;
        }
    }
}