using ImagemFiltro.Efeitos.RGB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace ImagemFiltro.Efeitos.UltraBlur
{
    public class EfeitoUltraBlur
    {
        public void Desfocar(PictureBox imgOrigem, PictureBox imgDesfoque, int raio, int peso)
        {
            var bitMap = new Bitmap(imgOrigem.Image);
            var bitDest = new Bitmap(imgOrigem.Image.Width, imgOrigem.Image.Height, PixelFormat.Format24bppRgb);

            for (int x = 0; x < imgOrigem.Image.Width; x++)
            {
                for (int y = 0; y < imgOrigem.Image.Height; y++)
                {
                    var pixelDesfocado = Desfocar(bitMap, x, y, raio, peso);
                    bitDest.SetPixel(x, y, pixelDesfocado);
                }
            }
            imgDesfoque.Image = bitDest;

        }
        private Color Desfocar(Bitmap img, int x, int y, int raio, int peso)
        {
            var red = MediaCor(EnumCor.Red, img, x, y, raio, peso);
            var green = MediaCor(EnumCor.Green, img, x, y, raio, peso);
            var blue = MediaCor(EnumCor.Blue, img, x, y, raio, peso);

            return Color.FromArgb(red, green, blue);
        }

        private int MediaCor(EnumCor cor, Bitmap image, int x, int y, int raio, int peso)
        {
            var valores = new List<int>();

            var inicioX = x - raio;
            var inicioY = y - raio;

            var tamanhoMatriz = (raio + 1 + raio);

            var limiteX = inicioX + tamanhoMatriz;
            var limiteY = inicioY + tamanhoMatriz;

            for (int xCalc = inicioX; xCalc < limiteX; xCalc++)
            {
                for (int yCalc = inicioY; yCalc < limiteY; yCalc++)
                {
                    var pesoCalc = 1;
                    if (xCalc == x && yCalc == y) // é o pixel base
                        pesoCalc = peso;

                    AdicionarValorSePixelValido(valores, cor, image, xCalc, yCalc, raio, pesoCalc);
                }
            }


            var media = valores.Average();
            var retorno = Convert.ToInt32(Math.Round(media, 0));

            // tratamento para não extrapolar o valor máximo do RGB
            if (retorno > 255)
                retorno = 255;

            return retorno;
        }

        private void AdicionarValorSePixelValido(List<int> valores, EnumCor cor, Bitmap image, int xCalc, int yCalc, int raio, int peso)
        {
            if (PixelValido(image, xCalc, yCalc))
            {
                var pixel = image.GetPixel(xCalc, yCalc);
                var corComPeso = ResolverCor(cor, pixel) * peso;
                valores.Add(corComPeso);
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