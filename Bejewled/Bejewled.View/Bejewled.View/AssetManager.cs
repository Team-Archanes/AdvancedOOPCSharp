using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Bejewled.View
{
    using System.Threading;

    public class AssetManager
    {
        private readonly ContentManager contentManager;

        public AssetManager(ContentManager content)
        {
            this.contentManager = content;
        }

        public ContentManager Content
        {
            get
            {
                return this.contentManager;
            }
        }

        public Texture2D GetSprite(string assetName)
        {
            if (assetName == "") return null;
            return this.contentManager.Load<Texture2D>(assetName);
        }

        public void PlaySound(string assetName)
        {
            var snd = this.contentManager.Load<SoundEffect>(assetName);
            snd.Play();
        }

        public void PlayMusic(string assetName, bool repeat = true)
        {
            MediaPlayer.IsRepeating = repeat;

            MediaPlayer.Play(this.contentManager.Load<Song>(assetName));
        }

        public void ChangeSoundState()
        {
            if (MediaPlayer.IsMuted)
            {
                MediaPlayer.Resume();
                MediaPlayer.IsMuted = false;
                Thread.Sleep(500);
            }
            else
            {
                MediaPlayer.IsMuted = true;
                MediaPlayer.Pause();
                Thread.Sleep(500);
            }
        }

        public bool IsMuted()
        {
            return MediaPlayer.IsMuted;
        }
    }
}