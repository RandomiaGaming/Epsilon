using System;
using System.Collections.Generic;
namespace Epsilon
{
    public sealed class StagePlayer
    {
        public static EpsilonGame magicVoodoo;

        public static Point viewPortPixelRect { get; private set; } = new Point(16*8*2, 9*8*2);
        public Point cameraPosition { get; set; } = Point.Zero;

        private List<StageObject> stageItems = new List<StageObject>();

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

            stageItems = new List<StageObject>();

            if (stageData is not null)
            {
                foreach (TileData tileData in stageData.tilemapData)
                {
                    StageObject newItem = null;
                    if (tileData.stageItem == "Player")
                    {
                        newItem = new Player(this)
                        {
                            position = new Point(tileData.position.x * 8, tileData.position.y * 8),
                        };
                    }
                    else if (tileData.stageItem == "Ground")
                    {
                        newItem = new Ground(this)
                        {
                            position = new Point(tileData.position.x * 8, tileData.position.y * 8),
                        };
                    }
                    else if (tileData.stageItem == "Lava")
                    {
                        newItem = new Lava(this)
                        {
                            position = new Point(tileData.position.x * 8, tileData.position.y * 8),
                        };
                    }
                    else if (tileData.stageItem == "NoJump")
                    {
                        newItem = new NoJump(this)
                        {
                            position = new Point(tileData.position.x * 8, tileData.position.y * 8),
                        };
                    }
                    else if (tileData.stageItem == "Spring")
                    {
                        newItem = new Spring(this)
                        {
                            position = new Point(tileData.position.x * 8, tileData.position.y * 8),
                        };
                    }
                    else if (tileData.stageItem == "BounceBox")
                    {
                        newItem = new BounceBox(this)
                        {
                            position = new Point(tileData.position.x * 8, tileData.position.y * 8),
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
            foreach (StageObject stageItem in stageItems)
            {
                if (stageItem.collider is not null)
                {
                    loadedColliders.Add(stageItem.collider);
                }
            }

            foreach (StageObject stageItem in stageItems)
            {
                if (stageItem.rigidbody is not null)
                {
                    stageItem.rigidbody.TickMovement(loadedColliders);
                }
            }

            foreach (StageObject stageItem in stageItems)
            {
                if (stageItem.collisionLogger is not null)
                {
                    stageItem.collisionLogger.LogCollisions(loadedColliders);
                }
            }

            foreach (StageObject stageItem in stageItems)
            {
                stageItem.Update();
            }
        }
        public void Render()
        {
            foreach (StageObject gameObject in stageItems)
            {
                if (gameObject.texture is not null)
                {
                   magicVoodoo.DrawSprite(gameObject.position - cameraPosition, gameObject.texture);
                }
            }
        }
    }
}