using ImagemFiltro.MetodoExtensao;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace ImagemFiltro.Efeitos.PB
{
    class EfeitoPB
    {
        internal void Aplicar(PictureBox imgOrigem, PictureBox imgDestino)
        {
            var bitMap = new Bitmap(imgOrigem.Image);
            var bitDest = new Bitmap(imgOrigem.Image.Width, imgOrigem.Image.Height, PixelFormat.Format24bppRgb);
            imgDestino.Image = bitDest;

            double limiarMedio = LimiarMedio(imgOrigem, bitMap);

            for (int x = 0; x < imgOrigem.Image.Width; x++)
            {
                for (int y = 0; y < imgOrigem.Image.Height; y++)
                {
                    var pixel = bitMap.GetPixel(x, y);
                    var color = ResolveCor(pixel, limiarMedio);
                    bitDest.SetPixel(x, y, color);
                }
            }
            imgDestino.Refresh();
        }

        private static double LimiarMedio(PictureBox imgOrigem, Bitmap bitMap)
        {
            var grau = new List<double>();
            for (int x = 0; x < imgOrigem.Image.Width; x++)
            {
                for (int y = 0; y < imgOrigem.Image.Height; y++)
                {
                    grau.Add(bitMap.GetPixel(x, y).GrauLuminosidade());
                }
            }
            var limiarMedio = grau.Average();
            return limiarMedio;
        }

        private Color ResolveCor(Color pixel, double limiar)
        {
            if (pixel.GrauLuminosidade() > limiar)
                return Color.White;
            else
                return Color.Black;
        }
    }
}
