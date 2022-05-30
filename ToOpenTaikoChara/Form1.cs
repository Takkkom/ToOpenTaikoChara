using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToOpenTaikoChara
{
    public partial class Form1 : Form
    {
        private string TaikoJiro_PlayerCharPath = "";
        private string TaikoJiro_PlayerCharBalloonPath = "";

        private string TJAP_PlayerCharNormalPath = "";
        private string TJAP_PlayerCharMaxPath = "";
        private string TJAP_PlayerCharBadPath = "";
        private string TJAP_PlayerCharBalloonPath = "";

        private CharaInfo Chara;

        private CharaPreview.PreviewType CurPreview;

        private List<Bitmap> Cache = new List<Bitmap>();

        private void ChangePreview(CharaPreview.PreviewType num)
        {
            CurPreview = (CharaPreview.PreviewType)num;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = new Bitmap(CharaPreview.Previews[CurPreview].SceneImage);
            UpdateCharaPos();

            if (Chara != null)
            {
                numericUpDown1.Value = CharaPreview.Previews[CurPreview].X[0];
                numericUpDown2.Value = CharaPreview.Previews[CurPreview].Y[0];

                numericUpDown3.Value = CharaPreview.Previews[CurPreview].X[1];
                numericUpDown4.Value = CharaPreview.Previews[CurPreview].Y[1];

                numericUpDown5.Value = (decimal)CharaPreview.Previews[CurPreview].CharaScale;
            }
        }

        private void SetValue(CharaPreview.PreviewType previewType)
        {
            for (int player = 0; player < 2; player++)
            {
                switch (previewType)
                {
                    case CharaPreview.PreviewType.TaikoSelect:
                        Chara.Title_Chara_Entry_X[player] = CharaPreview.Previews[CurPreview].X[player];
                        Chara.Title_Chara_Entry_Y[player] = CharaPreview.Previews[CurPreview].Y[player];
                        break;
                    case CharaPreview.PreviewType.ModeSelect:
                        Chara.Title_Chara_Normal_X[player] = CharaPreview.Previews[CurPreview].X[player];
                        Chara.Title_Chara_Normal_Y[player] = CharaPreview.Previews[CurPreview].Y[player];
                        break;
                    case CharaPreview.PreviewType.SongSelect:
                        Chara.Menu_Chara_X[player] = CharaPreview.Previews[CurPreview].X[player];
                        Chara.Menu_Chara_Y[player] = CharaPreview.Previews[CurPreview].Y[player];
                        break;
                    case CharaPreview.PreviewType.Game:
                        Chara.Game_Chara_X[player] = CharaPreview.Previews[CurPreview].X[player];
                        Chara.Game_Chara_Y[player] = CharaPreview.Previews[CurPreview].Y[player];
                        break;
                    case CharaPreview.PreviewType.Game_Balloon:
                        Chara.Game_Chara_Balloon_X[player] = CharaPreview.Previews[CurPreview].X[player];
                        Chara.Game_Chara_Balloon_Y[player] = CharaPreview.Previews[CurPreview].Y[player];
                        break;
                    case CharaPreview.PreviewType.Result:
                        Chara.Result_Chara_X[player] = CharaPreview.Previews[CurPreview].X[player];
                        Chara.Result_Chara_Y[player] = CharaPreview.Previews[CurPreview].Y[player];
                        break;
                }
            }
        }

        private void UpdateCharaPos()
        {
            double scale = CharaPreview.BaseScale * CharaPreview.Previews[CurPreview].CharaScale;

            for (int i = 0; i < Cache.Count; i++)
            {
                Cache[i]?.Dispose();
            }

            if (Chara != null)
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = new Bitmap(CharaPreview.Previews[CurPreview].SceneImage);
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    Bitmap[] bitmaps = null;

                    int xPrefix = 150;
                    int yPrefix = 0;

                    switch ((CharaPreview.PreviewType)CurPreview)
                    {
                        case CharaPreview.PreviewType.TaikoSelect:
                            bitmaps = Chara.Title_Entry;
                            if (bitmaps != null) xPrefix -= (int)(bitmaps[0].Width / 2 * CharaPreview.Previews[CurPreview].CharaScale);
                            break;
                        case CharaPreview.PreviewType.ModeSelect:
                            bitmaps = Chara.Title_Normal;
                            if (bitmaps != null) xPrefix -= (int)(bitmaps[0].Width / 2 * CharaPreview.Previews[CurPreview].CharaScale);
                            break;
                        case CharaPreview.PreviewType.SongSelect:
                            bitmaps = Chara.Menu_Loop;
                            if (bitmaps != null) xPrefix -= (int)(bitmaps[0].Width / 2 * CharaPreview.Previews[CurPreview].CharaScale);
                            break;
                        case CharaPreview.PreviewType.Game:
                            bitmaps = Chara.Normal;
                            xPrefix = 0;
                            break;
                        case CharaPreview.PreviewType.Game_Balloon:
                            bitmaps = Chara.Balloon_Breaking;
                            xPrefix = 0;
                            break;
                        case CharaPreview.PreviewType.Result:
                            bitmaps = Chara.Result_Clear;
                            xPrefix = -20;
                            if (bitmaps != null)
                            {
                                xPrefix -= (int)(bitmaps[0].Width / 2 * CharaPreview.Previews[CurPreview].CharaScale);
                                yPrefix -= (int)(bitmaps[0].Height / 2 * CharaPreview.Previews[CurPreview].CharaScale);
                            }
                            break;
                    }

                    if (bitmaps != null)
                    {

                        int width = (int)(bitmaps[0].Width * scale);
                        int height = (int)(bitmaps[0].Height * scale);

                        for (int player = 0; player < 2; player++)
                        {

                            var image = new Bitmap(bitmaps[0], new Size(width, height));
                            Cache.Add(image);
                            if (player == 1 && CharaPreview.Previews[CurPreview].Is2PFlipX) image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            g.DrawImage(image,
                            new Rectangle((int)((CharaPreview.Previews[CurPreview].X[player] + xPrefix) * CharaPreview.BaseScale),
                            (int)((CharaPreview.Previews[CurPreview].Y[player] + yPrefix) * CharaPreview.BaseScale),
                            width, height),

                            new Rectangle(0, 0, width, height),
                            GraphicsUnit.Pixel);
                        }

                        numericUpDown1.Minimum = -bitmaps[0].Width;
                        numericUpDown1.Maximum = 1280 + bitmaps[0].Width;

                        numericUpDown2.Minimum = -bitmaps[0].Height;
                        numericUpDown2.Maximum = 720 + bitmaps[0].Height;

                        numericUpDown3.Minimum = -bitmaps[0].Width;
                        numericUpDown3.Maximum = 1280 + bitmaps[0].Width;

                        numericUpDown4.Minimum = -bitmaps[0].Height;
                        numericUpDown4.Maximum = 720 + bitmaps[0].Height;
                    }
                }

                SetValue(CurPreview);
            }

        }

        private void LoadTaikoJiroChara()
        {
            if (TaikoJiro_PlayerCharPath == "" || TaikoJiro_PlayerCharBalloonPath == "") return;

            Chara?.Dispose();
            Chara = new TaikoJiroChara(TaikoJiro_PlayerCharPath, TaikoJiro_PlayerCharBalloonPath);

            for (CharaPreview.PreviewType i = 0; i < CharaPreview.PreviewType.Result; i++)
            {
                CharaPreview.Previews[i].ResetValue();

                SetValue(i);
            }
            ChangePreview(CurPreview);
        }

        private void LoadTJAPChara()
        {
            if (TJAP_PlayerCharNormalPath == "" || TJAP_PlayerCharMaxPath == "" || TJAP_PlayerCharBadPath == "" || TJAP_PlayerCharBalloonPath == "") return;

            Chara?.Dispose();
            Chara = new TJAPChara(TJAP_PlayerCharNormalPath, TJAP_PlayerCharMaxPath, TJAP_PlayerCharBadPath, TJAP_PlayerCharBalloonPath);

            for (CharaPreview.PreviewType i = 0; i < CharaPreview.PreviewType.Result; i++)
            {
                CharaPreview.Previews[i].ResetValue();
            }
            ChangePreview(CurPreview);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(numericUpDown1);
            Controls.Add(numericUpDown2);

            Controls.Add(pictureBox1);

            Controls.Add(textBox2);
            Controls.Add(textBox1);

            for (CharaPreview.PreviewType i = 0; i < CharaPreview.PreviewType.Result + 1; i++)
            {
                CharaPreview.Previews[i] = new CharaPreview((CharaPreview.PreviewType)i);
            }
            ChangePreview(0);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)//Jiro playerchar_p
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TaikoJiro_PlayerCharPath = openFileDialog1.FileName;
                textBox1.Text = TaikoJiro_PlayerCharPath;
                LoadTaikoJiroChara();
            }
        }

        private void button2_Click(object sender, EventArgs e)//playercharballoon_p
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TaikoJiro_PlayerCharBalloonPath = openFileDialog1.FileName;
                textBox2.Text = TaikoJiro_PlayerCharBalloonPath;
                LoadTaikoJiroChara();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Chara != null)
            {
                //どっちも選択したなら

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Select Export Folder";
                folderBrowserDialog.SelectedPath = @"C:";
                folderBrowserDialog.ShowNewFolderButton = true;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    var selectDir = folderBrowserDialog.SelectedPath;
                    Chara.ExportForOpenTaiko(selectDir);
                }

                folderBrowserDialog.Dispose();
            }
            else
            {
                //どちらかが未選択なら
                MessageBox.Show("ファイルを選択してください");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CurPreview <= CharaPreview.PreviewType.TaikoSelect) return;
            ChangePreview(CurPreview - 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (CurPreview >= CharaPreview.PreviewType.Result) return;
            ChangePreview(CurPreview + 1);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Chara != null)
            {
                CharaPreview.Previews[CurPreview].X[0] = (int)numericUpDown1.Value;
                UpdateCharaPos();
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (Chara != null)
            {
                CharaPreview.Previews[CurPreview].Y[0] = (int)numericUpDown2.Value;
                UpdateCharaPos();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (Chara != null)
            {
                CharaPreview.Previews[CurPreview].X[1] = (int)numericUpDown3.Value;
                UpdateCharaPos();
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (Chara != null)
            {
                CharaPreview.Previews[CurPreview].Y[1] = (int)numericUpDown4.Value;
                UpdateCharaPos();
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (Chara != null)
            {
                CharaPreview.Previews[CurPreview].CharaScale = (double)numericUpDown5.Value;
                UpdateCharaPos();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TJAP_PlayerCharNormalPath = openFileDialog1.FileName;
                textBox3.Text = TJAP_PlayerCharNormalPath;
                LoadTJAPChara();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TJAP_PlayerCharMaxPath = openFileDialog1.FileName;
                textBox4.Text = TJAP_PlayerCharMaxPath;
                LoadTJAPChara();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TJAP_PlayerCharBadPath = openFileDialog1.FileName;
                textBox5.Text = TJAP_PlayerCharBadPath;
                LoadTJAPChara();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                TJAP_PlayerCharBalloonPath = openFileDialog1.FileName;
                textBox6.Text = TJAP_PlayerCharBalloonPath;
                LoadTJAPChara();
            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            CharaInfo.JumpFrame = (int)numericUpDown6.Value;
        }
    }
}
