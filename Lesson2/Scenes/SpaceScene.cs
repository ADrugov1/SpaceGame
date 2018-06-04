﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2.Scenes
{
    public class SpaceScene : Scene
    {
        private List<GameObjects> _stars = new List<GameObjects>();
        private Bullet _bullet;
        private List<Asteroid> _asteroids = new List<Asteroid>();

        public override void Load()
        {
            _stars.Clear();
            _asteroids.Clear();

            var rnd = new Random();

            var random = new Random();
            for (var i = 0; i < 30; i++)
            {
                int r = rnd.Next(5, 50);
                var position = new Point(Drawer.Width, rnd.Next(0, Drawer.Height));
                var direction = new Point(-r, 0);
                var size = new Size(3, 3);
                
                var type = random.Next(2) % 2 == 0 ? typeof(Star) : typeof(XStar);
                
                _stars.Add((GameObjects)Activator.CreateInstance(type, position, direction, size));
            }

            _bullet = new Bullet(new Point(0, rnd.Next(0, Drawer.Height)), new Point(5, 0), new Size(4, 1));

            for (var i = 0; i < 3; i++)
            {
                int r = rnd.Next(5, 50);
                var position = new Point(Drawer.Width, rnd.Next(0, Drawer.Height));
                var direction = new Point(-r / 5, 0);
                var size = new Size(r, r);
                _asteroids.Add(new Asteroid(position, direction, size));
            }

            _toDraw.Clear();
            _toDraw.AddRange(_stars);
            _toDraw.AddRange(_asteroids);
            _toDraw.Add(_bullet);

            _toUpdate.Clear();
            _toUpdate.AddRange(_stars);
            _toUpdate.AddRange(_asteroids);
        }

        public override void Update()
        {
            foreach (var obj in _toUpdate)
            {
                obj.Update();
            }

            foreach (var asteroid in _asteroids)
            {
                asteroid.Collision(_bullet);
            }

            _bullet.Update();

            _asteroids.RemoveAll(asteroid => asteroid.IsDead);
        }
    }
}