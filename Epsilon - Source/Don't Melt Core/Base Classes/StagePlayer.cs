using System;
using System.Collections.Generic;
namespace DontMelt
{
    public sealed class StagePlayer
    {
        public static Point viewPortPixelRect { get; private set; } = new Point(16 * 24, 9 * 24);
        public Point cameraPosition { get; set; } = Point.Zero;

        private List<StageItem> stageItems = new List<StageItem>();

        public readonly InputManager inputManager = null;
        public StageData stageData { get; private set; } = null;
        public StagePlayer(StageData stageData)
        {
            if (stageData is null)
            {
                throw new NullReferenceException();
            }
            this.stageData = stageData;
            inputManager = new InputManager(this);
            Regenerate();
        }

        public bool playerIsDead = false;
        private void Regenerate()
        {
            playerIsDead = false;

            stageItems = new List<StageItem>();

            if (stageData is not null)
            {
                foreach (TileData tileData in stageData.tilemapData)
                {
                    StageItem newItem = null;
                    if (tileData.stageItem == "Player")
                    {
                        newItem = new Player(this)
                        {
                            position = new Point(tileData.position.x * 16, tileData.position.y * 16),
                        };
                    }
                    else if (tileData.stageItem == "Ground")
                    {
                        newItem = new Ground(this)
                        {
                            position = new Point(tileData.position.x * 16, tileData.position.y * 16),
                        };
                    }
                    else if (tileData.stageItem == "Lava")
                    {
                        newItem = new Lava(this)
                        {
                            position = new Point(tileData.position.x * 16, tileData.position.y * 16),
                        };
                    }
                    else if (tileData.stageItem == "NoJump")
                    {
                        newItem = new NoJump(this)
                        {
                            position = new Point(tileData.position.x * 16, tileData.position.y * 16),
                        };
                    }
                    else if (tileData.stageItem == "Spring")
                    {
                        newItem = new Spring(this)
                        {
                            position = new Point(tileData.position.x * 16, tileData.position.y * 16),
                        };
                    }
                    else if (tileData.stageItem == "BounceBox")
                    {
                        newItem = new BounceBox(this)
                        {
                            position = new Point(tileData.position.x * 16, tileData.position.y * 16),
                        };
                    }
                    if (newItem is not null)
                    {
                        stageItems.Add(newItem);
                    }
                }
            }
        }
        public void Tick(InputPacket packet)
        {
            if (playerIsDead)
            {
                Regenerate();
            }

            inputManager.UpdateInput(packet);

            List<Collider> loadedColliders = new List<Collider>();
            foreach (StageItem stageItem in stageItems)
            {
                if (stageItem.collider is not null)
                {
                    loadedColliders.Add(stageItem.collider);
                }
            }

            foreach (StageItem stageItem in stageItems)
            {
                if (stageItem.rigidbody is not null)
                {
                    stageItem.rigidbody.TickMovement(loadedColliders);
                }
            }

            foreach (StageItem stageItem in stageItems)
            {
                if (stageItem.collisionLogger is not null)
                {
                    stageItem.collisionLogger.LogCollisions(loadedColliders);
                }
            }

            foreach (StageItem stageItem in stageItems)
            {
                stageItem.Update();
            }
        }
        public Texture Render()
        {
            Texture frame = new Texture(viewPortPixelRect.x, viewPortPixelRect.y, new Color(255, 255, 155, 255));

            foreach (StageItem gameObject in stageItems)
            {
                if (gameObject.texture is not null)
                {
                    TextureHelper.Blitz(gameObject.texture, frame, new Point(gameObject.position.x - cameraPosition.x, gameObject.position.y - cameraPosition.y));
                }
            }

            return frame;
        }
    }
}