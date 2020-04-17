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



        private bool extra;

        public GuanSkillComponent()
        {

        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {

            rng = rnd.Next(1, 11);

            if (rng >= 8 && !parent.InTurn && !parent.action)
            {
                Console.WriteLine(parent.Name + "  activated passive");
                
                parent.InTurn = true;

                parent.SendMessage(this, 3);
            }


            if(parent.skill == 1)
            {
                Console.WriteLine(parent.Name + " use skill 1");
                parent.SendMessage(this, 201);
                parent.skill = 0;
            }

            else if(parent.skill == 2)
            {
                Console.WriteLine(parent.Name + " use skill 2");
                parent.SendMessage(this, 201);
                parent.skill = 0;
            }
                
               



            base.Update(gameTime, gameObjects, parent);
        }

        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
        }
    }
}
