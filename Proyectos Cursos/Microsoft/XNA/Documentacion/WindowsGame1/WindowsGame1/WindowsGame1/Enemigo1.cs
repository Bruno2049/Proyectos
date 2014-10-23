using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    class Enemigo1
    {
        private const int anchoImagen = 40;
        private const int altoImagen = 57;
        private Texture2D imagen;
        public Texture2D Imagen
        {
            get { return imagen; }
        }
        private Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
        }
        private Vector2 posicion;
        public Vector2 Posicion
        {
            get { return posicion; }
        }
        int altoVentana;
        int anchoVentana;
        public event EventHandler FueraDePantalla;
        public Enemigo1(Vector2 posicion, int altoVentana, int anchoVentana, ContentManager Content)
        {
            this.altoVentana = altoVentana;
            this.anchoVentana = anchoVentana;
            this.posicion = posicion;
            //Ya que los disparos pueden surgir en cualquier momento, y no al principio de la ejecución no tiene sentido tener un método
            //LoadContent que cargue las imágenes. En vez de eso las cargaremos en el constructor.
            imagen = Content.Load<Texture2D>("EnemigoBasico");
        }
        public void Update()
        {
            posicion.Y += 3;
            bounds = new Rectangle((int)posicion.X, (int)posicion.Y, anchoImagen, altoImagen);
            if (posicion.Y >= altoVentana)
                FueraDePantalla(this, null);
        }
        public void Draw(SpriteBatch spbtch)
        {
            spbtch.Draw(imagen, posicion, new Rectangle(0,0,anchoImagen, altoImagen), Color.White);
        }
    }
}
