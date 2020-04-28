﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class BulletPhysicComponent : PhysicComponent
    {

        private bool hitting;
        private bool touch;
        private bool hasPreviousPosition = false;
        private bool HasMaxY = false;
        public float maxY;
        private GameObject target;
        private float waitTime;

        public Vector2 PreviousPosition;

        public BulletPhysicComponent()
        {
            hitting = false;
            touch = false;
        }


        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {


            if (!hitting)
            {

                parent.Direction.Y += parent.gravity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                

                if (HasMaxY)
                {
                    if (maxY >= parent.Position.Y)
                    {
                        parent.Position.Y += parent.Direction.Y * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                        maxY = parent.Position.Y;
                        HasMaxY = true;
                    }
                    else
                    {
                        parent.Position.Y += parent.Direction.Y * 1000f * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                    }

                }
                else
                {
                    parent.Position.Y += parent.Direction.Y * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                    maxY = parent.Position.Y;
                    HasMaxY = true;
                }



                parent.Position.X += parent.Direction.X * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

                if (PreviousPosition.Y < parent.Position.Y && PreviousPosition.Y < maxY && hasPreviousPosition)
                {
                    maxY = PreviousPosition.Y;
                }

                PreviousPosition = parent.Position;
                hasPreviousPosition = true;



                if (parent.Position.X > 4000 || parent.Position.X < 0
                   || parent.Position.Y > 1000)
                {
                    parent.IsActive = false;
                }

                if (!touch)
                {
                    foreach (GameObject s in gameObjects)
                    {

                        if (s.IsActive && parent.IsActive && IsTouching(parent, s))
                        {
                            parent.Viewport = new Rectangle(0,0,150, 150);
                            parent.IsHit = true;
                            touch = true;
                        }
                    }
                }

                else
                {
                    foreach (GameObject s in gameObjects)
                    {

                        if (s.IsActive && parent.IsActive && IsTouching(parent, s))
                        {
                            if (s.status != 3)
                            {
                                s.HP -= parent.attack;
                            }
                            s.IsHit = true;
                            if (s.status == 0)
                            {
                                s.status = parent.status;
                            }
                            target = s;
                            hitting = true;

                        }
                    }
                }
            }

            else
            {
                waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;

                if (waitTime > 0.5)
                {
                    Console.WriteLine("hitting  " + target.Name);
                    parent.IsActive = false;
                    hitting = false;
                    touch = false;
                    waitTime = 0;
                }
            }

            parent.Rotation = (float)Math.Atan2(parent.Direction.X, -parent.Direction.Y);
        }


        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            base.Draw(spriteBatch, parent);
        }
    }
}