using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
        public class Game1 : Microsoft.Xna.Framework.Game
        {
            private GraphicsDeviceManager graphics;
            private SpriteBatch spriteBatch;
            private Nave nave;
            private Fondo fondo;
            private int frameCounter = 0;
            private List<Enemigo1> enemigos;

            public Game1()
            {
                graphics = new GraphicsDeviceManager(this);
                enemigos = new List<Enemigo1>();
                Content.RootDirectory = "Content";            
            }

            /// <summary>
            /// Allows the game to perform any initialization it needs to before starting to run.
            /// This is where it can query for any required services and load any non-graphic
            /// related content.  Calling base.Initialize will enumerate through any components
            /// and initialize them as well.
            /// </summary>
            protected override void Initialize()
            {
                //Console.WriteLine(graphics.PreferredBackBufferHeight + "x" + graphics.PreferredBackBufferWidth);
                fondo = new Fondo(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
                nave = new Nave(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
                this.Window.Title="Space Burst";
                base.Initialize();            
            }

            /// <summary>
            /// LoadContent will be called once per game and is the place to load
            /// all of your content.
            /// </summary>
            protected override void LoadContent()
            {
                // Create a new SpriteBatch, which can be used to draw textures.
                spriteBatch = new SpriteBatch(GraphicsDevice);
                fondo.LoadContent(Content);
                nave.LoadContent(Content);
            }

            /// <summary>
            /// UnloadContent will be called once per game and is the place to unload
            /// all content.
            /// </summary>
            protected override void UnloadContent()
            {
                // TODO: Unload any non ContentManager content here
            }

            /// <summary>
            /// Allows the game to run logic such as updating the world,
            /// checking for collisions, gathering input, and playing audio.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            protected override void Update(GameTime gameTime)
            {
                // Cuidado con esto, no usamos mando.
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    this.Exit();
                fondo.Update();
                nave.Update();
                UpdateEnemigos();
                UpdateColisiones();
                base.Update(gameTime);
            }
            private void UpdateEnemigos()
            {
                frameCounter++;
                if (frameCounter > 60)
                {
                    Random r = new Random();
                    Enemigo1 e = new Enemigo1(
                        new Vector2(r.Next(graphics.PreferredBackBufferWidth), -57),
                        graphics.PreferredBackBufferHeight,
                        graphics.PreferredBackBufferWidth,
                        Content
                        );
                    enemigos.Add(e);
                    e.FueraDePantalla += new EventHandler(FueraDePantallaHandler);
                    frameCounter = 0;
                }
                enemigos.ForEach(x => x.Update());  
            }
            private void UpdateColisiones()
            {
                //eliminamos cualquier enemigo que haya colisionado contra cualquier disparo en la pantalla
                bool colision = false;
                Enemigo1 enemigoABorrar = null;
                Disparo disparoABorrar = null;

                foreach (Enemigo1 e in enemigos)
                {
                    foreach (Disparo d in nave.Disparos)
                    {
                        if(ColisionEnemigoDisparo(e, d))
                        {
                            //si hay colision ponemos el tag a true, almacenamos el disparo y el enemigo y rompemos el primer bucle
                            colision = true;
                            enemigoABorrar = e;
                            disparoABorrar = d;
                            break;                            
                        }
                    }
                    //si el tag esta activo rompemos el segundo bucle
                    if (colision)
                        break;
                }
                //si el tag esta activo borramos el enemigo y el disparo
                if (colision)
                {
                    enemigos.Remove(enemigoABorrar);
                    nave.Disparos.Remove(disparoABorrar);
                }
            }
            private void FueraDePantallaHandler(Object sender, EventArgs args)
            {
                enemigos.Remove((Enemigo1)sender);
            }
            /// <summary>
            /// This is called when the game should draw itself.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin();
                fondo.Draw(spriteBatch);
                nave.Draw(spriteBatch);
                foreach (Enemigo1 e in enemigos)
                {
                    e.Draw(spriteBatch);
                }
                spriteBatch.End();

                base.Draw(gameTime);
            }
            private bool ColisionEnemigoDisparo(Enemigo1 e, Disparo d)
            {
                //Si los rectangle de e y de d se intersectan comprobamos la colision.
                if(e.Bounds.Intersects(d.Bounds))
                    return ColisionPixel(d.Imagen, e.Imagen, d.Posicion, e.Posicion);
                return false;
            }

            public static bool ColisionPixel(Texture2D texturaA, Texture2D texturaB, Vector2 posicionA, Vector2 posicionB)
            {
                bool colisionPxAPx = false;

                uint[] bitsA = new uint[texturaA.Width * texturaA.Height];
                uint[] bitsB = new uint[texturaB.Width * texturaB.Height];

                Rectangle rectanguloA = new Rectangle(Convert.ToInt32(posicionA.X), Convert.ToInt32(posicionA.Y), texturaA.Width, texturaA.Height);
                Rectangle rectanguloB = new Rectangle(Convert.ToInt32(posicionB.X), Convert.ToInt32(posicionB.Y), texturaB.Width, texturaB.Height);
                //almacenamos los datos de los pixeles en las variables locales bitsA y bitsB
                texturaA.GetData<uint>(bitsA);
                texturaB.GetData<uint>(bitsB);
                
                //almacenamos las coordenadas que delimitaran la zona en la que trabajaremos
                int x1 = Math.Max(rectanguloA.X, rectanguloB.X);
                int x2 = Math.Min(rectanguloA.X + rectanguloA.Width, rectanguloB.X + rectanguloB.Width);

                int y1 = Math.Max(rectanguloA.Y, rectanguloB.Y);
                int y2 = Math.Min(rectanguloA.Y + rectanguloA.Height, rectanguloB.Y + rectanguloB.Height);

                for (int y = y1; y < y2; ++y)
                {
                    for (int x = x1; x < x2; ++x)
                    {
                        if (((bitsA[(x - rectanguloA.X) + (y - rectanguloA.Y) * rectanguloA.Width] & 0xFF000000) >> 24) > 20 &&
                            ((bitsB[(x - rectanguloB.X) + (y - rectanguloB.Y) * rectanguloB.Width] & 0xFF000000) >> 24) > 20)
                        {
                            //Se comprueba el canal alpha de las dos imagenes en el mismo pixel. Si los dos son visibles hay colision.
                            colisionPxAPx = true;
                            break;
                        }
                    }

                    // Rompe el bucle si la condicion ya se ha cumplido.
                    if (colisionPxAPx)
                    {
                        break;
                    }
                }

                return colisionPxAPx;
            }
        }
}
