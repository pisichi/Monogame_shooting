﻿using Final_Assignment.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignment.AnimationManagers
{
    class AnimationManager
    {
        private Animation _animation;
        private float _timer;
        public float TimePlayed;

        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public void Play(Animation animation)
        {
            if (_animation.Equals(animation))
                return;

            _animation = animation;
            _animation.CurrentFrame = 0;
            _timer = 0;
            TimePlayed = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            TimePlayed += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;
                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, Vector2 scale)
        {
            spriteBatch.Draw(_animation.Texture,
                            position,
                            new Rectangle(_animation.AnimationRectangle.X + _animation.CurrentFrame * _animation.FrameWidth,
                                          _animation.AnimationRectangle.Y,
                                          _animation.FrameWidth,
                                          _animation.FrameHeight),
                            Color.White,
                            rotation,
                            new Vector2(_animation.FrameWidth / 2.0f, _animation.FrameHeight / 2.0f),
                            scale,
                            SpriteEffects.None,
                            0);
        }
    }
}
