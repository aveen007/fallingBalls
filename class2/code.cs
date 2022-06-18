using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using System.Windows.Forms;
using System.Threading;
//using OpenTK.Windowing.GraphicsLibraryFramework;
namespace class2

{
    class Program:GameWindow
    {
        public static string TITLE = " Evo GameBall";
        public static int WIDTH = 600;
        public static int HEIGHT = 600;
        //progress bar
        public static Vector2[] animTextureIndexs = 
        {
        new Vector2(0f,0f),
        new Vector2(0.25f,0f),
        new Vector2(0.50f,0f),
        new Vector2(0.75f,0f),
        };

        //cooredinates
        float basketX, basketY;
        float blockX, blockY;
        float cloudX, cloudY;
        float ballX, ballY;
        int looseBalls = 5;
        //flags
      public  bool ballFlag = false;
      public  bool blockFlag = false;
      public  bool cloudFlag = false;
      public  bool restartFlag = false;
      public  bool dead = false;
      public  bool pauseFlag = false;

        bool    rainBallFlag = false;
        bool coughtRain = false;
     

        //randoms
        Random rnd = new Random();
        Random rand = new Random();
        Random randC = new Random();
        Random rndm = new Random();
        double min = -0.8;
        double max = 0.8;
        double range = 1.6;
        //level variables
        double level = 0;
        double previosLevel = 0;
        int score;
        //text
        public static OpenTK.Graphics.TextPrinter text = new OpenTK.Graphics.TextPrinter();
        public static Font font;
        //texture and shape declaration
        public int baskettex, balltex, blocktex, skytex, starttex, restarttex,
            cloudtex, pausePlaytex, pauseStoptex, gameOvertex, playAgaintex,rainBalltex,progresstex;
         public Shape background;
         public Shape basket;
         public Shape pause;
         public Shape progressBar;
         public List<Shape> balls;
         public List<Shape> blocks;
         public List<Shape> clouds;
         public List<Shape> rainBalls;
         public Shape restart;
         public static bool dir = true;
         public double t = 5;
         public double blockt = 3;
         public double cloudt = 3;
         public double probarT = 1;
         public double rainballT = 17;
         public float ballSpead = -0.05f;
         public float blockSpead = -0.05f;
         public int animIndex = 0;
       //  public float baSpead = -0.05f;

        public Program() : base(WIDTH, HEIGHT, GraphicsMode.Default, TITLE) { }


        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            font = new Font("arial", 30, FontStyle.Bold);
            score = 0;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            //textures
           
            balltex = Utilities.LoadTexture(@"Images\ball.png");
            baskettex = Utilities.LoadTexture(@"Images\basket.png");
            blocktex = Utilities.LoadTexture(@"Images\block.png");
            skytex = Utilities.LoadTexture(@"Images\snowymountains.png");
            starttex = Utilities.LoadTexture(@"Images\start.png");
            restarttex = Utilities.LoadTexture(@"Images\restart.png");
            gameOvertex = Utilities.LoadTexture(@"Images\gameOver.png");
            playAgaintex = Utilities.LoadTexture(@"Images\playAgain.png");
            cloudtex = Utilities.LoadTexture(@"Images\cl.png");
            pausePlaytex = Utilities.LoadTexture(@"Images\pause_play.png");
            pauseStoptex = Utilities.LoadTexture(@"Images\pause_stop.png");
            progresstex = Utilities.LoadTexture(@"Images\progressBar.png");
            rainBalltex = Utilities.LoadTexture(@"Images\rainball.png");
            //initial coordinates
            basketX = -0.9f;
            basketY = -0.7f;
            blockY = 1.2f;
            cloudX= -0.3f;
            cloudX = -0.3f;
            ballY= 1.1f;

            Vector3 start_background = new Vector3(-1,1,0f);    
            Vector3 start_Basket = new Vector3(basketX, basketY, 0f);
            Vector3 start_restart = new Vector3(-0.4f, 0.2f, 0f);
            Vector3 start_pause = new Vector3(-0.4f, 0.2f, 0f);
            Vector3 start_progress = new Vector3(-0.8f, 0.2f, 0f);

