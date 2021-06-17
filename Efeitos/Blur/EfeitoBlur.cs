using ImagemFiltro.Efeitos.RGB;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImagemFiltro.Efeitos.Blur
{
    public class EfeitoBlur
    {
        public void Aplicar(PictureBox imgOrigem, PictureBox imgDesfoque)
        {
            var bitMap = new Bitmap(imgOrigem.Image);
            var bitDest = new Bitmap(imgOrigem.Image.Width, imgOrigem.Image.Height, PixelFormat.Format24bppRgb);
            imgDesfoque.Image = bitDest;

            for (int x = 0; x < imgOrigem.Image.Width; x++)
            {
                for (int y = 0; y < imgOrigem.Image.Height; y++)
                {
                    var pixelDesfocado = Desfocar(bitMap, x, y);
                    bitDest.SetPixel(x, y, pixelDesfocado);
                }             
            }
        }
        private Color Desfocar(Bitmap img, int x, int y)
        {
            var red = MediaCor(EnumCor.Red, img, x, y);
            var green = MediaCor(EnumCor.Green, img, x, y);
            var blue = MediaCor(EnumCor.Blue, img, x, y);

            return Color.FromArgb(red, green, blue);
        }

        private int MediaCor(EnumCor cor, Bitmap image, int x, int y)
        {
            var valores = new List<int>();
            var xCalc = 0;
            var yCalc = 0;

            // valor 1
            xCalc = x - 1;
            yCalc = y - 1;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);

            // valor 2
            xCalc = x;
            yCalc = y - 1;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);

            // valor 3
            xCalc = x + 1;
            yCalc = y - 1;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);

            // linha 2
            // valor 4
            xCalc = x - 1;
            yCalc = y;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);

            // valor 5
            xCalc = x;
            yCalc = y;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);

            // valor 6
            xCalc = x + 1;
            yCalc = y;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);


            // linha 3
            // valor 7
            xCalc = x - 1;
            yCalc = y + 1;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);

            // valor 8
            xCalc = x;
            yCalc = y + 1;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);

            // valor 9
            xCalc = x + 1;
            yCalc = y + 1;
            AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc);


            var media = valores.Average();
            var retorno = Convert.ToInt32(Math.Round(media, 0));
            return retorno;
        }

        private void AdicionarValorSePixelValido(List<int> valores, EnumCor cor, Bitmap image, int xCalc, int yCalc)
        {
            if (PixelValido(image, xCalc, yCalc))
            {
                var pixel = image.GetPixel(xCalc, yCalc);
                valores.Add(ResolverCor(cor, pixel));
            }
        }

        private int ResolverCor(EnumCor corDestino, Color pixel)
        {
            switch (corDestino)
            {
                case EnumCor.Red:
                    return pixel.R;
                case EnumCor.Blue:
                    return pixel.B;
                case EnumCor.Green:
                    return pixel.G;
                default:
                    throw new Exception("Não implementado");
            }
        }

        private bool PixelValido(Bitmap image, int x, int y)
        {
            if (x < 0)
                return false;

            if (y < 0)
                return false;

            if (x >= image.Width)
                return false;

            if (y >= image.Height)
                return false;

            return true;
        }
    }
}