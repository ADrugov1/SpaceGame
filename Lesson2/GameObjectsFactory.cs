﻿using System;
using System.Drawing;
using Lesson2.Drawables;
using Lesson2.Drawables.BaseObjects;

namespace Lesson2
{
    public static class GameObjectsFactory
    {
        private static Random rnd = new Random();
        
        public static GameObjects CreateStar()
        {
            var position = new Point(rnd.Next(0, Drawer.Width), rnd.Next(0, Drawer.Height));
            var direction = new Point(-rnd.Next(50, 500), 0);
            var size = new Size(3, 3);
                
            var type = rnd.Next(2) % 2 == 0 ? typeof(Star) : typeof(XStar);

            return (GameObjects) Activator.CreateInstance(type, position, direction, size);
        }

        public static Bullet CreateBullet()
        {
            return new Bullet(new Point(0, rnd.Next(0, Drawer.Height)), new Point(500, 0), new Size(4, 1));
        }

        public static Asteroid CreateAsteroid()
        {
            var position = new Point(Drawer.Width, rnd.Next(0, Drawer.Height));
            var direction = new Point(-rnd.Next(50, 500), 0);
            var r = rnd.Next(5, 50);
            var size = new Size(r, r);
            return new Asteroid(position, direction, size);
        }
    }
}