﻿
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class GuanSkillComponent : SkillComponent
    {

        Random rnd = new Random();
        int rng;

        public GuanSkillComponent()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {
            base.Draw(spriteBatch, parent);
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
            
            base.Update(gameTime, gameObjects, parent);
        }
    }
}
