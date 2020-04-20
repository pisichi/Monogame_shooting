﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Assignment
{
    class CharacterAIComponent : InputComponent
    {

        int message;
        int count = 0;
        int _bulletSkill;

        int _action = 0;
        ContentManager content;
        Texture2D _bullet;

        float waitTime = 0;
        private bool _throw;

        private Vector2 _direction;
        private float _rotation;
        private float _force;


        private int Cooldown_1;
        private int Cooldown_2;

        Random rnd = new Random();

        public GameObject bullet;

        public CharacterAIComponent(ContentManager content)
        {
            _bullet = content.Load<Texture2D>("sprites/ball");
            this.content = content;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent)
        {

            _rotation = rnd.Next(1, 6);
            CheckRemove(parent);


            if (parent.action)
            {

                if (parent.status == 1 && parent.InTurn)
                {
                    parent.shooting = false;
                    parent.InTurn = false;
                    parent.action = false;
                    parent.status = 0;
                    Console.WriteLine(parent.Name + " count : " +count);
                    Console.WriteLine(parent.Name + " status : " + parent.status);


                }
                else
                {
                    _action += 1;
                    parent.action = false;
                    if (_action == 1)
                    {
                        UseSkill(parent);
                        _throw = true;
                    }
                }
            }

            if (_throw)
            {
                parent.SendMessage(this, 2);
                waitTime += gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                if (waitTime > 0.3)
                {
                    _throw = false;
                   

                    Shoot(gameObjects, parent);
                    Singleton.Instance.CurrentTurnState = Singleton.TurnState.enemy;
                    waitTime = 0;
                }
            }



            base.Update(gameTime, gameObjects, parent);

        }

        private void UseSkill(GameObject parent)
        {
            Cooldown_1 -= 1;
            Cooldown_2 -= 1;

            if (Cooldown_1 <= 0)
            {
                Console.WriteLine(parent.Name + " call skill 1");
                parent.skill = 1;
                Cooldown_1 = 5;
            }
            else if (Cooldown_2 <= 0)
            {
                Console.WriteLine(parent.Name + " call skill 2");
                parent.skill = 2;
                Cooldown_2 = 6;
            }
        }

        private void CheckRemove(GameObject parent)
        {

            if (bullet != null && bullet.IsActive == false)
            {
                _action = 0;
                parent.shooting = false;
                parent.InTurn = false;
                bullet = null;
            }


        }

        public void Shoot(List<GameObject> gameObjects, GameObject parent)
        {

            Console.WriteLine("add bullet");
            bullet = new GameObject(null,
                                    new BulletPhysicComponent(),
                                    new BulletGraphicComponent(content, new Dictionary<string, Animation>() {
                                         { "Shoot", new Animation(_bullet, new Rectangle(0,0,100,200),1) }
                                         }),
                                    null)
            {
                Viewport = new Rectangle(0, 0, 50, 100),
                _hit = parent._hit,

            };

            _direction = new Vector2((float)Math.Cos(_rotation + (float)Math.PI / 2), (float)Math.Sin(_rotation + (float)Math.PI / 2));
            bullet.Rotation = _rotation - (float)Math.PI;

            bullet.Direction = _direction;
            bullet.Position = parent.Position + new Vector2(-120, -100);
            bullet.attack = parent.attack;
            bullet.LinearVelocity = parent.LinearVelocity * 50;
            if (_bulletSkill == 201)
            {
                bullet.status = 1;
                _bulletSkill = 0;
            }
            else if (_bulletSkill == 202)
                {
                     bullet.status = 2;
                    _bulletSkill = 0;
                }

            gameObjects.Add(bullet);
            parent.shooting = true;
            Singleton.Instance.follow = bullet;
        }


        public override void ReceiveMessage(int message, Component sender)
        {
            base.ReceiveMessage(message, sender);
            if (sender.id == 4)
            {
                _bulletSkill = message;
            }
            else
                this.message = message;
        }

        public override void Reset()
        {
            base.Reset();
        }

    }
}
