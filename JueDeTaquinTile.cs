using System;
using System.Windows.Forms;
using System.Drawing;

class JueDeTaquinTile : UserControl
{
    int iNum;

    public JueDeTaquinTile(int iNum)
    {
        this.iNum = iNum;
        Enabled = false;

    }
//
    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics graf = e.Graphics;
        graf.Clear(SystemColors.Control);

        int cx = Size.Width;
        int cy = Size.Height;
        int wx = SystemInformation.FrameBorderSize.Width;
        int wy = SystemInformation.FrameBorderSize.Height;

        graf.FillPolygon(SystemBrushes.ControlLightLight,
            new Point[] { new Point(0, cy), new Point(0, 0), 
            new Point(cx, 0), new Point(cx - wx, wy), 
            new Point(wx, wy), new Point(wx, cy - wy) });

        graf.FillPolygon(SystemBrushes.ControlDark ,
            new Point[] { new Point(cx, 0), new Point(cx, cy), 
            new Point(0, cy), new Point(wx , cy- wy), 
            new Point(cx-wx,cy-wy), new Point(cx-wx, wy) });

        Font font = new Font("Times New Roman", 24);
        StringFormat strf = new StringFormat();
        strf.Alignment = strf.LineAlignment = StringAlignment.Center;

        graf.DrawString(iNum.ToString(), font, SystemBrushes.ControlText, ClientRectangle, strf);
    }
}
 
