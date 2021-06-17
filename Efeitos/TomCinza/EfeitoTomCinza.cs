using ImagemFiltro.MetodoExtensao;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImagemFiltro.Efeitos.PB
{
    class EfeitoTomCinza
    {
        internal void Aplicar(PictureBox imgOrigem, PictureBox imgDestino)
        {
            var bitMap = new Bitmap(imgOrigem.Image);
            var bitDest = new Bitmap(imgOrigem.Image.Width, imgOrigem.Image.Height, PixelFormat.Format24bppRgb);
            imgDestino.Image = bitDest;

            for (int x = 0; x < imgOrigem.Image.Width; x++)
            {
                for (int y = 0; y < imgOrigem.Image.Height; y++)
                {
                    var pixel = bitMap.GetPixel(x, y);
                    var color = ResolveCor(pixel);
                    bitDest.SetPixel(x, y, color);
                }
            }
            imgDestino.Refresh();
        }
        private Color ResolveCor(Color pixel)
        {
            return Color.FromArgb(pixel.GrauLuminosidadeToInt(), pixel.GrauLuminosidadeToInt(), pixel.GrauLuminosidadeToInt());
        }
    }
}
