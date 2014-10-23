using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1
{
    class Disparo
    {
        private const int anchoImagen = 6;
        private const int altoImagen = 22;
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
        public event EventHandler FueraDePantalla;
        
        public Disparo(Vector2 posicion, int anchoNave, ContentManager Content)
        {
            this.posicion = posicion;
            //movemos un poco la posicion del disparo, para que salga desde el centro de la nave y no desde una esquina
            posicion.X += (anchoNave / 2);
            //lo que acabamos de centrar es la esquina superior izquierda del disparo. Así situaremos el centro alineado con el centro de la imagen
            //los 3 pixeles extra es por que la imagen del disparo no esta perfectamente centrada.
            posicion.X -= (anchoImagen / 2)+3;
            //Ya que los disparos pueden surgir en cualquier momento, y no al principio de la ejecución no tiene sentido tener un método
            //LoadContent que cargue las imágenes. En vez de eso las cargaremos en el constructor.
            imagen = Content.Load<Texture2D>("weapons");
        }
        public void Update()
        {
            posicion.Y-=5;
            bounds = new Rectangle((int)posicion.X, (int)posicion.Y, anchoImagen, altoImagen);
            if (posicion.Y <= 0)
                FueraDePantalla(this, null);
        }
        public void Draw(SpriteBatch spbtch)
        {
            spbtch.Draw(imagen, posicion, new Rectangle(0,0,anchoImagen, altoImagen), Color.White);
        }
    }
}
