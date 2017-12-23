using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class Sprite2D : AbstactModel
    {
        private List<Texture2D> textures = new List<Texture2D>();

        private int _iTexture;

        private float _left;
        private float _top;
        private float _height;
        private float _width;

        private float _depth;

        private Color _color = Color.White;

        #region properties
        public List<Texture2D> Textures
        {
            get
            {
                return textures;
            }

            set
            {
                textures = value;
                _iTexture = 0;
                Width = textures[0].Width;
                Height = textures[0].Height;
            }
        }

        public float Depth
        {
            get
            {
                return _depth;
            }

            set
            {
                _depth = value;
            }
        }

        public float Left
        {
            get
            {
                return _left;
            }

            set
            {
                _left = value;
            }
        }

        public float Top
        {
            get
            {
                return _top;
            }

            set
            {
                _top = value;
            }
        }

        public float Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        public float Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }
        #endregion
        public Sprite2D(float left, float top, List<Texture2D> texs)
        {
            Textures = texs;
            this.Left = left;
            this.Top = top;
            this._depth = 0;
            this.State = 0;
        }
        public Sprite2D(float left, float top, List<Texture2D> texs, float depth)
        {
            Textures = texs;
            this.Left = left;
            this.Top = top;
            this._depth = depth;
            this.State = 0;
        }


        public override bool IsSelected(Vector2 pos)
        {
            if (pos.X >= this.Left && pos.X <= this.Left + this.Width &&
                pos.Y >= this.Top && pos.Y <= this.Top + this.Height)
                return true;

            return false;
        }

        public override void Select()
        {
            //this.State = (State + 1) % 2;
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Textures.Count >= 4)
            {
                int nTexturePerType = Textures.Count / 4;
                int di = (_iTexture + 1) % nTexturePerType;
                switch (State)
                {
                    case UnitStateEnum.MOVEFORWAR:
                        _iTexture = di;
                        break;
                    case UnitStateEnum.MOVELEFT:
                        _iTexture = di + nTexturePerType * 1;
                        break;
                    case UnitStateEnum.MOVERIGHT:
                        _iTexture = di + nTexturePerType * 2;
                        break;
                    case UnitStateEnum.MOVEBACK:
                        _iTexture = di + nTexturePerType * 3;
                        break;
                }
            }
            else
            {
                _iTexture = (_iTexture + 1) % Textures.Count;
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Textures[_iTexture], new Rectangle((int)Left, (int)Top, (int)Width, (int)Height), null, Color, 0f, Vector2.Zero, SpriteEffects.None, _depth);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float scale)
        {
            spriteBatch.Draw(this.Textures[_iTexture], new Rectangle((int)Left, (int)Top, (int)(Width / scale), (int)(Height / scale)), null, Color, 0f, Vector2.Zero, SpriteEffects.None, _depth);
        }

        public override void transact(float left, float top)
        {
            if (top > Top)
                State = UnitStateEnum.MOVEFORWAR;
            if (top < Top)
                State = UnitStateEnum.MOVEBACK;
            if (left > Left)
                State = UnitStateEnum.MOVERIGHT;
            if (left < Left)
                State = UnitStateEnum.MOVELEFT;
            this.Top = top;
            this.Left = left;
            base.transact(top, left);
        }
    }
}