using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToOpenTaikoChara
{
    class TaikoJiroChara : CharaInfo
    {
        private Bitmap[][] SplitImage(string filePath, int xcount, int ycount)
        {
            if (!System.IO.File.Exists(filePath)) return null;

            Bitmap[][] result = new Bitmap[xcount][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Bitmap[ycount];
            }

            using (Bitmap original = new Bitmap(filePath))
            {
                for (int i1 = 0; i1 < xcount; i1++)
                {
                    for (int i2 = 0; i2 < ycount; i2++)
                    {
                        int width = original.Width / xcount;
                        int height = original.Height / ycount;
                        Bitmap bitmap = new Bitmap(width, (int)(height * 1.2));
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.DrawImage(original, new Rectangle(0, (int)(height * 1.2) - height, width, height), new Rectangle(width * i1, height * i2, width, height), GraphicsUnit.Pixel);
                            result[i1][i2] = bitmap;
                        }
                    }
                }
            }
            
            return result;
        }

        private Bitmap[] GenJumpArray(Bitmap original)
        {
            Bitmap[] result = new Bitmap[JumpFrame];

            for (int i = 0; i < result.Length; i++)
            {
                int width = original.Width;
                int height = original.Height;
                Bitmap bitmap = new Bitmap(width, height);

                int y = -(int)(Math.Sin(((double)i / result.Length) * Math.PI) * (height / 6.0));

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawImage(original, new Rectangle(0, y, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
                    result[i] = bitmap;
                }
            }

            return result;
        }

        public TaikoJiroChara(string charaPath, string charaBaloonPath)
        {
            var normals = SplitImage(charaPath, 4, 5);


            Title_Entry = GenJumpArray(normals[2][0]);
            Title_Normal = new Bitmap[] { normals[0][0], normals[1][0] };

            Normal = new Bitmap[] { normals[0][0], normals[1][0] };
            Clear = new Bitmap[] { normals[0][0], normals[1][0] };
            Clear_Max = new Bitmap[] { normals[0][3], normals[1][3] };
            GoGo = new Bitmap[] {
                normals[0][1],
                normals[1][1],
                normals[2][1],
                normals[3][1] };
            GoGo_Max = new Bitmap[] {
                normals[0][4],
                normals[1][4],
                normals[2][4],
                normals[3][4] };

            Combo10 = GenJumpArray(normals[2][0]);
            Combo10_Max = GenJumpArray(normals[2][3]);

            var balloons = SplitImage(charaBaloonPath, 3, 1);
            Balloon_Breaking = new Bitmap[] { balloons[0][0], balloons[1][0] };
            Balloon_Broke = new Bitmap[] { balloons[2][0] };
            Balloon_Miss = new Bitmap[] { balloons[0][0] };


            Menu_Loop = new Bitmap[] { normals[0][0], normals[1][0] };
            Menu_Select = GenJumpArray(normals[2][0]);
            Menu_Start = GenJumpArray(normals[2][0]);


            Result_Normal = new Bitmap[] { normals[0][0], normals[1][0] };
            Result_Clear = new Bitmap[] { normals[0][3], normals[1][3] };
            Result_Failed = new Bitmap[] { normals[0][2], normals[1][2] };
            Result_Failed_In = new Bitmap[] { normals[0][2], normals[1][2] };


            Game_Chara_Motion_Normal.Add(0);
            Game_Chara_Motion_Normal.Add(1);

            Game_Chara_Motion_Clear.Add(0);
            Game_Chara_Motion_Clear.Add(1);

            Game_Chara_Motion_GoGo.Add(0);
            Game_Chara_Motion_GoGo.Add(0);
            Game_Chara_Motion_GoGo.Add(0);
            Game_Chara_Motion_GoGo.Add(0);
            Game_Chara_Motion_GoGo.Add(0);
            Game_Chara_Motion_GoGo.Add(0);
            Game_Chara_Motion_GoGo.Add(1);
            Game_Chara_Motion_GoGo.Add(2);
            Game_Chara_Motion_GoGo.Add(3);
            Game_Chara_Motion_GoGo.Add(3);
            Game_Chara_Motion_GoGo.Add(3);
            Game_Chara_Motion_GoGo.Add(3);
            Game_Chara_Motion_GoGo.Add(3);
            Game_Chara_Motion_GoGo.Add(3);
            Game_Chara_Motion_GoGo.Add(2);
            Game_Chara_Motion_GoGo.Add(1);

            Game_Chara_Beat_Normal = 1;
            Game_Chara_Beat_Clear = 1;
            Game_Chara_Beat_GoGo = 2;
        }
    }
}
