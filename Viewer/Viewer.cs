using HackathonWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalaxyConquest
{
    public partial class Viewer : Form
    {
        private List<Frame> _frames;

        private double _scaleFactor;
        private int _topCorrection = 100;
        private int _leftCorrection = 100;
        private int _currentFrame = 0;
        private int _tickCounter = 0;
        private string[] playerNames;
        private bool initialized = false;
        private Graphics g;

        public Viewer(MatchData matchData)//   List<Frame> frames, string player1 = "Player1", string player2 = "player2")
        {
            InitializeComponent();
                      
            _frames = matchData.Frames;
            g = CreateGraphics();
            g.Clear(Color.Black);

            playerNames = matchData.PlayerNames;

            trackBar1.Maximum = _frames.Count - 1;
            initialized = true;
        }

        private void Repaint()
        {
            if (initialized)
            {
                g = CreateGraphics();
                g.Clear(Color.Black);
                DrawMap();
            }
        }

        private void Viewer_Resize(object sender, EventArgs e)
        {
            Repaint();
        }

        private void DrawMap()
        {
            Frame frame = _frames[_currentFrame];
            double dh = this.Height - 200;
            dh /= Settings.Height;
            double dw = this.Width - 200;
            dw /= Settings.Width;
            double delta = Math.Min(dh, dw);
            _scaleFactor = delta;

            _topCorrection =  ((this.Height - 200)- (int)(Settings.Height * _scaleFactor)) / 2 +100;
            _leftCorrection = ((this.Width - 200) - (int)(Settings.Width * _scaleFactor)) / 2 + 100;
            // g.Clear(Color.Black);

            foreach (Frame.FactoryInfo f in frame.Factories)
            {
                DrawFactory(f);
            }

            foreach (Frame.TroopInfo t in frame.Troops)
            {
                DrawTroop(frame, t, true);
            }

            foreach (Frame.BombInfo b in frame.Bombs)
            {
                DrawBomb(frame, b);
            }

            DrawScore();
        }

        private void DrawTroop(Frame frame, Frame.TroopInfo t, bool showNumber)
        {
            Pen p = new Pen(Color.Black, (int)(10.0 * _scaleFactor));
            Frame.FactoryInfo sf = frame.Factories[t.SourceId];
            Frame.FactoryInfo df = frame.Factories[t.DestinationId];

            int tt = t.TotalTurns;
            int rt = t.RemaingTurns;
            int pt = tt - rt;

            tt += 2;
            tt *= 5;

            rt += 1;
            rt *= 5;
            rt -= _tickCounter;
            pt += 1;
            pt *= 5;
            pt += _tickCounter;
            double tx = (sf.X * rt + df.X * pt) / tt;
            double ty = (sf.Y * rt + df.Y * pt) / tt;

            int x = (int)(tx * _scaleFactor) + _leftCorrection;
            int y = (int)(ty * _scaleFactor) + _topCorrection;
            int r = (int)(120 * _scaleFactor);
            g.FillEllipse(p.Brush, x - r, y - r, 2 * r, 2 * r);
            string drawString = t.UnitCount.ToString();
            if (showNumber)
            {
                g.FillRectangle(p.Brush, x, y, (int)(200 * _scaleFactor * drawString.Length), (int)(250 * _scaleFactor));
            }

            rt--;
            pt++;
            tx = (sf.X * rt + df.X * pt) / tt;
            ty = (sf.Y * rt + df.Y * pt) / tt;

            x = (int)(tx * _scaleFactor) + _leftCorrection;
            y = (int)(ty * _scaleFactor) + _topCorrection;
            r = (int)(100 * _scaleFactor);
            p.Color = GetColor(t.OwnerId);

            g.DrawEllipse(p, x - r, y - r, 2 * r, 2 * r);

            if (showNumber)
            {
                SolidBrush brush = new SolidBrush(GetLightColor(t.OwnerId));

                Font drawFond = new Font("Arial", (int)(150 * _scaleFactor));
                g.DrawString(drawString, drawFond, brush, x, y);
            }
        }


        private void DrawBomb(Frame frame, Frame.BombInfo b)
        {
            Frame.FactoryInfo sf = frame.Factories[b.SourceId];
            Frame.FactoryInfo df = frame.Factories[b.DestinationId];

            int tt = b.TotalTurns;
            int rt = b.RemainingTurns;
            int pt = tt - rt;

            tt += 2;
            tt *= 5;

            rt += 1;
            rt *= 5;
            rt -= _tickCounter;
            pt += 1;
            pt *= 5;
            pt += _tickCounter;
            double tx = (sf.X * rt + df.X * pt) / tt;
            double ty = (sf.Y * rt + df.Y * pt) / tt;

            int x = (int)(tx * _scaleFactor) + _leftCorrection;
            int y = (int)(ty * _scaleFactor) + _topCorrection;
            int r = (int)(120 * _scaleFactor);

            Brush br = new SolidBrush(Color.Black);
            g.FillEllipse(br, x - r - 1, y - r - 1, 2 * r + 2, 2 * r + 2);
            rt--;
            pt++;
            tx = (sf.X * rt + df.X * pt) / tt;
            ty = (sf.Y * rt + df.Y * pt) / tt;

            x = (int)(tx * _scaleFactor) + _leftCorrection;
            y = (int)(ty * _scaleFactor) + _topCorrection;
            r = (int)(100 * _scaleFactor);

            Pen p = new Pen(GetColor(b.OwnerId), (int)(50.0 * _scaleFactor));
            g.DrawEllipse(p, x - r, y - r, 2 * r, 2 * r);
            br = new SolidBrush(Color.Yellow);
            g.FillEllipse(br, x - r, y - r, 2 * r, 2 * r);

        }



        private void DrawFactory(Frame.FactoryInfo factory)
        {
            int x = (int)(factory.X * _scaleFactor) + _leftCorrection;
            int y = (int)(factory.Y * _scaleFactor) + _topCorrection;
            int r = (int)(1000.0 * _scaleFactor);
            int pr = (int)(150.0 * _scaleFactor);
            int pd = (int)(350.0 * _scaleFactor);
            int tc = (int)(200.0 * _scaleFactor);

            Pen p = new Pen(Color.Black, (int)(50.0 * _scaleFactor));
            g.FillEllipse(p.Brush, x - r, y - r, 2 * r, 2 * r);
            p.Color = GetColor(factory.OwnerId);
            r = (int)(600.0 * _scaleFactor);
            g.DrawEllipse(p, x - r, y - r, 2 * r, 2 * r);

            p.Width = 1;

            for (int i = 0; i < 3; i++)
            {
                if (factory.CurrentProduction > i)
                {
                    g.FillEllipse(p.Brush, x + (i - 1) * pd - pr, y - pr, 2 * pr, 2 * pr);
                }
                else
                {
                    g.DrawEllipse(p, x + (i - 1) * pd - pr, y - pr, 2 * pr, 2 * pr);
                }
            }
            // print the number of units

            Font drawFond = new Font("Arial", (int)(300 * _scaleFactor));

            SolidBrush brush = new SolidBrush(GetLightColor(factory.OwnerId));
            string drawString = factory.UnitCount.ToString();
            g.DrawString(drawString, drawFond, brush, x - tc * drawString.Length, y - tc * 3);

            drawString = factory.Id.ToString();
            drawFond = new Font("Arial", (int)(150 * _scaleFactor));
            brush = new SolidBrush(Color.Green);
            g.DrawString(drawString, drawFond, brush, x + tc * 2, y + tc * 2);
        }

        private void DrawScore()
        {
            Frame frame = _frames[_currentFrame];
            ScorePlayer1.Text = $"{playerNames[0]}   {frame.Players[0].Score.ToString()}";
            ScorePlayer2.Text = $"{frame.Players[1].Score.ToString()}    {playerNames[1]}";
        }


        private Color GetColor(int? Id)
        {
            return Id.HasValue ? Id.Value == 0 ? Color.Blue : Color.Red : Color.Gray;
        }

        private Color GetLightColor(int? Id)
        {
            return Id.HasValue ? Id.Value == 0 ? Color.LightBlue : Color.Pink : Color.White;
        }

        private void Viewer_Activated(object sender, EventArgs e)
        {
            Repaint();
        }

        private void Viewer_Shown(object sender, EventArgs e)
        {
            Repaint();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _tickCounter++;
            _tickCounter %= 5;
            if (_tickCounter == 0)
            {

                if (_currentFrame < _frames.Count - 1)
                {
                    _currentFrame++;
                    trackBar1.Value = _currentFrame;
                    DrawMap();
                }
                else
                {
                    timer1.Stop();
                    DrawMap();
                    if (_frames[_currentFrame].Winner.HasValue)
                    {
                        PrintWinner();
                    }
                }
            }
            else
            {
                Frame frame = _frames[_currentFrame];
                // draw troopmovement
                foreach (Frame.TroopInfo t in frame.Troops)
                {
                    DrawTroop(frame, t, true);

                }
                foreach (Frame.BombInfo b in frame.Bombs)
                {
                    DrawBomb(frame, b);
                }
            }
        }


        private void HideWinner()
        {
            tableLayoutPanel2.Visible = false;
        }

        private void PrintWinner()
        {

            if (_frames[_currentFrame].Winner.HasValue)
            {
                if (_frames[_currentFrame].Winner.Value == -1)
                {

                    Winner.ForeColor = Color.White;
                    Winner.Text = "DRAW";
                    tableLayoutPanel2.Visible = true;
                }
                else
                {
                    int winner = _frames[_currentFrame].Winner.Value;
                    string drawstring = $"{playerNames[winner]} Wins";
                    Winner.ForeColor = GetLightColor(winner);
                    Winner.Text = drawstring;
                    tableLayoutPanel2.Visible = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                button1.Text = ">";
            }
            else
            {
                timer1.Start();
                button1.Text = "||";
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _currentFrame = trackBar1.Value;
            HideWinner();
            Repaint();


        }

        private void ScorePlayer1_Click(object sender, EventArgs e)
        {

        }

        private void Winner_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = $"{_currentFrame} of {_frames.Count}";
        }
    }
}
