﻿
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class BulletPhysicComponent : PhysicComponent
    {
        public BulletPhysicComponent()
        {
            Console.WriteLine("bullet is here");
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
            //Start Potato Physics
            parent.Direction.Y += parent.gravity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            //parent.force = 1.3f;
            if (parent.PreviousDirection.Y <= parent.Direction.Y) 
                parent.Position += parent.Direction * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            else 
                parent.Position += parent.Direction * 1000f * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            
            parent.Position.X += parent.Direction.X * (1000f * parent.force) * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            //End Potato Physics

            if (parent.Position.X > 4000 || parent.Position.X < 0
                || parent.Position.Y < 0 || parent.Position.Y > 1000)
            {
                parent.IsActive = false;
            }


                foreach (GameObject s in gameObjects)
            {
                if(s.IsActive && parent.IsActive && IsTouching(parent,s))
                {
                    Console.WriteLine("hitting  " + s.Name);
                    s.HP -= parent.attack;
                    s.IsHit = true;
                    s.status = parent.status;
                    parent.IsActive = false;
                    
                }
            }
            parent.PreviousDirection = parent.Direction;
            //Console.WriteLine(parent.Position.X);
            //Console.WriteLine(parent.Position.Y);
            //Console.WriteLine(parent.Direction.X);
            //Console.WriteLine(parent.Direction.Y);
            //Console.WriteLine(parent.LinearVelocity);
        }

        public void Hit(List<GameObject> gameObjects, GameObject parent)
        {
            parent.IsActive = false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            base.Draw(spriteBatch, parent);
        }
    }
}
