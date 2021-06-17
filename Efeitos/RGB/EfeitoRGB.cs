using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImagemFiltro.Efeitos.RGB
{
    public class EfeitoRGB
    {
        internal void Aplicar(PictureBox imgOrigem, PictureBox imgDestino, EnumCor corDesejada)
        {
            var bitMap = new Bitmap(imgOrigem.Image);
            var bitDest = new Bitmap(imgOrigem.Image.Width, imgOrigem.Image.Height, PixelFormat.Format24bppRgb);
            imgDestino.Image = bitDest;

            for (int x = 0; x < imgOrigem.Image.Width; x++)
            {
                for (int y = 0; y < imgOrigem.Image.Height; y++)
                {
                    var pixel = bitMap.GetPixel(x, y);
                    var color = ResolveCor(pixel, corDesejada);
                    bitDest.SetPixel(x, y, color);
                }
            }
            imgDestino.Refresh();
        }
        private Color ResolveCor(Color pixel, EnumCor corDestino)
        {
            switch (corDestino)
            {
                case EnumCor.Red:
                    return Color.FromArgb(pixel.R, 0, 0);
                case EnumCor.Blue:
                    return Color.FromArgb(0, 0, pixel.B);
                case EnumCor.Green:
                    return Color.FromArgb(0, pixel.G, 0);
                default:
                    throw new Exception("Não implementado");
            }
        }
    }
}
