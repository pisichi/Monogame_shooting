﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment
{
    class Component
    {
        public int id;
         public Component() 
        {

        }

        public virtual void Update(GameTime gameTime,List<GameObject> gameObjects , GameObject parent)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch,GameObject parent)
        {

        }
        public virtual void Reset()
        {

        }

        public virtual void ReceiveMessage(int message,Component sender)
        {

        }

    }
}
