using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    class Fondo
    {
        private const int anchoImagen = 800;
        private const int altoImagen = 1700;
        private int altoVentana;
        private Texture2D imagen;
        private Rectangle rectangulo;
        public Fondo(int altoVentana, int anchoVentana)
        {            
            rectangulo = new Rectangle(0, altoImagen - altoVentana, anchoVentana, altoVentana);
            this.altoVentana = altoVentana;
        }
        public void LoadContent(ContentManager Content)
        {
            imagen = Content.Load<Texture2D>("spaceBackgr");
        }
        public void Update()
        {
            rectangulo.Y -= 1;
            if (rectangulo.Y <= 0)
                rectangulo.Y = ((altoImagen - altoVentana) -15);
        }
        public void Draw(SpriteBatch spbtch)
        {
            spbtch.Draw(imagen, new Vector2(0, 0), rectangulo, Color.White);
        }
    }
}
