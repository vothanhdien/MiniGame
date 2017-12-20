using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Global
    {
        public static ContentManager Content;
        public static KeyboardHelper keyboardHelper = new KeyboardHelper();
        public static MouseHelper mouseHelper = new MouseHelper();
    }
}