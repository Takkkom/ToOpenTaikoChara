using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToOpenTaikoChara
{
    class CharaPreview : IDisposable
    {
        public enum PreviewType
        {
            TaikoSelect,
            ModeSelect,
            SongSelect,
            Game,
            Game_Balloon,
            Result
        }

        public static Dictionary<PreviewType, CharaPreview> Previews = new Dictionary<PreviewType, CharaPreview>();

        public const double BaseScale = 0.5;
        public double CharaScale = 1.0;

        public Bitmap SceneImage;

        public int[] X = new int[2], Y = new int[2];

        public bool Is2PFlipX;

        public CharaPreview(PreviewType name)
        {
            SceneImage = new Bitmap($@"Assets\Scenes\{name}.png");
            SceneImage = new Bitmap(SceneImage, new Size((int)(SceneImage.Width * BaseScale), (int)(SceneImage.Height * BaseScale)));

            switch (name)
            {
                case PreviewType.TaikoSelect:
                    CharaScale = 1.3;
                    X[0] = 485;
                    Y[0] = 140;
                    X[1] = 485;
                    Y[1] = 140;
                    break;
                case PreviewType.ModeSelect:
                    CharaScale = 1.3;
                    X[0] = 0;
                    Y[0] = 341;
                    X[1] = 981;
                    Y[1] = 341;
                    Is2PFlipX = true;
                    break;
                case PreviewType.SongSelect:
                    CharaScale = 1.3;
                    X[0] = 0;
                    Y[0] = 330;
                    X[1] = 981;
                    Y[1] = 330;
                    Is2PFlipX = true;
                    break;
                case PreviewType.Game:
                    CharaScale = 1;
                    X[0] = 0;
                    Y[0] = 0;
                    X[1] = 0;
                    Y[1] = 537;
                    break;
                case PreviewType.Game_Balloon:
                    CharaScale = 1;
                    X[0] = 240;
                    Y[0] = 0;
                    X[1] = 240;
                    Y[1] = 297;
                    break;
                case PreviewType.Result:
                    CharaScale = 0.8;
                    X[0] = 202;
                    Y[0] = 532;
                    X[1] = 1138;
                    Y[1] = 532;
                    Is2PFlipX = true;
                    break;
            }
        }
        public void Dispose()
        {
            SceneImage.Dispose();
        }
    }
}
