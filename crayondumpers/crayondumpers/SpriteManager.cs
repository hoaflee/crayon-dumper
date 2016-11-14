using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace crayondumpers
{
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        #region sound
        //load audio
        Song backgroundMusic;
        SoundEffect nukebom;
        SoundEffect sound_bullethit;
        SoundEffect sound_enemy1; SoundEffect sound_enemy2;
        SoundEffect sound_speedup;
        SoundEffect sound_shielup;
        SoundEffect sound_shootup;
        SoundEffect sound_shoot;
        SoundEffect sound_shoot2;
        SoundEffect sound_shoot3;
        SoundEffect sound_getready;
        SoundEffect sound_hited;
        SoundEffect sound_addrocket;
        SoundEffect sound_rocket;
        SoundEffect sound_enemyshoot;
        SoundEffect sound_bossexplode;
        SoundEffect sound_enemyshoot2;
        SoundEffect sound_minehit;
        SoundEffect sound_healthenvironup;
        SoundEffect sound_healthdumperup;
        SoundEffect sound_speedslow;
        SoundEffect sound_dumpersdestroy;
        SoundEffect sound_nukedanger;
        SoundEffect sound_bomup;
        SoundEffect sound_bang;
        SoundEffect sound_victory;
        SoundEffect sound_rocket_boss;
        SoundEffect sound_underhit;
        SoundEffect sound_gettrash;
        SoundEffect sound_trashfall;
        SoundEffect sound_bulletcollision;
        #endregion

        //load healthBar
        Texture2D mHealthBar, mHealthBar2, defend, iconofbom,nukeicon,rocket_button;
        boss boss;
        handofboss hand1, hand2, eye1;
        //mouse taget
        taget mousetaget;
        bool trum;
        //khoi tao background
        Texture2D paperTexture;

        MouseState mouseStateCurrent, mouseStatePrevious;
        //crayon font
        SpriteFont font;

        int nuke = 100;
        int levelshoot = 1;
        int mCurrentHealthDumpers = 100;
        int mCurrentHealthEnviron = 199;
        private int score = 0;
        private int checkbullet = 0;
        private int bom = 100;

        public bool first = false;
        float fBulletDelayTimer = 0.0f;
        
        UserControlledDumpers dumpers;

        AutomatedSprite sun, cloud, cloud2, under, icon, sos, depth1, depth2,depth3;
        #region create list
        List<AutomatedSprite> spriteList = new List<AutomatedSprite>();
        List<AutomatedSprite> itemList = new List<AutomatedSprite>();
        List<AutomatedSprite> enemyList = new List<AutomatedSprite>();
        List<AutomatedSprite> bulletList = new List<AutomatedSprite>();
        List<AutomatedSprite> shootList = new List<AutomatedSprite>();
        List<AutomatedSprite> bomList = new List<AutomatedSprite>();
        List<AutomatedSprite> mineList = new List<AutomatedSprite>();
        List<AutomatedSprite> trashList = new List<AutomatedSprite>();
        List<AutomatedSprite> playerdepth1 = new List<AutomatedSprite>();
        List<AutomatedSprite> playerdepth2 = new List<AutomatedSprite>();
        List<AutomatedSprite> playerdepth3 = new List<AutomatedSprite>();
        List<AutomatedSprite> rocket_boss_list = new List<AutomatedSprite>();
        
        List<Explosion> explosion = new List<Explosion>();
        List<boss> BOSS = new List<boss>();
        List<handofboss> hand = new List<handofboss>();
        #endregion
        #region Spawning variables
        int trashSpawnMinMilliseconds = 1500;
        int trashSpawnMaxMilliseconds = 2500;

        int depth2SpawnMinMilliseconds = 2000; int depth3SpawnMinMilliseconds = 10000;
        int depth2SpawnMaxMilliseconds = 6000; int depth3SpawnMaxMilliseconds = 15000;

        int enemySpawnMinMilliseconds = 3500; int itemSpawnMinMilliseconds = 7000;
        int enemySpawnMaxMilliseconds = 7000; int itemSpawnMaxMilliseconds = 10000;
        int mineSpawnMinMilliseconds = 12000;
        int mineSpawnMaxMilliseconds = 20000;

        int trashMinSpeed = 1;
        int trashMaxSpeed = 5;

        int enemyMinSpeed = 3;
        int enemyMaxSpeed = 5;

        int nextTrashSpawnTime = 2000;
        int nextEnemySpawnTime = 10000; 
        int nextItemSpawnTime = 15000;
        int nextMineSpawnTime = 17000;
        int nextRocketBossSpawnTime = 5000;

        int nextDepth2SpawnTime = 0; 
        int nextDepth3SpawnTime = 0;

        int nextSpawnTimeChange = 5000;
        int timeSinceLastSpawnTimeChange = 0;

        bool delaygetrealdy = false;
        bool victory = false;
        #endregion
        public bool Getvictory
        {
            get { return victory; }
            set { victory = value; }
        }
        bool gameover = false;
        public bool Getgameover
        {
            get { return gameover; }
            set { gameover = value; }
        }
        int powerUpExpiration = 0;

        public SpriteManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            //load background
            paperTexture =Game.Content.Load<Texture2D>(@"content\paper");

            //load audio
            backgroundMusic = Game.Content.Load<Song>(@"content\sound\Media - Nhac - .-.Nam lun phieu luu ky.-._5");
            nukebom = Game.Content.Load<SoundEffect>(@"content\sound\cached_bossblast");
            sound_bullethit = Game.Content.Load<SoundEffect>(@"content\sound\cached_bullethit");
            sound_enemy1 = Game.Content.Load<SoundEffect>(@"content\sound\cached_jetdive");
            sound_enemy2 = Game.Content.Load<SoundEffect>(@"content\sound\Swoosh");
            sound_speedup = Game.Content.Load<SoundEffect>(@"content\sound\cached_speedup");
            sound_shielup = Game.Content.Load<SoundEffect>(@"content\sound\shieldup");
            sound_shootup = Game.Content.Load<SoundEffect>(@"content\sound\cached_shootup");
            sound_shoot = Game.Content.Load<SoundEffect>(@"content\sound\fire");
            sound_shoot2 = Game.Content.Load<SoundEffect>(@"content\sound\fire2");
            sound_shoot3 = Game.Content.Load<SoundEffect>(@"content\sound\fire3");
            sound_getready = Game.Content.Load<SoundEffect>(@"content\sound\v_getready");
            sound_hited = Game.Content.Load<SoundEffect>(@"content\sound\statichit");
            sound_addrocket = Game.Content.Load<SoundEffect>(@"content\sound\stats");
            sound_rocket = Game.Content.Load<SoundEffect>(@"content\sound\rocket");
            sound_enemyshoot = Game.Content.Load<SoundEffect>(@"content\sound\enemyfire");
            sound_bossexplode = Game.Content.Load<SoundEffect>(@"content\sound\bossexplode");
            sound_enemyshoot2 = Game.Content.Load<SoundEffect>(@"content\sound\enemyshoot2");
            sound_minehit = Game.Content.Load<SoundEffect>(@"content\sound\minehit");
            sound_healthenvironup = Game.Content.Load<SoundEffect>(@"content\sound\healthenvirup");
            sound_healthdumperup = Game.Content.Load<SoundEffect>(@"content\sound\healthdumperup");
            sound_speedslow = Game.Content.Load<SoundEffect>(@"content\sound\speedslow");
            sound_dumpersdestroy = Game.Content.Load<SoundEffect>(@"content\sound\dumpersdestroyed a1");
            sound_nukedanger = Game.Content.Load<SoundEffect>(@"content\sound\nukedanger");
            sound_bomup = Game.Content.Load<SoundEffect>(@"content\sound\bomup");
            sound_bang = Game.Content.Load<SoundEffect>(@"content\sound\bang");
            sound_victory = Game.Content.Load<SoundEffect>(@"content\sound\victory");
            sound_rocket_boss = Game.Content.Load<SoundEffect>(@"content\sound\aler");
            sound_underhit = Game.Content.Load<SoundEffect>(@"content\sound\enemytankgun");
            sound_gettrash = Game.Content.Load<SoundEffect>(@"content\sound\Gettrash");
            sound_trashfall = Game.Content.Load<SoundEffect>(@"content\sound\trashfall");
            sound_bulletcollision = Game.Content.Load<SoundEffect>(@"content\sound\bulletcollision");

            defend = Game.Content.Load<Texture2D>(@"content\defend");

            nukeicon = Game.Content.Load<Texture2D>(@"content\nukeicon");

            rocket_button = Game.Content.Load<Texture2D>(@"content\rocket_button");

            iconofbom = Game.Content.Load<Texture2D>(@"content\iconofbom");

            mHealthBar = Game.Content.Load<Texture2D>(@"content\HealthBar"); mHealthBar2 = Game.Content.Load<Texture2D>(@"content\HealthBar2");
            //load font
            font= Game.Content.Load<SpriteFont>(@"content\font");
             //load sun
            mousetaget = new taget(Game.Content.Load<Texture2D>(@"content\taget"), new Vector2(200,200), new Point(227, 210),
                0, new Point(0, 0), new Point(4,3), Vector2.Zero, 500, 0.4f);
          
            //load under
            under = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\under"), new Vector2(0, 693),
                new Point(1366, 75), 0, new Point(0, 0), new Point(1, 1), Vector2.Zero,1000, 1f);

            //load cloud
            sun = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\sun"), new Vector2(0, 0),
                new Point(600, 400), 0, new Point(0, 0), new Point(3, 1), Vector2.Zero, 400, 0.6f);
            depth1 = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\depth1\2"),
                new Vector2(000, 590), new Point(500, 300), 0, new Point(2, 0), new Point(1, 1), new Vector2(2, 0), 100, 1f);
            depth2 = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\depth1\2"),
                new Vector2(500, 590), new Point(500, 300), 0, new Point(2, 0), new Point(1, 1), new Vector2(2, 0), 100, 1f);
            depth3 = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\depth1\2"),
                new Vector2(950, 590), new Point(500, 300), 0, new Point(2, 0), new Point(1, 1), new Vector2(2, 0), 100, 1f);
            //load cloud
            cloud = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\cloud"), new Vector2(150, 100),
                new Point(600, 450), 0, new Point(0, 0), new Point(3, 1), new Vector2(0, 0), 100, 0.3f);
            cloud2 = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\cloud"), new Vector2(200, 0),
                new Point(600, 450), 0, new Point(0, 0), new Point(3, 1), new Vector2(0, 0), 100, 0.2f);

            //load dumpers
            sos = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\sos"), new Vector2(600, 670),
            new Point(500, 500), 0, new Point(0, 0), new Point(4, 1), Vector2.Zero, 150, 0.2f);
            dumpers = new UserControlledDumpers(Game.Content.Load<Texture2D>(@"content\dumper"),
                new Vector2(550, 580), new Point(321, 200), 30, new Point(0, 0), new Point(4, 1), new Vector2(8, 8), 150,0, 0.6f);
            icon = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\dumper"),
                new Vector2(20, 700), new Point(321, 200), 0, new Point(0, 0), new Point(4, 1), new Vector2(0, 0), 200, 0.3f);
            playerdepth1.Add(depth1); playerdepth1.Add(depth2); playerdepth1.Add(depth3);
            SpawnBoss();
            base.LoadContent();
        }
        public void Statup()
        {
            this.dumpers.Reset();
            nuke = 1;
            levelshoot = 1;
            mCurrentHealthDumpers = 100;
            mCurrentHealthEnviron = 100;
            score = 0;
            checkbullet = 0;
            bom = 1;

            fBulletDelayTimer = 0.0f;

            ///////////////////////

            spriteList = new List<AutomatedSprite>();
            itemList = new List<AutomatedSprite>();
            enemyList = new List<AutomatedSprite>();
            bulletList = new List<AutomatedSprite>();
            shootList = new List<AutomatedSprite>();
            bomList = new List<AutomatedSprite>();
            mineList = new List<AutomatedSprite>();
            trashList = new List<AutomatedSprite>();
            playerdepth1 = new List<AutomatedSprite>();
            playerdepth2 = new List<AutomatedSprite>();
            playerdepth3 = new List<AutomatedSprite>();
            rocket_boss_list = new List<AutomatedSprite>();

            explosion = new List<Explosion>();
            BOSS = new List<boss>();
            hand = new List<handofboss>();

            //Spawning variables
            trashSpawnMinMilliseconds = 1500;
            trashSpawnMaxMilliseconds = 2500;

            depth2SpawnMinMilliseconds = 2000; depth3SpawnMinMilliseconds = 10000;
            depth2SpawnMaxMilliseconds = 6000; depth3SpawnMaxMilliseconds = 15000;

            enemySpawnMinMilliseconds = 3500; itemSpawnMinMilliseconds = 7000;
            enemySpawnMaxMilliseconds = 7000; itemSpawnMaxMilliseconds = 12000;
            mineSpawnMinMilliseconds = 10000;
            mineSpawnMaxMilliseconds = 20000;

            trashMinSpeed = 1;
            trashMaxSpeed = 5;

            enemyMinSpeed = 2;
            enemyMaxSpeed = 4;

            nextTrashSpawnTime = 2000;
            nextEnemySpawnTime = 8000;
            nextItemSpawnTime = 15000;
            nextMineSpawnTime = 17000;
            nextRocketBossSpawnTime = 5000;

            nextDepth2SpawnTime = 0;
            nextDepth3SpawnTime = 0;

            nextSpawnTimeChange = 5000;
            timeSinceLastSpawnTimeChange = 0;

            delaygetrealdy = false;
            victory = false;
            gameover = false;
            trum = false;
            powerUpExpiration = 0;
            ///////////////////////////

            defend = Game.Content.Load<Texture2D>(@"content\defend");

            nukeicon = Game.Content.Load<Texture2D>(@"content\nukeicon");

            rocket_button = Game.Content.Load<Texture2D>(@"content\rocket_button");

            iconofbom = Game.Content.Load<Texture2D>(@"content\iconofbom");

            mHealthBar = Game.Content.Load<Texture2D>(@"content\HealthBar"); mHealthBar2 = Game.Content.Load<Texture2D>(@"content\HealthBar2");
            //load font
            font = Game.Content.Load<SpriteFont>(@"content\font");
            //load sun
            mousetaget = new taget(Game.Content.Load<Texture2D>(@"content\taget"), new Vector2(200, 200), new Point(227, 210),
                0, new Point(0, 0), new Point(4, 3), Vector2.Zero, 500, 0.4f);

            //load under
            under = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\under"), new Vector2(0, 693),
                new Point(1366, 75), 0, new Point(0, 0), new Point(1, 1), Vector2.Zero, 1000, 1f);

            //load cloud
            sun = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\sun"), new Vector2(0, 0),
                new Point(600, 400), 0, new Point(0, 0), new Point(3, 1), Vector2.Zero, 400, 0.6f);
            depth1 = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\depth1\2"),
                new Vector2(000, 590), new Point(500, 300), 0, new Point(2, 0), new Point(1, 1), new Vector2(2, 0), 100, 1f);
            depth2 = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\depth1\2"),
                new Vector2(500, 590), new Point(500, 300), 0, new Point(2, 0), new Point(1, 1), new Vector2(2, 0), 100, 1f);
            depth3 = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\depth1\2"),
                new Vector2(950, 590), new Point(500, 300), 0, new Point(2, 0), new Point(1, 1), new Vector2(2, 0), 100, 1f);
            //load cloud
            cloud = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\cloud"), new Vector2(150, 100),
                new Point(600, 450), 0, new Point(0, 0), new Point(3, 1), new Vector2(0, 0), 100, 0.3f);
            cloud2 = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\cloud"), new Vector2(200, 0),
                new Point(600, 450), 0, new Point(0, 0), new Point(3, 1), new Vector2(0, 0), 100, 0.2f);

            //load dumpers
            sos = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\sos"), new Vector2(600, 670),
            new Point(500, 500), 0, new Point(0, 0), new Point(4, 1), Vector2.Zero, 150, 0.2f);
            dumpers = new UserControlledDumpers(Game.Content.Load<Texture2D>(@"content\dumper"),
                new Vector2(550, 580), new Point(321, 200), 30, new Point(0, 0), new Point(4, 1), new Vector2(8, 8), 150, 0, 0.6f);
            icon = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\dumper"),
                new Vector2(20, 700), new Point(321, 200), 0, new Point(0, 0), new Point(4, 1), new Vector2(0, 0), 200, 0.3f);
            playerdepth1.Add(depth1); playerdepth1.Add(depth2); playerdepth1.Add(depth3);
            SpawnBoss();
        }
        public override void Update(GameTime gameTime)
        {
            UpdateSprite(gameTime);         
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fBulletDelayTimer += elapsed;
            if (boss == null && trum)
            {
                first = true;
            }
            if(delaygetrealdy==false)
            {
                sound_getready.Play();
                delaygetrealdy = true;
            }
            nextItemSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            nextRocketBossSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            if ((mCurrentHealthEnviron < 199)&&trum==false)
            {
                nextEnemySpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
                nextTrashSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
                nextMineSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
                nextDepth2SpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
                nextDepth3SpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
                if (nextTrashSpawnTime < 0)
                {
                    SpawnTrash();
                    ResetTrashSpawnTime();
                }
                if (nextEnemySpawnTime < 0)
                {
                    SpawnEnemy();
                    ResetEnemySpawnTime();
                }

                if (nextMineSpawnTime < 0)
                {
                    SpawnMine();
                    ResetMineSpawnTime();
                }
            }
            if (gameTime.TotalGameTime.Milliseconds % 2000 == 0)
            {
                SpawnBullet();
            }
            if (mCurrentHealthEnviron >= 199)
            {                
                trum = true;
            }
            if ( boss!=null && (trum) && nextRocketBossSpawnTime < 0)
            {
                SpawnRocketBoss();
                Random rand = new Random();
                nextRocketBossSpawnTime = rand.Next(2000,5000);
            }
            if (nextItemSpawnTime < 0)
            {
                SpawnItem();
                ResetItemSpawnTime();
            }
            if (nextDepth2SpawnTime < 0)
            {
                SpawnDepth2();
                ResetDepth2SpawnTime();
            }
            if (nextDepth3SpawnTime < 0)
            {
                SpawnDepth3();
                ResetDepth3SpawnTime();
            }
            
            if (gameTime.TotalGameTime.Milliseconds % 1000 == 0)
                SpawnDepth1();
            UpdateSprite(gameTime);
            Shooting(gameTime);
            AdjustSpawnTimes(gameTime);
            CheckPowerUpExpiration(gameTime);
            Nuke(gameTime);            
            base.Update(gameTime);
        }
        protected void UpdateSprite(GameTime gameTime)
        {
            //if (MediaPlayer.State == MediaState.Stopped)
            //    MediaPlayer.Play(backgroundMusic);
            sos.Update(gameTime, Game.Window.ClientBounds);
            dumpers.Update(gameTime, Game.Window.ClientBounds);
            icon.Update(gameTime, Game.Window.ClientBounds);
            sun.Update(gameTime, Game.Window.ClientBounds);
            cloud.Update(gameTime, Game.Window.ClientBounds); 
            cloud2.Update(gameTime, Game.Window.ClientBounds);
            under.Update(gameTime, Game.Window.ClientBounds);
            mousetaget.Update(gameTime, Game.Window.ClientBounds);

            mCurrentHealthDumpers = (int)MathHelper.Clamp(mCurrentHealthDumpers, 0, 100); 
            mCurrentHealthEnviron = (int)MathHelper.Clamp(mCurrentHealthEnviron, 0, 200);

            if (boss!=null && trum == true)
            {
                boss.Update(gameTime, Game.Window.ClientBounds);
                for (int i = 0; i < hand.Count; ++i)
                {
                    handofboss s = hand[i];
                    s.Update(gameTime, Game.Window.ClientBounds);
                }                
            }
            if (boss != null && boss.GetHealth <= 1)
            {
                score += 2000;
                sound_bossexplode.Play();
                victory = true;
                BOSS.Remove(boss); hand.Remove(hand1); hand.Remove(hand2); hand.Remove(eye1);
                boss = null; hand1 = null; hand2 = null; eye1 = null;
                Random rand = new Random();
                SpawnExplosionEnemy(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionEnemy(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionEnemy(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionEnemy(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionEnemy(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionEnemy(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionEnemy(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionEnemy(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionShoot(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionShoot(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionShoot(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionShoot(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionShoot(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionShoot(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionShoot(new Vector2(rand.Next(1366), rand.Next(700)));
                SpawnExplosionShoot(new Vector2(rand.Next(1366), rand.Next(700)));
                
            }

            // Update all non-player sprites
            for (int i = 0; i < spriteList.Count; ++i)
            {
                AutomatedSprite s = spriteList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.collisionRect.Intersects(dumpers.collisionRect))
                {
                    sound_gettrash.Play();
                    if (s.collisionCueName == "trash")
                    {
                        score += 50;
                        checkbullet += 1;
                        mCurrentHealthEnviron += 2;
                        if (checkbullet >= 10)
                        {
                            sound_bomup.Play();
                            bom += 1;
                            checkbullet = 0;                          
                        }
                    }                    
                    spriteList.RemoveAt(i);
                    --i;
                }
                else if(s.collisionRect.Intersects(under.collisionRect))
                {
                    mCurrentHealthEnviron -= 3;
                    score -= 10;
                    sound_trashfall.Play();
                    if (spriteList.Count > 0)
                    {
                        spriteList.RemoveAt(i);
                        --i;
                    }
                    if (s.collisionCueName == "trash")
                    {
                        Vector2 position; 
                        position.X = s.GetPosition.X + (int)(0.3 * 180) - 20;
                        position.Y = s.GetPosition.Y + (int)(0.3 * 240) - 20;
                        trashList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\trashfall"),
                            position, new Point(292, 52), 0, new Point(0, 0),
                            new Point(1, 1), new Vector2(2, 0), 100, "trashfall", 0.3f));
                    }
                }
            }
            // update all item
            for (int i = 0; i < itemList.Count; ++i)
            {
                AutomatedSprite s = itemList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.collisionRect.Intersects(dumpers.collisionRect))
                {
                    if (s.collisionCueName == "health")
                    {
                        sound_healthdumperup.Play();
                        // Collided with health
                        if (gameover==false)
                        {
                            mCurrentHealthDumpers += 25;
                            if (mCurrentHealthDumpers > 100)
                                mCurrentHealthDumpers = 100;
                        }
                    }
                    else if (s.collisionCueName == "health_2")
                    {
                        // Collided with health environ
                        mCurrentHealthEnviron += 15;
                        sound_healthenvironup.Play();
                    }
                    else if (s.collisionCueName == "speedslow")
                    {
                        // Collided with speed slow
                        sound_speedslow.Play();
                        powerUpExpiration = 10000;
                        dumpers.ModifySpeed(0.5f);
                    }
                    if (s.collisionCueName == "speedfast")
                    {
                        // Collided with speed fast
                        powerUpExpiration = 20000;
                        dumpers.ModifySpeed(2f);
                        sound_speedup.Play();
                    }
                    if (s.collisionCueName == "item_rocket")
                    {
                        bom += 5;
                        sound_addrocket.Play();
                    }
                    if (s.collisionCueName == "levelshoot")
                    {
                        levelshoot+=1;
                        sound_shootup.Play();
                        if (levelshoot >= 3)
                            levelshoot = 3;
                    }
                    if (s.collisionCueName == "nuke")
                    {
                        nuke += 1;
                        sound_nukedanger.Play();
                    }
                    if (s.collisionCueName == "defend_plus")
                    {
                        dumpers.addDefend();
                        sound_shielup.Play();
                    }
                    itemList.RemoveAt(i);
                    --i;
                }
                else if (s.collisionRect.Intersects(under.collisionRect))
                {                        
                    itemList.RemoveAt(i);
                        --i;

                }
            }
            // update all enemy
            for (int i = 0; i < enemyList.Count; ++i)
            {
                AutomatedSprite s = enemyList[i];
                s.Update(gameTime, Game.Window.ClientBounds);   
                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {                    
                   enemyList.RemoveAt(i);
                    --i;
                }
                if (s.getProtect <= 0)
                {
                    enemyList.RemoveAt(i);
                    --i;
                    sound_bang.Play();
                    score += 100;
                    SpawnExplosionEnemy(s.GetPosition);
                }
            }
           
            // update bullet from enemy
            for (int i = 0; i < bulletList.Count; ++i)
            {
                AutomatedSprite s = bulletList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.collisionRect.Intersects(dumpers.collisionRect))
                {
                    if (dumpers.getProtect > 0)
                    {
                        dumpers.removeDefend();
                    }
                    else
                    {
                        if (s.collisionCueName == "bullet")
                        {
                            if (gameover == false)
                            {
                                mCurrentHealthDumpers -= 5;
                                sound_hited.Play();
                            }
                        }
                        else
                            if (s.collisionCueName == "bullet_2")
                            {
                                if (gameover == false) mCurrentHealthDumpers -= 10;
                                sound_dumpersdestroy.Play();
                            }
                        score -= 20;
                    }
                        bulletList.RemoveAt(i);
                        --i;
                        SpawnExplosionShoot(s.GetPosition);
                }
                else if (s.collisionRect.Intersects(under.collisionRect))
                {
                    sound_underhit.Play();
                    if (s.collisionCueName == "bullet")
                    {
                        Vector2 position;
                        position.X = s.GetPosition.X;
                        position.Y = s.GetPosition.Y+30;
                        trashList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\crack"),
                            position, new Point(200, 135), 0, new Point(0, 0),
                            new Point(1, 1), new Vector2(2, 0), 100, "crack", 0.3f));
                        SpawnExplosionUnder(s.GetPosition);
                    }
                    if (s.collisionCueName == "bullet_2")
                    {
                        Vector2 position;
                        position.X = s.GetPosition.X;
                        position.Y = s.GetPosition.Y-16;
                        trashList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\fire"),
                            position, new Point(200, 200), 0, new Point(0, 0),
                            new Point(4, 1), new Vector2(2, 0), 200, "fire", 0.3f));
                    }
                        bulletList.RemoveAt(i);
                        --i;
                }                
            }
            //update shoot of dumper
            for (int i = 0; i < shootList.Count; ++i)
            {
                AutomatedSprite s = shootList[i];
                s.Update(gameTime, Game.Window.ClientBounds);

                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    shootList.RemoveAt(i);
                    --i;
                }
                else
                    if (boss != null && trum && s.collisionRect.Intersects(boss.collisionRect))
                    {
                        sound_bullethit.Play();
                        shootList.RemoveAt(i);
                        --i;
                        boss.downHealth(1);
                        SpawnExplosionShoot(s.GetPosition);
                    }
                    else
                    {
                        for (int j = 0; j < enemyList.Count; ++j)
                        {
                            AutomatedSprite x = enemyList[j];
                            if (s.collisionRect.Intersects(x.collisionRect))
                            {
                                shootList.RemoveAt(i);
                                --i;
                                x.destroyProtect();
                                SpawnExplosionShoot(s.GetPosition);
                                sound_bullethit.Play();
                            }

                        }
                        for (int j = 0; j < bulletList.Count; ++j)
                        {
                            AutomatedSprite x = bulletList[j];
                            if (s.collisionRect.Intersects(x.collisionRect) && (x.collisionCueName == "bullet_2"))
                            {
                                score += 10;
                                sound_bulletcollision.Play();
                                bulletList.RemoveAt(j);
                                --j;
                                SpawnExplosionShoot(s.GetPosition);
                            }
                        }
                        for (int j = 0; j < mineList.Count; ++j)
                        {
                            AutomatedSprite x = mineList[j];
                            if (s.collisionRect.Intersects(x.collisionRect))
                            {
                                sound_minehit.Play();
                                shootList.RemoveAt(i);
                                --i;
                                mineList.RemoveAt(j);
                                --j;
                                score += 100;
                                SpawnExplosionMine(x.GetPosition);
                            }

                        }
                        for (int j = 0; j <rocket_boss_list.Count; ++j)
                        {
                            AutomatedSprite x = rocket_boss_list[j];
                            if (s.collisionRect.Intersects(x.collisionRect))
                            {
                                score += 150;
                                shootList.RemoveAt(i);
                                --i;
                                x.destroyProtect();
                                SpawnExplosionShoot(s.GetPosition);
                                sound_bullethit.Play();
                            }
                        }                    
                    }               

            }
            //update bom from dumpers
            for (int i = 0; i < bomList.Count; ++i)
            {
                AutomatedSprite s = bomList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    bomList.RemoveAt(i);
                    --i;
                }
                else
                {
                    for (int j = 0; j < enemyList.Count; ++j)
                    {
                        AutomatedSprite x = enemyList[j];
                        if (s.collisionRect.Intersects(x.collisionRect))
                        {
                            enemyList.RemoveAt(j);
                            --j;
                            score += 100;
                            sound_bang.Play();
                            SpawnExplosionEnemy(x.GetPosition);
                        }

                    }
                    for (int j = 0; j < spriteList.Count; ++j)
                    {
                        AutomatedSprite x = spriteList[j];
                        if (s.collisionRect.Intersects(x.collisionRect) && (x.collisionCueName == "trash"))
                        {
                            spriteList.RemoveAt(j);
                            --j;
                            score += 50;
                            sound_dumpersdestroy.Play();
                            sound_underhit.Play();
                            SpawnExplosionShoot(x.GetPosition);
                        }

                    }
                    for (int j = 0; j < bulletList.Count; ++j)
                    {
                        AutomatedSprite x = bulletList[j];
                        if (s.collisionRect.Intersects(x.collisionRect))
                        {
                            bulletList.RemoveAt(j);
                            --j;
                            if (x.collisionCueName == "bullet_2")
                                score += 10;
                            SpawnExplosionShoot(x.GetPosition);
                        }

                    }
                    for (int j = 0; j < rocket_boss_list.Count; ++j)
                    {
                        AutomatedSprite x = rocket_boss_list[j];
                        if (s.collisionRect.Intersects(x.collisionRect))
                        {
                            rocket_boss_list.RemoveAt(j);
                            --j;
                            score += 150;
                            SpawnExplosionEnemy(s.GetPosition);
                            SpawnExplosionShoot(s.GetPosition);
                            sound_bullethit.Play();
                        }
                    }
                    if (boss != null && trum && s.collisionRect.Intersects(boss.collisionRect))
                    {
                        bomList.RemoveAt(i);
                        --i;
                        boss.downHealth(4);
                        SpawnExplosionShoot(s.GetPosition);
                        SpawnExplosionEnemy(s.GetPosition);
                    }
                }
            }

            //update minelist
            for (int i = 0; i < mineList.Count; ++i)
            {
                AutomatedSprite s = mineList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.collisionRect.Intersects(dumpers.collisionRect))
                {
                    sound_minehit.Play();
                    if (dumpers.getProtect > 0)
                        dumpers.removeDefend();
                    else
                    {
                        mineList.RemoveAt(i);
                        --i;
                        if (gameover == false) mCurrentHealthDumpers -= 12;
                    }
                    SpawnExplosionShoot(s.GetPosition);
                    SpawnExplosionMine(s.GetPosition);
                }
                else if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    mineList.RemoveAt(i);
                    --i;
                }
            }
            //update rocket of boss
            for (int i = 0; i <rocket_boss_list.Count; ++i)
            {
                AutomatedSprite s = rocket_boss_list[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.collisionRect.Intersects(dumpers.collisionRect))
                {
                    sound_minehit.Play();
                    if (dumpers.getProtect > 0)
                        dumpers.removeDefend();
                    else
                    {
                        rocket_boss_list.RemoveAt(i);
                        --i;
                        if (gameover == false) mCurrentHealthDumpers -= 12;
                        SpawnExplosionShoot(new Vector2(s.GetPosition.X+33,s.GetPosition.Y+100));
                        SpawnExplosionMine(new Vector2(s.GetPosition.X + 33, s.GetPosition.Y + 100));
                    }

                }
                else
                    if (s.collisionRect.Intersects(under.collisionRect))
                    {
                        sound_minehit.Play();
                        SpawnExplosionMine(new Vector2(s.GetPosition.X + 33, s.GetPosition.Y + 100));
                        rocket_boss_list.RemoveAt(i);
                        --i;
                    }
                else
                        if (s.getProtect <= 0)
                        {
                            rocket_boss_list.RemoveAt(i);
                            --i;
                            sound_bang.Play();
                            score += 100;
                            SpawnExplosionEnemy(new Vector2(s.GetPosition.X + 33, s.GetPosition.Y + 100));
                        }
            }
            //update trash after fall
            for (int i = 0; i < trashList.Count; ++i)
            {
                AutomatedSprite s = trashList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    trashList.RemoveAt(i);
                    --i;
                }
            }
            for (int i = 0; i < playerdepth1.Count; ++i)
            {
                AutomatedSprite s = playerdepth1[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    playerdepth1.RemoveAt(i);
                    --i;
                }
            }
            for (int i = 0; i < playerdepth2.Count; ++i)
            {
                AutomatedSprite s = playerdepth2[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    playerdepth2.RemoveAt(i);
                    --i;
                }
            }
            for (int i = 0; i < playerdepth3.Count; ++i)
            {
                AutomatedSprite s = playerdepth3[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    playerdepth3.RemoveAt(i);
                    --i;
                }
            }

            for (int i = 0; i < explosion.Count; ++i)
            {
                Explosion s = explosion[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    explosion.RemoveAt(i);
                    --i;
                }
            }
            if (mCurrentHealthDumpers <= 0) 
                gameover = true;
            else 
                if(mCurrentHealthEnviron <= 0)
                    gameover = true;
        }

        protected void Shooting(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();                        
            Random rand = new Random();
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed)
            {
                Vector2 position = dumpers.GetPosition;
                double hypotenuse = Math.Sqrt((mouseStateCurrent.X - position.X) * (mouseStateCurrent.X - position.X) + (position.Y - mouseStateCurrent.Y) * (position.Y - mouseStateCurrent.Y));
                if (fBulletDelayTimer >= 0.1f)
                {
                    switch(levelshoot)
                    {
                        case 1:
                    fBulletDelayTimer = 0.0f;
                    sound_shoot.Play(0.3f, 0f, 0f);
                    shootList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\shoot"),
                            position, new Point(113, 113), 10, new Point(0, 0),
                            new Point(1, 1), new Vector2(((mouseStateCurrent.X - position.X) / (int)hypotenuse) * 8, (-(position.Y - mouseStateCurrent.Y) / (int)hypotenuse) * 8), 100, "shoot", 0.3f));
                    break;
                        case 2:
                            fBulletDelayTimer = 0.0f;
                    sound_shoot2.Play(0.5f, 0f, 0f);
                    shootList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\shoot"),
                            position, new Point(113, 113), 10, new Point(0, 0),
                            new Point(1, 1), new Vector2(((mouseStateCurrent.X - position.X) / (int)hypotenuse) * 8, (-(position.Y - mouseStateCurrent.Y) / (int)hypotenuse) * 8), 100, "shoot", 0.3f));
                    shootList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\shoot_2"),new Vector2(position.X+113,position.Y),
                            new Point(113, 113), 10, new Point(0, 0),
                            new Point(1, 1), new Vector2(((mouseStateCurrent.X - position.X) / (int)hypotenuse) * 8, (-(position.Y - mouseStateCurrent.Y) / (int)hypotenuse) * 8), 100, "shoot", 0.3f));
                    
                    break;
                        case 3:
                            fBulletDelayTimer = 0.0f;
                    sound_shoot3.Play(0.7f, 0f, 0f);
                    shootList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\shoot"),
                            position, new Point(113, 113), 10, new Point(0, 0),
                            new Point(1, 1), new Vector2(((mouseStateCurrent.X - position.X) / (int)hypotenuse) * 8, (-(position.Y - mouseStateCurrent.Y) / (int)hypotenuse) * 8), 100, "shoot", 0.3f));

                    shootList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\shoot_2"),
                            new Vector2(position.X + 113, position.Y), new Point(113, 113), 10, new Point(0, 0),
                            new Point(1, 1), new Vector2(((mouseStateCurrent.X - position.X) / (int)hypotenuse) * 8, (-(position.Y - mouseStateCurrent.Y) / (int)hypotenuse) * 8), 100, "shoot", 0.3f));

                    shootList.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\shoot_3"),
                            new Vector2(position.X + 56, position.Y), new Point(113, 113), 10, new Point(0, 0),
                            new Point(1, 1), new Vector2(((mouseStateCurrent.X - position.X) / (int)hypotenuse) * 8, (-(position.Y - mouseStateCurrent.Y) / (int)hypotenuse) * 8), 100, "shoot", 0.3f));
                    
                    break;
                    }
                }
            }
            if (mouseStateCurrent.RightButton == ButtonState.Pressed && mouseStatePrevious.RightButton == ButtonState.Released && (bom > 0))
            {
                Vector2 position = dumpers.GetPosition;
                sound_rocket.Play();
                bomList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\shoot"),
                        position, new Point(113,113), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(5, -4), 100, "shoot", 0.5f));
                bomList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\rocket_3"),
                        position, new Point(129, 174), 10, new Point(0, 0),
                        new Point(4, 1), new Vector2(3, -4), 100, "rocket_3", 0.7f));
                bomList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\shoot"),
                        position, new Point(113, 113), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(1, -4), 100, "shoot", 0.5f));
                bomList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\rocket_1"),
                        position, new Point(66, 200), 10, new Point(0, 0),
                        new Point(4, 1), new Vector2(0, -4), 100, "rocket_1", 0.7f));
                bomList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\shoot"),
                        position, new Point(113, 113), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(-1, -4), 100, "shoot", 0.5f));
                bomList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\rocket_2"),
                        position, new Point(129, 174), 10, new Point(0, 0),
                        new Point(4, 1), new Vector2(-3, -4), 100, "rocket_2", 0.7f));
                bomList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\shoot"),
                        position, new Point(113, 113), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(-5, -4), 100, "shoot", 0.5f));
                bom -= 1;
            }
            mouseStatePrevious = mouseStateCurrent;
        }

        KeyboardState lastKbs;
        protected void Nuke(GameTime gameTime)
        {
            KeyboardState keystate = Keyboard.GetState();
            if (lastKbs != null)
            {
                if ((lastKbs.IsKeyDown(Keys.F) && keystate.IsKeyUp(Keys.F) && nuke > 0))
                {
                    nukebom.Play();
                    ResetTime();
                    for (int j = 0; j < enemyList.Count; ++j)
                    {
                        AutomatedSprite e = enemyList[j];
                        enemyList.RemoveAt(j);
                        --j;
                        score += 100;
                        SpawnExplosionEnemy(e.GetPosition);
                        SpawnExplosionShoot(e.GetPosition);
                    }
                    for (int j = 0; j < bulletList.Count; ++j)
                    {
                        AutomatedSprite e = bulletList[j];
                        bulletList.RemoveAt(j);
                        --j;
                        SpawnExplosionShoot(e.GetPosition);
                    }
                    for (int j = 0; j < spriteList.Count; ++j)
                    {
                        AutomatedSprite e = spriteList[j];
                        spriteList.RemoveAt(j);
                        --j;
                        score += 50;
                        SpawnExplosionEnemy(e.GetPosition);
                        SpawnExplosionShoot(e.GetPosition);
                    }
                    for (int j = 0; j < mineList.Count; ++j)
                    {
                        AutomatedSprite e = mineList[j];
                        mineList.RemoveAt(j);
                        --j;
                        score += 100;
                        SpawnExplosionEnemy(e.GetPosition);
                        SpawnExplosionShoot(e.GetPosition);
                    }
                    if (boss != null && trum)
                    {
                        boss.downHealth(10);
                        SpawnExplosionEnemy(new Vector2(boss.GetPosition.X + 20, boss.GetPosition.Y + 20));
                        SpawnExplosionEnemy(new Vector2(boss.GetPosition.X + 100, boss.GetPosition.Y + 150));
                        SpawnExplosionEnemy(new Vector2(boss.GetPosition.X + 90, boss.GetPosition.Y + 210));
                        SpawnExplosionEnemy(new Vector2(boss.GetPosition.X + 150, boss.GetPosition.Y));
                        SpawnExplosionEnemy(new Vector2(boss.GetPosition.X + 190, boss.GetPosition.Y + 80));
                        SpawnExplosionEnemy(new Vector2(boss.GetPosition.X + 120, boss.GetPosition.Y));
                        SpawnExplosionEnemy(new Vector2(boss.GetPosition.X + 70, boss.GetPosition.Y + 40));
                        SpawnExplosionEnemy(new Vector2(boss.GetPosition.X + 250, boss.GetPosition.Y + 250));
                        SpawnExplosionShoot(new Vector2(boss.GetPosition.X + 20, boss.GetPosition.Y + 20));
                        SpawnExplosionShoot(new Vector2(boss.GetPosition.X + 100, boss.GetPosition.Y + 150));
                        SpawnExplosionShoot(new Vector2(boss.GetPosition.X + 90, boss.GetPosition.Y + 210));
                        SpawnExplosionShoot(new Vector2(boss.GetPosition.X + 150, boss.GetPosition.Y));
                        SpawnExplosionShoot(new Vector2(boss.GetPosition.X + 190, boss.GetPosition.Y + 80));
                        SpawnExplosionShoot(new Vector2(boss.GetPosition.X + 120, boss.GetPosition.Y));
                        SpawnExplosionShoot(new Vector2(boss.GetPosition.X + 70, boss.GetPosition.Y + 40));
                        SpawnExplosionShoot(new Vector2(boss.GetPosition.X + 250, boss.GetPosition.Y + 250));
                    }

                    nuke -= 1;
                }
            }
            lastKbs = keystate;
        }
        protected void CheckPowerUpExpiration(GameTime gameTime)
        {
            // Is a power-up active?
            if (powerUpExpiration > 0)
            {
                // Decrement power-up timer
                powerUpExpiration -=
                gameTime.ElapsedGameTime.Milliseconds;
                if (powerUpExpiration <= 0)
                {
                    // If power-up timer has expired, end all power-ups
                    powerUpExpiration = 0;
                    dumpers.ResetSpeed();
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //draw paper background
            spriteBatch.Draw(paperTexture, Vector2.Zero, Color.White);

            cloud2.Draw(gameTime, spriteBatch);
            //draw sun
            sun.Draw(gameTime, spriteBatch);
            //draw cloud
            cloud.Draw(gameTime, spriteBatch);

            foreach (AutomatedSprite s in playerdepth3)
                s.Draw(gameTime, spriteBatch);

            foreach (AutomatedSprite s in playerdepth2)
                s.Draw(gameTime, spriteBatch);

            //draw boss
            if (boss != null && trum == true)
            {
                hand1.Draw(gameTime, spriteBatch);
                hand2.Draw(gameTime, spriteBatch);
                boss.Draw(gameTime, spriteBatch);
                eye1.Draw(gameTime, spriteBatch);
            }
            foreach (AutomatedSprite s in rocket_boss_list)
                s.Draw(gameTime, spriteBatch);

            dumpers.Draw(gameTime, spriteBatch);

            foreach (AutomatedSprite s in playerdepth1)
                s.Draw(gameTime, spriteBatch);
            foreach (Explosion s in explosion)
                s.Draw(gameTime, spriteBatch);

            foreach (AutomatedSprite s in mineList)
                s.Draw(gameTime, spriteBatch);

            under.Draw(gameTime, spriteBatch);

            if (bom > 5)
            {
                spriteBatch.Draw(iconofbom, new Rectangle((int)dumpers.GetPosition.X + 100, (int)dumpers.GetPosition.Y, 30, 73), Color.White);
            }
            if (bom > 0)
            {
                spriteBatch.Draw(iconofbom, new Rectangle((int)dumpers.GetPosition.X + 130, (int)dumpers.GetPosition.Y, 30, 73), Color.White);
            }
            //draw defend of dumpers
            if (dumpers.getProtect > 0)
            {
                spriteBatch.Draw(defend, new Rectangle((int)dumpers.GetPosition.X + 40, (int)dumpers.GetPosition.Y + 15, 90, 90), Color.White);
            }

            icon.Draw(gameTime, spriteBatch);
            // Draw all sprites
            foreach (AutomatedSprite s in spriteList)
                s.Draw(gameTime, spriteBatch);
            foreach (AutomatedSprite s in itemList)
                s.Draw(gameTime, spriteBatch);
            foreach (AutomatedSprite s in enemyList)
                s.Draw(gameTime, spriteBatch);
            foreach (AutomatedSprite s in bulletList)
                s.Draw(gameTime, spriteBatch);
            foreach (AutomatedSprite s in shootList)
                s.Draw(gameTime, spriteBatch);
            foreach (AutomatedSprite s in bomList)
                s.Draw(gameTime, spriteBatch);

            foreach (AutomatedSprite s in trashList)
                s.Draw(gameTime, spriteBatch);

            //draw mouse taget
            mousetaget.Draw(gameTime, spriteBatch);
            //draw score
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(820, 710), Color.Red);

            //draw number of bom
            spriteBatch.Draw(rocket_button, new Rectangle(1065, 701, 65, 59), new Rectangle(0, 0, 65, 59), Color.White);
            spriteBatch.DrawString(font, "x" + bom, new Vector2(1125, 710), Color.Violet);
            //draw number of nuke
            spriteBatch.Draw(nukeicon, new Rectangle(1215, 701, 65, 65), new Rectangle(0, 0, 100, 100), Color.White);
            spriteBatch.DrawString(font, "x" + nuke, new Vector2(1278, 710), Color.Violet);

            spriteBatch.DrawString(font, "                safe", new Vector2(500, 690), Color.Violet);
            spriteBatch.DrawString(font, "danger       ", new Vector2(500, 690), Color.Black);
            spriteBatch.DrawString(font, "X Health", new Vector2(150, 690), Color.Blue);


            //draw healthBar of dumpers
            //Draw the health for the health bar
            spriteBatch.Draw(mHealthBar, new Rectangle(130,
                  730, mHealthBar.Width, 34), new Rectangle(0, 41, mHealthBar.Width, 24), Color.White);

            //Draw the current health level based on the current Health
            spriteBatch.Draw(mHealthBar, new Rectangle(130,
                730, (int)(mHealthBar.Width * ((double)mCurrentHealthDumpers / 100)), 33),
                new Rectangle(0, 73, mHealthBar.Width, 22), Color.White);

            //Draw the box around the health bar
            spriteBatch.Draw(mHealthBar, new Rectangle(130,
                  730, mHealthBar.Width, 33), new Rectangle(0, 0, mHealthBar.Width, 33), Color.White);

            //draw healthbar of environment
            //Draw the health for the health bar
            spriteBatch.Draw(mHealthBar2, new Rectangle(500,
                  730, mHealthBar.Width, 33), new Rectangle(0, 41, mHealthBar.Width, 24), Color.White);

            //Draw the current health level based on the current Health
            spriteBatch.Draw(mHealthBar2, new Rectangle(500,
                730, (int)((mHealthBar.Width) / 2 * ((double)mCurrentHealthEnviron / 100)), 33),
                new Rectangle(0, 73, mHealthBar.Width, 22), Color.White);

            //Draw the box around the health bar
            spriteBatch.Draw(mHealthBar2, new Rectangle(500,
                  730, mHealthBar.Width, 33), new Rectangle(0, 0, mHealthBar.Width, 33), Color.White);

            if (mCurrentHealthEnviron < 50)
            {
                sos.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);

        }
        private void ResetTrashSpawnTime()
        {
            Random rand = new Random();
            nextTrashSpawnTime = rand.Next(
            trashSpawnMinMilliseconds,
            trashSpawnMaxMilliseconds);
        }
        private void ResetEnemySpawnTime()
        {
            Random rand = new Random();
            nextEnemySpawnTime = rand.Next(
            enemySpawnMinMilliseconds,
            enemySpawnMaxMilliseconds);
        }
        private void ResetItemSpawnTime()
        {
            Random rand = new Random();
            nextItemSpawnTime = rand.Next(
            itemSpawnMinMilliseconds,
            itemSpawnMaxMilliseconds);
        }
        private void ResetMineSpawnTime()
        {
            Random rand = new Random();
            nextMineSpawnTime = rand.Next(
            mineSpawnMinMilliseconds,
            mineSpawnMaxMilliseconds);
        }
        private void ResetDepth2SpawnTime()
        {
            Random rand = new Random();
            nextDepth2SpawnTime = rand.Next(
            depth2SpawnMinMilliseconds,
            depth2SpawnMaxMilliseconds);

        }
        private void ResetDepth3SpawnTime()
        {
            Random rand = new Random();
            nextDepth3SpawnTime = rand.Next(
            depth3SpawnMinMilliseconds,
            depth3SpawnMaxMilliseconds);

        }
        private void ResetTime()
        {
            trashSpawnMinMilliseconds = 1500;
            trashSpawnMaxMilliseconds = 2500;

             enemySpawnMinMilliseconds = 2000;
             enemySpawnMaxMilliseconds = 8000;

             nextTrashSpawnTime = 2000;
             nextEnemySpawnTime = 4000; 
             nextItemSpawnTime = 8000;
             nextMineSpawnTime = 12000;
        }
        private void SpawnTrash()
        {

            Random rand = new Random();

                   Vector2 position = new Vector2(rand.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth-100),0);

                   Vector2 speed = new Vector2(0,
                        rand.Next(trashMinSpeed,
                        trashMaxSpeed));

                        spriteList.Add(
                        new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\trash"),
                            position, new Point(180, 240), 10, new Point(0, 0),
                            new Point(1, 1), speed, 100, "trash", 0.3f));


        }
        private void SpawnItem()
        {
            Random rand = new Random();
            Vector2 position = new Vector2(rand.Next(0,
                 Game.GraphicsDevice.PresentationParameters.BackBufferWidth - 100), 0);

                switch (rand.Next(8))
                {
                    case 0:
                        itemList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\health"),
                        position, new Point(60, 60), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(0, rand.Next(1, 3)), 150, "health", 1f));
                        break;

                    case 1:
                        itemList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\health_2"),
                        position, new Point(60, 62), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(0, rand.Next(1, 3)), 150, "health_2", 1f));
                        break;
                    case 2:
                        itemList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\speedfast"),
                        position, new Point(75, 75), 10, new Point(0, 0),
                        new Point(6, 1), new Vector2(0, rand.Next(1, 3)), 150, "speedfast", 1.5f));
                        break;
                    case 3:
                        itemList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\speedslow"),
                        position, new Point(30, 32), 10, new Point(0, 0),
                        new Point(10, 1), new Vector2(0, rand.Next(1, 3)), 200, "speedslow", 2f));
                        break;
                    case 4:
                        itemList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\nukeicon"),
                        position, new Point(100, 100), 10, new Point(0, 0),
                        new Point(1, 1), new Vector2(0, rand.Next(1, 3)), 150, "nuke", 0.6f));
                        break;
                    case 5:
                        itemList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\defend_plus"),
                        position, new Point(70, 73), 10, new Point(0, 0),
                        new Point(6, 1), new Vector2(0, rand.Next(1, 3)), 150, "defend_plus", 0.9f));
                        break;
                    case 6:
                        itemList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"content\item_rocket"),
                    position, new Point(157, 69), 20, new Point(0, 0),
                    new Point(4, 1), new Vector2(0, rand.Next(1, 3)), 150, "item_rocket", 0.7f));
                        break;
                    case 7:
                        itemList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"content\levelshoot"),
                    position, new Point(110, 97), 0, new Point(0, 0),
                    new Point(1, 1), new Vector2(0, rand.Next(1, 3)), 150, "levelshoot", 0.5f));
                        break;
            }
        }
        private void SpawnEnemy()
        {
            AutomatedSprite s;
            Vector2 speed = Vector2.Zero;
            Vector2 position = Vector2.Zero;
            Random rand = new Random();
            switch (rand.Next(4))
            {
                case 0: // LEFT to RIGHT mb1
                    position = new Vector2(-250, rand.Next(0,768 - 550));
                    speed = new Vector2(rand.Next(enemyMinSpeed, enemyMaxSpeed), 0);
                    s = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\maybay1_1"),
                        position, new Point(500, 358), 65, new Point(0, 0),new Point(4, 1), speed, 100, "maybay1", 0.4f,10);
                    enemyList.Add(s);
                    sound_enemy1.Play();
                    break;
                case 1: // RIGHT to LEFT mb1
                    position = new Vector2(1366,rand.Next(0,768- 550));
                    speed = new Vector2(-rand.Next(enemyMinSpeed, enemyMaxSpeed), 0);
                    s = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\maybay1"),
                        position, new Point(500, 358), 65, new Point(0, 0),new Point(4, 1), speed, 100, "maybay1", 0.4f,10);
                    enemyList.Add(s);
                    sound_enemy1.Play();
                    break;
                case 2: // LEFT to RIGHT mb2
                    position = new Vector2(-250, rand.Next(0, 768 - 550));
                    speed = new Vector2(rand.Next(enemyMinSpeed, enemyMaxSpeed), 0);
                    s = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\maybay2_2"),
                        position, new Point(500, 202), 50, new Point(0, 0),new Point(4, 1), speed, 100, "maybay2", 0.4f,20);
                    enemyList.Add(s);
                    break;
                case 3: // RIGHT to LEFT mb2
                    position = new Vector2(1366, rand.Next(0, 768 - 550));
                    speed = new Vector2(-rand.Next(enemyMinSpeed, enemyMaxSpeed), 0);
                    s = new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\maybay2"),
                        position, new Point(500, 202), 50, new Point(0, 0),new Point(4, 1), speed, 100, "maybay2", 0.4f,20);
                    enemyList.Add(s);
                    sound_enemy2.Play();
                    break;
            }
        }
        private void SpawnBullet()
        {
            Random rand = new Random();
            
            for (int i = 0; i < enemyList.Count; ++i)
            {                
                AutomatedSprite s = enemyList[i];
                Vector2 position = s.GetPosition;
                switch (rand.Next(3))
                {
                    case 0: case 1:                    
                        bulletList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"content\bullet"),
                    position, new Point(60, 60), 10, new Point(0, 0),
                    new Point(5, 1), new Vector2(0, rand.Next(1,5)), 100, "bullet", 0.6f));
                        sound_enemyshoot.Play();
                    break;

                    case 2:
                    sound_enemyshoot2.Play();
                        bulletList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\bullet_2"),
                        position, new Point(60, 65), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(rand.Next(1, 5), rand.Next(1, 4)), 100, "bullet_2", 0.8f));
                        bulletList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\bullet_2"),
                        position, new Point(60, 65), 10, new Point(0, 0),
                        new Point(5,1), new Vector2(0, rand.Next(1, 5)), 100, "bullet_2", 0.8f));
                        bulletList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\bullet_2"),
                        position, new Point(60, 65), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(-rand.Next(1, 5), rand.Next(1, 4)), 100, "bullet_2", 0.8f));
                        break;
                }
            }
            switch (rand.Next(3))
                {
                case 0: case 1:
                    if (trum && boss != null)
                    {
                        sound_enemyshoot.Play();
                        bulletList.Add(new AutomatedSprite(
                Game.Content.Load<Texture2D>(@"content\bullet"),
                new Vector2(boss.GetPosition.X + 190, boss.GetPosition.Y + 160), new Point(60, 60), 10, new Point(0, 0),
                new Point(5, 1), new Vector2(0, rand.Next(1, 5)), 100, "bullet", 0.6f));
                    }
            break;
                case 2:
                     if (trum && boss != null)
                        {
                            sound_enemyshoot2.Play();
                            bulletList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\bullet_2"),
                        new Vector2(boss.GetPosition.X + 190, boss.GetPosition.Y + 160), new Point(60, 65), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(rand.Next(1, 5), rand.Next(1, 4)), 100, "bullet_2", 0.8f));
                            bulletList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\bullet_2"),
                         new Vector2(boss.GetPosition.X + 190, boss.GetPosition.Y + 160), new Point(60, 65), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(0, rand.Next(1, 5)), 100, "bullet_2", 0.8f));
                            bulletList.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\bullet_2"),
                         new Vector2(boss.GetPosition.X + 190, boss.GetPosition.Y + 160), new Point(60, 65), 10, new Point(0, 0),
                        new Point(5, 1), new Vector2(-rand.Next(1, 5), rand.Next(1, 4)), 100, "bullet_2", 0.8f));
                        }
                        break;

                }
        }

        private void SpawnMine()
        {
            Vector2 speed = new Vector2(2,0);
            Vector2 position = new Vector2(0,625);
            mineList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"content\mine"),
                    position, new Point(120, 108), 30, new Point(0, 0),
                    new Point(4, 1), speed, 200, "mine", 0.6f));

        }
        private void SpawnRocketBoss()
        {
            Random rand = new Random();
            sound_rocket_boss.Play();
            rocket_boss_list.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\rocket_boss"),
                        new Vector2(rand.Next(0,1300),-200), new Point(66, 200), 10, new Point(0, 0), new Point(4, 1), new Vector2(0,4), 150, "rocket_boss", 1f, 8));
            rocket_boss_list.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\rocket_boss"),
                        new Vector2(rand.Next(0, 1300), -200), new Point(66, 200), 10, new Point(0, 0), new Point(4, 1), new Vector2(0, 4), 150, "rocket_boss", 1f, 8));
            rocket_boss_list.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\rocket_boss"),
                                    new Vector2(rand.Next(0, 1300), -200), new Point(66, 200), 10, new Point(0, 0), new Point(4, 1), new Vector2(0, 4), 150, "rocket_boss", 1f, 8));
            rocket_boss_list.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\rocket_boss"),
                                    new Vector2(rand.Next(0, 1300), -200), new Point(66, 200), 10, new Point(0, 0), new Point(4, 1), new Vector2(0, 4), 150, "rocket_boss", 1f, 8));
            rocket_boss_list.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\rocket_boss"),
                        new Vector2(rand.Next(0, 1300), -200), new Point(66, 200), 10, new Point(0, 0), new Point(4, 1), new Vector2(0, 4), 150, "rocket_boss", 1f, 8));
            rocket_boss_list.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\rocket_boss"),
                        new Vector2(rand.Next(0, 1300), -200), new Point(66, 200), 10, new Point(0, 0), new Point(4, 1), new Vector2(0, 4), 150, "rocket_boss", 1f, 8));
            rocket_boss_list.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\rocket_boss"),
                                    new Vector2(rand.Next(0, 1300), -200), new Point(66, 200), 10, new Point(0, 0), new Point(4, 1), new Vector2(0, 4), 150, "rocket_boss", 1f, 8));
            rocket_boss_list.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\rocket_boss"),
                                    new Vector2(rand.Next(0, 1300), -200), new Point(66, 200), 10, new Point(0, 0), new Point(4, 1), new Vector2(0, 4), 150, "rocket_boss", 1f, 8));           
        
        }
        private void SpawnDepth1()
        {
            if (mCurrentHealthEnviron > 170)
            {
                playerdepth1.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth1\1"),
                        new Vector2(-499, 590), new Point(500, 300), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 100, "1", 1f));
            }
            else
                if (mCurrentHealthEnviron < 30)
                {
                    playerdepth1.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth1\3"),
                        new Vector2(-499, 590), new Point(500, 300), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 100, "3", 1f));
                }
                else
                    playerdepth1.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth1\22"),
                        new Vector2(-499, 590), new Point(500, 300), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 100, "3", 1f));


        }

        private void SpawnDepth2()
        {
            Random rand = new Random();
            switch (rand.Next(9))
            {
                case 0:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\tree"),
                        new Vector2(-299, 453), new Point(300, 200), 0, new Point(0, 0),
                        new Point(4, 1), new Vector2(2, 0), 200, "tree", 1.2f));
                    break;
                case 1:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\tree2"),
                        new Vector2(-299, 453), new Point(300, 200), 0, new Point(0, 0),
                        new Point(4, 1), new Vector2(2, 0), 200, "tree", 1.2f));
                    break;
                case 2:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\cay1"),
                        new Vector2(-599, 155), new Point(600, 600), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 200, "tree", 1f));
                    break;
                case 3:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\cay2"),
                        new Vector2(-599, 120), new Point(600, 600), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 200, "tree", 1f));
                    break;
                case 4:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\cay3"),
                        new Vector2(-599, 100), new Point(600, 600), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 200, "tree", 1f));
                    break;
                case 5:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\cay4"),
                        new Vector2(-599, 100), new Point(600, 600), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 200, "tree", 1f));
                    break;
                case 6:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\cay5"),
                        new Vector2(-599, 120), new Point(600, 600), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 200, "tree", 1f));
                    break;
                case 7:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\cay6"),
                        new Vector2(-599, 130), new Point(600, 600), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 200, "tree", 1f));
                    break;
                case 8:
                    playerdepth2.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth2\cay7"),
                        new Vector2(-599, 100), new Point(600, 600), 0, new Point(0, 0),
                        new Point(1, 1), new Vector2(2, 0), 200, "tree", 1f));
                    break;
            }
        }
        private void SpawnDepth3()
        {
            Random rand = new Random();
            switch(rand.Next(10))
            {
                case 0:
                    playerdepth3.Add(new AutomatedSprite(
                                Game.Content.Load<Texture2D>(@"content\depth3\chungcu"),
                                new Vector2(-722, 0), new Point(723, 701), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2(1, 0), 200, " ", 1f));
                    break;
                case 1:
                    playerdepth3.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\depth3\capture"),
                            new Vector2(-1499, 55), new Point(1500, 800), 0, new Point(0, 0),
                            new Point(1, 1), new Vector2(1, 0), 100, " ", 0.8f));
                    break;
                case 2:
                    playerdepth3.Add(new AutomatedSprite(
                            Game.Content.Load<Texture2D>(@"content\depth3\coixaygio"),
                            new Vector2(-480, 225), new Point(800, 800), 0, new Point(0, 0),
                            new Point(1, 1), new Vector2(1, 0), 100, " ", 0.6f));
                    playerdepth3.Add(new AutomatedSprite(
                        Game.Content.Load<Texture2D>(@"content\depth3\FrameCanhquat"),
                        new Vector2(-490, 55), new Point(500, 500), 0, new Point(0, 0),
                        new Point(3, 1), new Vector2(1, 0), 500, " ", 1f));
                    break;
                case 3:
                    playerdepth3.Add(new AutomatedSprite(
                                Game.Content.Load<Texture2D>(@"content\depth3\CotThu"),
                                new Vector2(-749, 390), new Point(1500, 800), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2(1, 0), 200, " ", 0.4f));
                    break;
                case 4:
                    playerdepth3.Add(new AutomatedSprite(
                                Game.Content.Load<Texture2D>(@"content\depth3\FrameNhamay"),
                                new Vector2(-599, 25), new Point(600, 796), 0, new Point(0, 0),
                                new Point(3, 1), new Vector2(1, 0), 400, " ", 0.85f));
                    break;
                case 5:
                    playerdepth3.Add(new AutomatedSprite(
                                Game.Content.Load<Texture2D>(@"content\depth3\Nha1"),
                                new Vector2(-1100, 128), new Point(1500, 800), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2(1, 0), 100, " ", 0.7f));
                    break;
                case 6:
                    playerdepth3.Add(new AutomatedSprite(
                                Game.Content.Load<Texture2D>(@"content\depth3\Nha2"),
                                new Vector2(-900, 300), new Point(1500, 800), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2(1, 0), 100, " ", 0.5f));
                    break;
                case 7:
                    playerdepth3.Add(new AutomatedSprite(
                                Game.Content.Load<Texture2D>(@"content\depth3\Nha3"),
                                new Vector2(-1200, 233), new Point(1500, 651), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2(1, 0), 100, " ", 0.7f));
                    break;
                case 8:
                    playerdepth3.Add(new AutomatedSprite(
                                Game.Content.Load<Texture2D>(@"content\depth3\NhaCay"),
                                new Vector2(-1000, 290), new Point(1500, 800), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2(1, 0), 100, " ", 0.5f));
                    break;
                case 9:
                    playerdepth3.Add(new AutomatedSprite(
                                Game.Content.Load<Texture2D>(@"content\depth3\Quan"),
                                new Vector2(-500, 465), new Point(1500, 800), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2(1, 0), 100, " ", 0.3f));
                    break;

            }
        }
        private void SpawnBoss()
        {
            boss = new boss(Game.Content.Load<Texture2D>(@"content\boss\thantrum"), Game.Content.Load<Texture2D>(@"content\HealthBar"), new Vector2(400, 35),
                new Point(642, 417), 115, new Point(0, 0), new Point(1, 1), new Vector2(1, 1), 200, 0.6f);
            BOSS.Add(boss);
            hand1 = new handofboss(Game.Content.Load<Texture2D>(@"content\boss\TuaTrai"), Vector2.Zero, new Point(200, 103), 30, new Point(0, 0), new Point(4, 1),
                300, 0.5f, boss, 13, 120);
            hand.Add(hand1);
            hand2 = new handofboss(Game.Content.Load<Texture2D>(@"content\boss\TuaPhai"), Vector2.Zero, new Point(200, 103), 30, new Point(0, 0), new Point(4, 1),
                300, 0.5f, boss, 270, 120);
            hand.Add(hand2);
            eye1 = new handofboss(Game.Content.Load<Texture2D>(@"content\boss\MatTrum"), Vector2.Zero, new Point(200, 100), 30, new Point(0, 0), new Point(4, 1),
                500, 0.5f, boss, 143, 100);
            hand.Add(eye1);
        }
        private void SpawnExplosionEnemy(Vector2 position)
        {
            Random rand = new Random();
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosion"), 
                position, new Point(170, 128), new Point(0, 0), new Point(3, 4), new Vector2(2, 0), 120, 0.9f));
            playerdepth1.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\smoke"),
                        position, new Point(40, 40), 10, new Point(0, 0), new Point(1, 1), new Vector2(rand.Next(0, 2), -rand.Next(1, 2)), 100, "smoke", 1.2f));
            playerdepth1.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\smoke"),
                        position, new Point(40, 40), 10, new Point(0, 0), new Point(1, 1), new Vector2(rand.Next(1, 2), -rand.Next(1, 2)), 100, "smoke", 2.5f));
            playerdepth1.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\smoke"),
                        position, new Point(40, 40), 10, new Point(0, 0), new Point(1, 1), new Vector2(-rand.Next(0, 2), -rand.Next(1, 2)), 100, "smoke", 2f));
            playerdepth1.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\smoke"),
                        position, new Point(40, 40), 10, new Point(0, 0), new Point(1, 1), new Vector2(-rand.Next(0, 2), -rand.Next(1, 2)), 100, "smoke", 1.5f));
            playerdepth1.Add(new AutomatedSprite(Game.Content.Load<Texture2D>(@"content\smoke"),
                        position, new Point(40, 40), 10, new Point(0, 0), new Point(1, 1), new Vector2(rand.Next(0, 2), -rand.Next(1, 2)), 100, "smoke", 1f));
        }
        private void SpawnExplosionMine(Vector2 position)
        {
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosion"),
                position, new Point(170, 128), new Point(0, 0), new Point(3, 4), new Vector2(2, 0), 120, 0.9f));
            Random rand = new Random();
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(-rand.Next(1, 3), rand.Next(1, 3)), 50, 0.8f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 3), rand.Next(1, 3)), 80, 1f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(1, 3), rand.Next(0, 3)), 50, 0.9f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(1, 3), rand.Next(0, 3)), 90, 0.5f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 3), -rand.Next(1, 3)), 70, 0.6f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(-rand.Next(0, 3), -rand.Next(1, 3)), 90, 0.7f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 3), -rand.Next(1, 3)), 90, 0.4f));

        }
        private void SpawnExplosionShoot(Vector2 position)
        {
            Random rand = new Random();
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(-rand.Next(1, 3), rand.Next(1, 3)), 50, 0.8f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 3), rand.Next(1, 3)), 80, 1f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(1, 3), rand.Next(0, 3)), 50, 0.9f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(1, 3), rand.Next(0, 3)), 90, 0.5f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 3), -rand.Next(1, 3)), 70, 0.6f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(-rand.Next(0, 3), -rand.Next(1, 3)), 90, 0.7f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 3), -rand.Next(1, 3)), 90, 0.4f));
        }
        private void SpawnExplosionUnder(Vector2 position)
        {
            Random rand = new Random();
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(-rand.Next(0, 2), -rand.Next(0, 2)), 30, 0.4f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 2), -rand.Next(0, 2)), 30, 0.5f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 2), -rand.Next(1, 2)), 30, 0.6f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(-rand.Next(0, 2), -rand.Next(0, 2)), 30, 0.6f));
            explosion.Add(new Explosion(Game.Content.Load<Texture2D>(@"content\explosi2"),
                position, new Point(40, 40), new Point(0, 0), new Point(20, 1), new Vector2(rand.Next(0, 2), -rand.Next(1, 2)), 30, 0.7f));

        }
        public Vector2 GetPlayerPosition()
        {
            return dumpers.GetPosition;
        }

        public int GetScore
        {
            get { return score;}
            set { score = value; }
        }

        protected void AdjustSpawnTimes(GameTime gameTime)
        {
            if (enemySpawnMaxMilliseconds > 2000)
            {
                timeSinceLastSpawnTimeChange += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastSpawnTimeChange > nextSpawnTimeChange)
                {
                    timeSinceLastSpawnTimeChange -= nextSpawnTimeChange;
                    if (enemySpawnMaxMilliseconds > 750)
                    {
                        enemySpawnMaxMilliseconds -= 50;
                        enemySpawnMinMilliseconds -= 50;
                    }
                    else
                    {
                        enemySpawnMaxMilliseconds -= 30;
                        enemySpawnMinMilliseconds -= 30;
                    }
                }
            }
            //adjust item SpawnTimes
            if (trashSpawnMaxMilliseconds > 500)
            {
                timeSinceLastSpawnTimeChange += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastSpawnTimeChange > nextSpawnTimeChange)
                {
                    timeSinceLastSpawnTimeChange -= nextSpawnTimeChange;
                    if (trashSpawnMaxMilliseconds > 1000)
                    {
                        trashSpawnMaxMilliseconds -= 100;
                        trashSpawnMinMilliseconds -= 50;
                    }
                    else
                    {
                        trashSpawnMaxMilliseconds -= 30;
                        trashSpawnMinMilliseconds -= 30;
                    }
                }
            }
        }
    }
}
