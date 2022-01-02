using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    /// <summary>
    /// This class represents the entire game, it implements the singleton pattern
    /// </summary>
    /// 

    enum GameState { Play, Pause, Win, Lost };
    class Game
    {

        public Bunker bunker1;
        public Bunker bunker2;
        public Bunker bunker3;


        private GameState state;

       

        public PlayerSpaceShip PlayerShip { get; set; }

        #region GameObjects management
        
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion
        private EnemyBlock enemies;


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {

            initiateGame(gameSize);





        }

        public void initiateGame(Size gameSize)
        {
            PlayerShip = new PlayerSpaceShip(10, gameSize.Width / 2, gameSize.Height - 50, side.Ally);
            AddNewGameObject(PlayerShip);
            this.gameSize = gameSize;
            this.state = GameState.Play;

            bunker1 = new Bunker((gameSize.Width / 5), gameSize.Height - 125, side.Neutral);
            bunker2 = new Bunker((gameSize.Width / 5) * 2, gameSize.Height - 125, side.Neutral);
            bunker3 = new Bunker((gameSize.Width / 5) * 3, gameSize.Height - 125, side.Neutral);

            AddNewGameObject(bunker1);
            AddNewGameObject(bunker2);
            AddNewGameObject(bunker3);
            Vecteur posEnnemie = new Vecteur(105, 107);
            enemies = new EnemyBlock(647, posEnnemie, side.Enemy);
            enemies.AddLine(5, 2, SpaceInvaders.Properties.Resources.ship4, this);
            enemies.AddLine(4, 2, SpaceInvaders.Properties.Resources.ship5, this);

            AddNewGameObject(enemies);
        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }

        public void DrawWin(Graphics g)
        { 
            g.DrawString("WIN", defaultFont, blackBrush, new PointF((float)gameSize.Width / 2, (float)gameSize.Height / 2));
        }

        public void DrawLose(Graphics g)
        {
            g.DrawString("LOSE", defaultFont, blackBrush, new PointF((float)gameSize.Width / 2, (float)gameSize.Height / 2));
        }

        public void DrawPause(Graphics g)
        {
            g.DrawString("PAUSE", defaultFont, blackBrush, new PointF((float)gameSize.Width / 2, (float)gameSize.Height / 2));
        }
        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {


            

            if (state == GameState.Play)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Draw(this, g);
                }

            }
            if (state == GameState.Lost)
            {
                DrawLose(g);

            }
            if (state == GameState.Win)
            {

                DrawWin(g);

            }

            if (state == GameState.Pause)
            {

                DrawPause(g);

            } 

        }


        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            if (enemies.Position.Y >= gameSize.Height - 125)
            {
                state = GameState.Lost;
            }

            // if space is pressed
            if (keyPressed.Contains(Keys.P)){
               
                if ( state == GameState.Play)
                {
                    
                    state = GameState.Pause;
                    ReleaseKey(Keys.P);
                }

                else if (state == GameState.Pause)
                {
                   
                    state = GameState.Play;
                    ReleaseKey(Keys.P);
                }
               
            }
           
              

            if (state == GameState.Play)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    if (gameObject.ToString()== "SpaceInvaders.SpaceShip")
                    {
                        
                    }
                    gameObject.Update(this, deltaT);
                }
            }




            // if space is pressed
            if (keyPressed.Contains(Keys.R))
            {

                if (state == GameState.Play)
                {

                    gameObjects.Clear();
                    initiateGame(gameSize);
                    
                }

                else if (state == GameState.Win)
                {
                    gameObjects.Clear();
                    initiateGame(gameSize);
                    state = GameState.Play;
                    ReleaseKey(Keys.R);
                }
                else if (state == GameState.Lost)
                {
                    gameObjects.Clear();
                    initiateGame(gameSize);
                    state = GameState.Play;
                    ReleaseKey(Keys.R);
                }


            }





           

             
            if (PlayerShip.Lives==0)
            {
                state = GameState.Lost;
            }
            

            if (!enemies.IsAlive())
            {
                state = GameState.Win;
            }


          

            // update each game object
           

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }
        #endregion
    }
}