            progressBar = new Shape(start_progress, 0.1f, 0.3f, progresstex);
            background = new Shape(start_background, 2, 2, skytex);
            basket = new Shape(start_Basket,0.4f,0.3f,baskettex);
            balls = new List<Shape>();
            blocks = new List<Shape>();
            clouds = new List<Shape>();
            rainBalls = new List<Shape>();
            restart = new Shape(start_restart, 0.8f, 0.4f, restarttex);
            pause = new Shape(start_pause, 0.8f, 0.8f, pauseStoptex);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

        }
        bool spacePressed = false;
        //KeyboardState keyboardState, lastKeyboardState;
        //public bool KeyPress(Key key)
        //{
        //    //return (keyboardState[key] && (keyboardState[key] != lastKeyboardState[key]));
        //    if (Keyboard[key])
        //    {
        //        spacePressed = true;
        //    }
        //    else
        //    {
        //        spacePressed = false;
        //    }
        //    return spacePressed;
        //}
        public void addToShapeList(Random rand, double range, double min, Shape shape,List<Shape> List , System.Threading.Timer Timer,double t)
        {
              var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(t);

            System.Threading.Timer timer = new System.Threading.Timer((ev) =>
            {
                double sample1 = rand.NextDouble();
                double scaled1 = (sample1 * range) + min;
              float  X = (float)scaled1;
               float Y = 1f;

                Vector3 start = new Vector3(X, Y, 0f);

                 shape = new Shape(start, shape.width, shape.height, shape.texture);
                List.Add(shape);
            }, null, startTimeSpan, periodTimeSpan);

        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //timespan
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(t);
            var startTimeSpanB = TimeSpan.Zero;
            var periodTimeSpanB = TimeSpan.FromSeconds(blockt);
            var startTimeSpanC = TimeSpan.Zero;
            var periodTimeSpanC = TimeSpan.FromSeconds(cloudt);
            var periodTimeSpanR = TimeSpan.FromSeconds(rainballT);
            var periodTimeSpanP = TimeSpan.FromSeconds(probarT);
           
            //move  balls
            if (!pauseFlag && !dead)
            {
                if (!ballFlag)
                {
                    ballFlag = true;

                    System.Threading.Timer timer = new System.Threading.Timer((ev) =>
                    {
                        double sample1 = rnd.NextDouble();
                        double scaled1 = (sample1 * range) + min;
                        ballX = (float)scaled1;
                        ballY = 1f;

                        Vector3 start_Ball = new Vector3(ballX, ballY, 0f);

                        Shape ball = new Shape(start_Ball, 0.1f, 0.1f, balltex);
                        balls.Add(ball);
                    }, null, startTimeSpan, periodTimeSpan);
              //      addToShapeList(rnd,range,min,ball,balls,new System.Threading.Timer (),t)


                }
                //cought a rainball
                if (animIndex > 3)
                {
                    coughtRain = false;
                    animIndex = 0;
                }
                if(coughtRain)
                {
                    System.Threading.Timer timer = new System.Threading.Timer((ev) =>
                    {
                        animIndex++;
                    }, null, startTimeSpan, periodTimeSpanR);

                }
                //move rainballs
                if (!pauseFlag && !dead)
                {
                    if (!rainBallFlag)
                    {
                        rainBallFlag = true;

                        System.Threading.Timer timer = new System.Threading.Timer((ev) =>
                        {
                            double sample1 = rndm.NextDouble();
                            double scaled1 = (sample1 * range) + min;
                            ballX = (float)scaled1;
                            ballY = 1f;

                            Vector3 start_rainBall = new Vector3(ballX, ballY, 0f);

                            Shape rainBall = new Shape(start_rainBall, 0.1f, 0.1f, rainBalltex);
                            balls.Add(rainBall);
                        }, null, startTimeSpan, periodTimeSpanR);
                        //      addToShapeList(rnd,range,min,ball,balls,new System.Threading.Timer (),t)


                    }
                }
              
                //move blocks
                if (!dead && !blockFlag)
                {
                    blockFlag = true;

                    System.Threading.Timer timer1 = new System.Threading.Timer((ev1) =>
                    {
                        double sample1 = rand.NextDouble();
                        double scaled1 = (sample1 * range) + min;
                        blockX = (float)scaled1;
                        blockY = 1f;

                        Vector3 start_Block = new Vector3(blockX, blockY, 0f);
                        Shape block = new Shape(start_Block, 0.4f, 0.1f, blocktex);

                        blocks.Add(block);
                    }, null, startTimeSpanB, periodTimeSpanB);
                }
                if (!dead && !cloudFlag)
                {
                    cloudFlag = true;

                    System.Threading.Timer timer2 = new System.Threading.Timer((ev2) =>
                    {
                        double sample1 = randC.NextDouble();
                        double scaled1 = (sample1 * range) + min;
                        cloudY = (float)scaled1;
                        cloudX = -1f;
                        Vector3 start_cloud = new Vector3(cloudX, cloudY, 0f);
                        Shape cloud = new Shape(start_cloud, 0.4f, 0.3f, cloudtex);
                        clouds.Add(cloud);
                    }, null, startTimeSpanC, periodTimeSpanC);
                }
                /////////
                foreach (Shape block in blocks.ToList())
                {
                    if (block.OutOfRange(new Vector3(-1, 1, 0), 2f, 2f))
                    {
                        blocks.Remove(block);
                        block.setIndexes(block.indexes[0].X, -1);

                    }
                }
                foreach (Shape ball in balls.ToList())
                {
                    if (ball.OutOfRange(new Vector3(-1, 1f, 0), 2.5f, 2f))
                    {


                        balls.Remove(ball);
                        ball.setIndexes(ball.indexes[0].X, 1);
                        if (looseBalls == 1)
                        {
                            dead = true;
                            balls = new List<Shape>();
                            blocks = new List<Shape>();
                        }
                        else
                        {
                            looseBalls--;
                        }

                    }
                }
                foreach (Shape cloud in clouds.ToList())
                {

                    if (!cloud.OutOfRange(new Vector3(-1, 1, 0), 2f, 2f))
                    {
                        cloudX += 0.005f;
                        cloud.setIndexes(cloudX, cloudY);

                    }
                    else
                    {
                        clouds.Remove(cloud);
                        cloudX = -1f;
                        cloud.setIndexes(cloudX, cloudY);

                    }
                }

                //move the basket around
                if (!dead)
                {
                    foreach (Shape block in blocks.ToList())
                    {
                        block.setIndexes(block.indexes[0].X, block.indexes[0].Y + blockSpead);
                    }
                    foreach (Shape ball in balls.ToList())
                    {
                        ballY = ball.indexes[0].Y + ballSpead;
                        ball.setIndexes(ball.indexes[0].X, ballY);
                    }

                    if (Keyboard[Key.D] && basket.indexes[1].X < 1)
                    {
                        basketX += 0.05f;
                        basket.setIndexes(basketX, basketY);

                        dir = true;

                    }
                    else
                    {
                        if (Keyboard[Key.A] && basket.indexes[0].X > -1)
                        {
                            basketX -= 0.05f;
                            basket.setIndexes(basketX, basketY);
                            dir = false;


                        }
                        else
                        {
                            if (Keyboard[Key.S] && basket.indexes[3].Y > -1)
                            {
                                basketY -= 0.05f;
                                basket.setIndexes(basketX, basketY);
                                dir = false;
                            }
                            else
                            {
                                if (Keyboard[Key.W] && basket.indexes[0].Y < -0.44)
                                {
                                    basketY += 0.05f;
                                    basket.setIndexes(basketX, basketY);
                                    dir = false;
                                }
                            }
                        }

                    }
                }
                //level
                if (score >= previosLevel + 2)
                {
                    previosLevel = score;
                    ballFlag = false;
                    rainBallFlag = false;
                    blockFlag = false;
                    blockt += 1;
                    ballSpead -= 0.005f;
                    blockSpead -= 0.005f;
                    t += 3;
                }
                //  Game over Case
                foreach (Shape block in blocks.ToList())
                {
                    if (basket.Hit(block))
                    {
                        if (!coughtRain) {
                            blocks.Remove(block);
                            dead = true;
                            blocks = new List<Shape>();
                            balls = new List<Shape>();
                        }
                       
                    }

                }
             
                //catching a ball
                foreach (Shape ball in balls.ToList())
                {
                    if ((!dead) && basket.previousState.OnTop(ball) && ball.inside(basket))
                    {
                        score++;
                        level += 0.5;
                        ballY = 1;
                        double sample1 = rnd.NextDouble();
                        double scaled1 = (sample1 * range) + min;
                        ballX = (float)scaled1;
                        ball.setIndexes(ballX, ballY);
                        balls.Remove(ball);
                        if (ball.texture == rainBalltex)
                        {
                            coughtRain = true;
                        }
                    }
                }

               // if(coughtRain)
            }
       
                //pause case
           // KeyPress(Key.Space);
                if (pauseFlag &&Keyboard[Key.Enter])
                {
                    pauseFlag = false;

                    Thread.Sleep(20);
                }
           //     KeyPress(Key.Space);

                if (!dead && Keyboard[Key.Space])
                {
                    pauseFlag = true;
                    bool b;
                    b = Keyboard[Key.Space];
                    Thread.Sleep(20);
                    b = Keyboard[Key.Space];
                  
                }
                if (pauseFlag)
                {
                        Vector3 start_pause = new Vector3(-0.4f, 0.4f, 0f);
                    if (Mouse.X < 380 && Mouse.X > 220 && Mouse.Y > 240 && Mouse.Y < 400)
                    {
                       // Console.WriteLine(Mouse.X + "***" + Mouse.Y);
                        
                        pause = new Shape(start_pause, 0.8f, 0.8f, pausePlaytex);
                        if (Mouse[MouseButton.Left])
                        {

                            pauseFlag = false;
                        }

                    }
                    else {
                        pause = new Shape(start_pause, 0.8f, 0.8f, pauseStoptex);
                    
                    }
                }
                if (dead && Keyboard[Key.Enter])
                {

                    dead = false;
                    blockFlag = false;
                    ballFlag = false;
                    rainBallFlag = false;
                    restartFlag = true;
                    //Thread.Sleep(20);
                }
                //restart case
                if (dead)
                {
                    score = 0;
                    previosLevel = 0;
                     t = 5;
                     blockt = 3;
                     cloudt = 3;
                     ballSpead = -0.05f;
                     blockSpead = -0.05f;
                     balls = new List<Shape>();
                     blocks = new List<Shape>();
                    Vector3 start_pause = new Vector3(-0.4f, 0.4f, 0f);
                    if (Mouse.X < 380 && Mouse.X > 220 && Mouse.Y > 240 && Mouse.Y < 400)
                    {
                        // Console.WriteLine(Mouse.X + "***" + Mouse.Y);

                        restart = new Shape(start_pause, 0.8f, 0.8f, playAgaintex);
                        if (Mouse[MouseButton.Left])
                        {

                            dead = false;
                            blockFlag = false;
                            ballFlag = false;
                            rainBallFlag = false;
                            
                        }

                    }
                    else
                    {
                        restart = new Shape(start_pause, 0.8f, 0.8f, gameOvertex);

                    }
                }
            
            
                ///
        }
      
       
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit);
            background.DrawShape();
            if (!dead )
            {
                if (coughtRain)
                {
                    progressBar.DrawComplexShape(animTextureIndexs[animIndex], )

                }
                foreach (Shape cloud in clouds.ToList())
                {

                    cloud.DrawShape();
                }
                foreach (Shape block in blocks.ToList())
                {
                    block.DrawShape();
                }

                basket.DrawShape();
                foreach (Shape ball in balls.ToList())
                {
                    ball.DrawShape();
                }
                foreach (Shape rainball in rainBalls.ToList())
                {
                    rainball.DrawShape();
                }
                Writer("Score:" + score, Color.Red, new RectangleF(200, 0, 200, 80));
                Writer(" "+looseBalls, Color.Red, new RectangleF(350, 0, 200, 80));
              
            }
            if (pauseFlag)
            {
                pause.DrawShape();
            }
            if (dead)
            {

                Writer("You dead", Color.Red, new RectangleF(200, 0, 200, 80));
                score = 0;
                looseBalls = 5;
                restart.DrawShape();
            }

         
            SwapBuffers();
        }
        public void Writer(String Str,Color color, RectangleF reff)
        {
            text.Begin();
            text.Print(Str, font, color, reff, OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Center);
            text.End();

        }

        static void Main(string[] args)
        {

            Program myGameWin = new Program();
            myGameWin.Run(25,25);
          
        }
    
    }
}
      

