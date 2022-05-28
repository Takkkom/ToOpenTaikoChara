using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToOpenTaikoChara
{
    class CharaInfo : IDisposable
    {
        public static int JumpFrame = 20;

        public Bitmap[] Normal;
        public Bitmap[] Clear;
        public Bitmap[] Clear_Max;
        public Bitmap[] GoGo;
        public Bitmap[] GoGo_Max;

        public Bitmap[] Combo10;
        public Bitmap[] Combo10_Max;
        public Bitmap[] Clearin;
        public Bitmap[] Soulin;
        public Bitmap[] GoGoStart;
        public Bitmap[] GoGoStart_Max;

        public Bitmap[] Balloon_Breaking;
        public Bitmap[] Balloon_Broke;
        public Bitmap[] Balloon_Miss;


        public Bitmap[] Title_Entry;
        public Bitmap[] Title_Normal;


        public Bitmap[] Menu_Loop;
        public Bitmap[] Menu_Start;
        public Bitmap[] Menu_Select;


        public Bitmap[] Result_Normal;
        public Bitmap[] Result_Clear;
        public Bitmap[] Result_Failed;
        public Bitmap[] Result_Failed_In;

        public int[] Game_Chara_X = new int[2];
        public int[] Game_Chara_Y = new int[2];

        public int[] Game_Chara_Balloon_X = new int[2];
        public int[] Game_Chara_Balloon_Y = new int[2];

        protected List<int> Game_Chara_Motion_Normal = new List<int>();
        protected List<int> Game_Chara_Motion_Clear = new List<int>();
        protected List<int> Game_Chara_Motion_GoGo = new List<int>();
        protected int Game_Chara_Beat_Normal = 1;
        protected int Game_Chara_Beat_Clear = 1;
        protected int Game_Chara_Beat_GoGo = 2;

        private int DispoeArrayBitmap(Bitmap[] bitmaps)
        {
            if (bitmaps == null) return -1; 
            for (int i = 0; i < bitmaps.Length; i++)
            {
                bitmaps[i]?.Dispose();
            }
            return 0;
        }

        private string ToArrayText(int[] array)
        {
            string result = "";

            for (int i = 0; i < array.Length; i++)
            {
                result += array[i] + (i == array.Length - 1 ? "" : ",");
            }

            return result;
        }

        private string ToArrayText(List<int> array)
        {
            string result = "";

            for (int i = 0; i < array.Count; i++)
            {
                result += array[i] + (i == array.Count - 1 ? "" : ",");
            }

            return result;
        }

        private int ExportArrayBitmap(Bitmap[] bitmaps, string dirName, double scale)
        {
            if (bitmaps == null) return -1;
            System.IO.Directory.CreateDirectory(dirName);
            for (int i = 0; i < bitmaps.Length; i++)
            {
                if (bitmaps[i] != null)
                {
                    var newbmp = new Bitmap(bitmaps[i], new Size((int)(bitmaps[i].Width * scale), (int)(bitmaps[i].Height * scale)));
                    newbmp.Save($@"{dirName}\{i}.png");
                    newbmp.Dispose();
                }
            }

            return 0;
        }

        public void ExportForOpenTaiko(string dirPath)
        {
            var taikoScale = CharaPreview.Previews[CharaPreview.PreviewType.TaikoSelect].CharaScale;
            var modeScale = CharaPreview.Previews[CharaPreview.PreviewType.ModeSelect].CharaScale;
            var songScale = CharaPreview.Previews[CharaPreview.PreviewType.SongSelect].CharaScale;
            var gameScale = CharaPreview.Previews[CharaPreview.PreviewType.Game].CharaScale;
            var resultScale = CharaPreview.Previews[CharaPreview.PreviewType.Result].CharaScale;
            ExportArrayBitmap(Normal, $@"{dirPath}\Normal", gameScale);
            ExportArrayBitmap(Clear, $@"{dirPath}\Clear", gameScale);
            ExportArrayBitmap(Clear_Max, $@"{dirPath}\Clear_Max", gameScale);
            ExportArrayBitmap(GoGo, $@"{dirPath}\GoGo", gameScale);
            ExportArrayBitmap(GoGo_Max, $@"{dirPath}\GoGo_Max", gameScale);

            ExportArrayBitmap(Combo10, $@"{dirPath}\10combo", gameScale);
            ExportArrayBitmap(Combo10_Max, $@"{dirPath}\10combo_Max", gameScale);
            ExportArrayBitmap(Clearin, $@"{dirPath}\Clearin", gameScale);
            ExportArrayBitmap(Soulin, $@"{dirPath}\Soulin", gameScale);
            ExportArrayBitmap(GoGoStart, $@"{dirPath}\GoGoStart", gameScale);
            ExportArrayBitmap(GoGoStart_Max, $@"{dirPath}\GoGoStart_Max", gameScale);

            ExportArrayBitmap(Balloon_Breaking, $@"{dirPath}\Balloon_Breaking", gameScale);
            ExportArrayBitmap(Balloon_Broke, $@"{dirPath}\Balloon_Broke", gameScale);
            ExportArrayBitmap(Balloon_Miss, $@"{dirPath}\Balloon_Miss", gameScale);


            ExportArrayBitmap(Title_Entry, $@"{dirPath}\Title_Entry", taikoScale);
            ExportArrayBitmap(Title_Normal, $@"{dirPath}\Title_Normal", modeScale);


            ExportArrayBitmap(Menu_Loop, $@"{dirPath}\Menu_Loop", songScale);
            ExportArrayBitmap(Menu_Start, $@"{dirPath}\Menu_Start", songScale);
            ExportArrayBitmap(Menu_Select, $@"{dirPath}\Menu_Select", songScale);


            ExportArrayBitmap(Result_Normal, $@"{dirPath}\Result_Normal", resultScale);
            ExportArrayBitmap(Result_Clear, $@"{dirPath}\Result_Clear", resultScale);
            ExportArrayBitmap(Result_Failed, $@"{dirPath}\Result_Failed", resultScale);
            ExportArrayBitmap(Result_Failed_In, $@"{dirPath}\Result_Failed_In", resultScale);


            using (System.IO.StreamWriter stream = new System.IO.StreamWriter($@"{dirPath}\CharaConfig.txt"))
            {
                stream.WriteLine($"Game_Chara_X={ToArrayText(Game_Chara_X)}");
                stream.WriteLine($"Game_Chara_Y={ToArrayText(Game_Chara_Y)}");
                stream.WriteLine($"Game_Chara_Balloon_X={ToArrayText(Game_Chara_Balloon_X)}");
                stream.WriteLine($"Game_Chara_Balloon_Y={ToArrayText(Game_Chara_Balloon_Y)}");
                stream.WriteLine($"Game_Chara_Motion_Normal={ToArrayText(Game_Chara_Motion_Normal)}");
                stream.WriteLine($"Game_Chara_Motion_Clear={ToArrayText(Game_Chara_Motion_Clear)}");
                stream.WriteLine($"Game_Chara_Motion_GoGo={ToArrayText(Game_Chara_Motion_GoGo)}");
                stream.WriteLine($"Game_Chara_Beat_Normal={Game_Chara_Beat_Normal}");
                stream.WriteLine($"Game_Chara_Beat_Clear={Game_Chara_Beat_Clear}");
                stream.WriteLine($"Game_Chara_Beat_GoGo={Game_Chara_Beat_GoGo}");
            }
        }

        public void Dispose()
        {
            DispoeArrayBitmap(Normal);
            DispoeArrayBitmap(Clear);
            DispoeArrayBitmap(Clear_Max);
            DispoeArrayBitmap(GoGo);
            DispoeArrayBitmap(GoGo_Max);

            DispoeArrayBitmap(Combo10);
            DispoeArrayBitmap(Combo10_Max);
            DispoeArrayBitmap(Clearin);
            DispoeArrayBitmap(Soulin);
            DispoeArrayBitmap(GoGoStart);
            DispoeArrayBitmap(GoGoStart_Max);

            DispoeArrayBitmap(Balloon_Breaking);
            DispoeArrayBitmap(Balloon_Broke);
            DispoeArrayBitmap(Balloon_Miss);


            DispoeArrayBitmap(Title_Entry);
            DispoeArrayBitmap(Title_Normal);


            DispoeArrayBitmap(Menu_Loop);
            DispoeArrayBitmap(Menu_Start);
            DispoeArrayBitmap(Menu_Select);


            DispoeArrayBitmap(Result_Normal);
            DispoeArrayBitmap(Result_Clear);
            DispoeArrayBitmap(Result_Failed);
            DispoeArrayBitmap(Result_Failed_In);
        }
    }
}
