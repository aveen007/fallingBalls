using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;

namespace class2

{
    class Program:GameWindow
    {
        public static string TITLE = " Graphics Lab 1";

        public static int WIDTH = 600;
        public static int HEIGHT = 600;
        public static int animIndex = 0;


        //cooredinates
       float basketX, basketY;

        //public static Vector2[] animTextureIndexs = 
        //{
        //new Vector2(0f,0f),
        ////new Vector2(0.25f,0f),
        ////new Vector2(0.50f,0f),
        ////new Vector2(0.75f,0f),
        ////new Vector2(0f,0.25f),
        ////new Vector2(0.25f,0.25f),
        ////new Vector2(0.50f,0.25f),
        ////new Vector2(0.75f,0.25f),

        //};



        public Program() : base(WIDTH, HEIGHT, GraphicsMode.Default, TITLE) { }


        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            basketX = 0f;
            basketY = 0f;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
          
         textureId=   Utilities.LoadTexture(@"Images\basket.png");

        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

        }
        public int textureId;




        public static double dx = 0;
        public static double dy = 0;
        public static bool dir = true;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            animIndex = 0;
            //Console.WriteLine("Mouse position on the X axes : " + dx);
            //Console.WriteLine("Mouse position on the Y axes : " + dy);
            //animIndex=(animIndex+1)%2;
            if (Mouse[MouseButton.Right] && dx < 1.65)
            {
                Console.WriteLine(dx);

                dx += 0.05;
                

                dir = true;

            }
            else
            {
                if (Mouse[MouseButton.Left] && dx > -0.1)
                {
                    Console.WriteLine(dx);

                    dx -= 0.05;
                    dir = false;


                }
                else
                {
                    if (Keyboard[Key.Down] && dy >- 0.05)
                    {
                        Console.WriteLine(dy);

                        dy -= 0.05;
                        dir = false;


                    }
                    else
                    {
                        if (Keyboard[Key.Up] && dy < 0.33)
                        {
                            Console.WriteLine(dy);


                            dy += 0.05;
                            dir = false;


                        }
                    }
                }

            }
      

          
        }

  
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.SkyBlue);

            GL.BindTexture(TextureTarget.Texture2D,textureId);
             GL.LoadIdentity();


            GL.Translate(dx, dy, 0);

                GL.Begin(BeginMode.Quads);
     


                GL.TexCoord2(basketX,basketY);

         
                GL.Vertex2(-0.9, -0.6);
                GL.TexCoord2(basketX + 1, basketY);


                GL.Vertex2(-0.5, -0.6);
                GL.TexCoord2(basketX + 1, basketY + 1);

     

                GL.Vertex2(-0.5, -1);
                GL.TexCoord2(basketX, basketY + 1);


                GL.Vertex2(-0.9, -1);

                GL.End();
                GL.BindTexture(TextureTarget.Texture2D, 0);
       
         
            SwapBuffers();
        }


        static void Main(string[] args)
        {

            Program myGameWin = new Program();
            myGameWin.Run(5,5);
        
        }
    
    }
}
      

