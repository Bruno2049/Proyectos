using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Nave
    {
        private Rectangle rectangulo;
        private const int anchoImagen = 42;
        private const int altoImagen = 44;
        private Texture2D imagen;
        public Texture2D Imagen
        {
            get { return imagen; }
        }
        //Este rectangulo representa la posicion dentro de la ventana, mientras que el que ya teniamos
        //representa la posición dentro de la imagen.
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
        private int altoVentana;
        private int anchoVentana;
        private List<Disparo>disparos;
        public List<Disparo> Disparos
        {
            get { return disparos; }
        }
        private int frameCounter = 0;
        private ContentManager content;
        public Nave(int altoVentana, int anchoVentana)
        {
            this.altoVentana = altoVentana;
            this.anchoVentana = anchoVentana;
            posicion = new Vector2(altoVentana - altoImagen * 2, (anchoVentana - anchoImagen) / 2);
            CrearRectangulo(anchoImagen, altoImagen * 2);
            disparos = new List<Disparo>();
        }
        public void LoadContent(ContentManager Content)
        {
            this.content = Content;
            imagen = Content.Load<Texture2D>("battleship");
        }
        public void Update()
        {
            UpdateShots();
            UpdatePosition();
            UpdateRectangle();            
        }
        private void UpdateShots()
        {
            frameCounter++;
            if (Keyboard.GetState().IsKeyDown(Keys.Z) && disparos.Count < 6 && frameCounter > 7)
            {
                Disparo s = new Disparo(posicion, anchoImagen, content);
                disparos.Add(s);
                s.FueraDePantalla += new EventHandler(FueraDePantallaHandler);
                frameCounter = 0;
            }
            disparos.ForEach(x => x.Update());        
        }
        private void UpdatePosition()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && posicion.X > 5)
                posicion.X -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && posicion.X < (anchoVentana - anchoImagen))
                posicion.X += 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && posicion.Y > 5)
                posicion.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && posicion.Y < (altoVentana - altoImagen))
                posicion.Y += 5;
            bounds = new Rectangle((int)Posicion.X, (int)Posicion.Y, anchoImagen, altoImagen);
        }
        private void UpdateRectangle()
        {
            //a partir de aquí escojemos la parte de imagen que queremos dibujar y la almacenamos en rectangle,
            // en funcion de la combinaion de botones que se esten pulsando.

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                CrearRectangulo(0, altoImagen);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                CrearRectangulo(anchoImagen * 2, altoImagen);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                CrearRectangulo(anchoImagen, altoImagen);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CrearRectangulo(0, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CrearRectangulo(anchoImagen * 2, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CrearRectangulo(anchoImagen, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                CrearRectangulo(0, altoImagen * 2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                CrearRectangulo(anchoImagen * 2, altoImagen * 2);
            }
            else
            {
                CrearRectangulo(anchoImagen, altoImagen * 2);
            }
        }
        private void CrearRectangulo(int x, int y)
        {
            rectangulo = new Rectangle(x, y, anchoImagen, altoImagen);
        }
        public void Draw(SpriteBatch spbtch)
        {
            spbtch.Draw(imagen, posicion, rectangulo, Color.White);
            DrawShots(spbtch);
        }
        private void DrawShots(SpriteBatch spbtch)
        {
            foreach (Disparo s in disparos)
            {
                s.Draw(spbtch);
            }
        }
        private void FueraDePantallaHandler(Object sender, EventArgs args)
        {
            disparos.Remove((Disparo)sender);
        }
    }
}
