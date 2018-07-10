using System;
using System.Drawing ;
using System.Windows.Forms;

class JueDeTuquin : Form
{
    const int nRows = 4;
    const int nCols = 4;
    Size sizeTie;
    JueDeTaquinTile[,] aTile = new JueDeTaquinTile[nRows, nCols];
    Random rand;
    Point ptBlank;
    int iTimerCountDown;

    //public static void Main()
    //{
    //    Application.Run(new JueDeTuquin());
    //}

    public JueDeTuquin()
    {
        Text = "JueDeTequin";
        FormBorderStyle = FormBorderStyle.Fixed3D;

    }
    protected override void OnLoad(EventArgs e)
    {
        Graphics graf = CreateGraphics();

        sizeTie = new Size((int)(2 * graf.DpiX / 3), (int)(2 * graf.DpiY / 3));
        ClientSize = new Size(nCols  * sizeTie.Width,nRows * sizeTie.Height);
        graf.Dispose();
        //create the tiles.
        for (int iRow = 0; iRow < nRows; iRow++)
            for (int iCol = 0; iCol < nCols; iCol++)
            {
                int iNum = iRow * nCols + iCol + 1;
                if (iNum == nRows * nCols)
                    continue;

                JueDeTaquinTile tile = new JueDeTaquinTile(iNum);
                tile.Parent = this;
                tile.Location = new Point(iCol * sizeTie.Width, iRow * sizeTie.Height);
                tile.Size = sizeTie;
                aTile[iRow, iCol] = tile;
            }
        ptBlank = new Point(nCols - 1, nRows - 1);

        Randomize();
    }
    protected void Randomize()
    {
        rand = new Random();
        iTimerCountDown = 64 * nRows * nCols;
        Timer timer = new Timer();
        timer.Tick += new EventHandler(TimerOnTik);
        timer.Interval = 1;
        timer.Enabled = true;
        

    }
    void TimerOnTik(object obj, EventArgs ea)
    {
        int x = ptBlank.X;
        int y = ptBlank.Y;

        switch (rand.Next(4))
        {
            case 0: x++; break;
            case 1: x--; break;
            case 2: y++; break;
            case 3: y--; break;

        }
        if (x >= 0 && x < nCols && y >= 0 && y < nRows)
            MoveTile(x, y);

        if (--iTimerCountDown == 0)
        {
            ((Timer)obj).Stop();
            ((Timer)obj).Tick -= new EventHandler(TimerOnTik);
        }
    }
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Left && ptBlank.X < nCols - 1)

            MoveTile(ptBlank.X + 1, ptBlank.Y);
        else if (e.KeyCode == Keys.Right && ptBlank.X > 0)
            MoveTile(ptBlank.X - 1, ptBlank.Y);
        else if (e.KeyCode == Keys.Up && ptBlank.Y < nRows - 1)
            MoveTile(ptBlank.X, ptBlank.Y + 1);
        else if (e.KeyCode == Keys.Down && ptBlank.Y > 0)
            MoveTile(ptBlank.X, ptBlank.Y - 1);

        e.Handled = true;
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        int x = e.X / sizeTie.Width;
        int y = e.Y / sizeTie.Height;

        if (x == ptBlank.X)
        {
            if (y < ptBlank.Y)
                for (int y2 = ptBlank.Y - 1; y2 >= y; y2--)
                    MoveTile(x, y2);
            else if (y > ptBlank.Y)
                for (int y2 = ptBlank.Y + 1; y2 <= y; y2++)
                    MoveTile(x, y2);
        }
        else if (y == ptBlank.Y)
        {
            if (x < ptBlank.X)
                for (int x2 = ptBlank.X - 1; x2 >= x; x2--)
                    MoveTile(x2, y);
            else if (x > ptBlank.X)
                for (int x2 = ptBlank.X + 1; x2 <= x; x2++)
                    MoveTile(x2, y);
        }

    }
    void MoveTile(int x, int y)
    {
        aTile[y, x].Location = new Point(ptBlank.X * sizeTie.Width, ptBlank.Y * sizeTie.Height);
        aTile[ptBlank.Y, ptBlank.X] = aTile[y, x];
        aTile[y, x] = null;
        ptBlank = new Point(x, y);

    }
}