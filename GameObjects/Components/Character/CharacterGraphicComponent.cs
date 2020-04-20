﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Final_Assignment
{
    class CharacterGraphicComponent : GraphicComponent
    {


        int CurrentCharState;
        ContentManager content;
        Texture2D _hp;
        Texture2D _burn;


        float waitTime = 0;



        public CharacterGraphicComponent(ContentManager content, Dictionary<string, Animation> animations) : base(animations)
        {
            CurrentCharState = 1;
            _hp =  content.Load<Texture2D>("sprites/heart");
            //Texture2D _burn = content.Load<Texture2D>("sprites/fx_burn");
            //Texture2D _bari = content.Load<Texture2D>("sprites/fx_barrier");
            _burn = content.Load<Texture2D>("sprites/fx_burn");
            this.content = content;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {
           

            switch (CurrentCharState)
            {
                case 1:
                    if(parent.status == 1)
                        _animationManager.Play(_animations["Stunt"]);
                    else if(parent.HP <= 0)
                    _animationManager.Play(_animations["Die"]);
                    else
                        _animationManager.Play(_animations["Idle"]);
                    break;
                case 2:
                    waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                    if (waitTime > 0.5)
                    {
                        CurrentCharState = 1;
                        waitTime = 0;
                    }
                    _animationManager.Play(_animations["Throw"]);
                    break;
                case 3:
                    waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                    if (waitTime > 0.5)
                    {
                        CurrentCharState = 1;
                        waitTime = 0;
                    }
                    _animationManager.Play(_animations["Skill"]);
                    break;
                case 4:
                    waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                    if (waitTime > 0.5)
                    {
                        CurrentCharState = 1;
                        waitTime = 0;
                    }
                    _animationManager.Play(_animations["Hit"]);
                    break;
                case 5:
                    _animationManager.Play(_animations["Stunt"]);
                    break;
                case 6:
                    _animationManager.Play(_animations["Die"]);
                    break;
            }



            _animationManager.Update(gameTime);
            base.Update(gameTime, gameObjects, parent);
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent)
        {

            for(int i = 1; i<= parent.HP; i++)
            {
                spriteBatch.Draw(_hp, new Rectangle((int)parent.Position.X + (i * 50), (int)parent.Position.Y + 100,50,50), Color.Red);
            }


            _animationManager.Draw(spriteBatch, parent.Position, 0f, new Vector2(1, 1));
            if(parent.status == 2)
            {
                spriteBatch.Draw(_burn, parent.Position, Color.Red);
            }


            base.Draw(spriteBatch, parent);
        }

        public override void ReceiveMessage(int message, Component sender)
        {
            CurrentCharState = message;
            base.ReceiveMessage(message, sender);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
