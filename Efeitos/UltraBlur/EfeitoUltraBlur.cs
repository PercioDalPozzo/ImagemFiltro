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
        public void Aplicar(PictureBox imgOrigem, PictureBox imgResultad, int raio, int peso)
        {
            var bitMap = new Bitmap(imgOrigem.Image);
            var bitDest = new Bitmap(imgOrigem.Image.Width, imgOrigem.Image.Height, PixelFormat.Format24bppRgb);
            imgResultad.Image = bitDest;

            for (int x = 0; x < imgOrigem.Image.Width; x++)
            {
                for (int y = 0; y < imgOrigem.Image.Height; y++)
                {
                    var pixelDesfocado = Desfocar(bitMap, x, y, raio, peso);
                    bitDest.SetPixel(x, y, pixelDesfocado);
                }
            }
        }

        private class PesoCores
        {
            public PesoCores(int r, int g, int b)
            {
                R = r;
                G = g;
                B = b;
            }
            public int R { get; private set; }
            public int G { get; private set; }
            public int B { get; private set; }
        }

        private Color Desfocar(Bitmap img, int x, int y, int raio, int peso)
        {
            var inicioX = x - raio;
            var inicioY = y - raio;

            var tamanhoMatriz = (raio + 1 + raio);

            var limiteX = inicioX + tamanhoMatriz;
            var limiteY = inicioY + tamanhoMatriz;


            var matrixVirtual = new List<Point>();


            for (int xCalc = inicioX; xCalc < limiteX; xCalc++)
            {
                for (int yCalc = inicioY; yCalc < limiteY; yCalc++)
                {
                    matrixVirtual.Add(new Point(xCalc, yCalc));
                }
            }

            var validos = matrixVirtual.Where(p => PixelValido(img.Width, img.Height, p.X, p.Y)).ToList();
            var pesoCores = new List<PesoCores>();

            foreach (var valido in validos)
            {
                var pesoCalc = 1;
                if ((x == valido.X) && (y == valido.Y))
                    pesoCalc = peso;

                var pixel = img.GetPixel(x, y);
                var r = pixel.R * pesoCalc;
                var g = pixel.G * pesoCalc;
                var b = pixel.B * pesoCalc;
                pesoCores.Add(new PesoCores(r, g, b));
            }




            var red = Media(peso, pesoCores.Select(p => p.R).ToList());
            var green = Media(peso, pesoCores.Select(p => p.G).ToList());
            var blue = Media(peso, pesoCores.Select(p => p.B).ToList());

            return Color.FromArgb(red, green, blue);
        }

        private int Media(int peso, List<int> pesoCores)
        {
            var aa = pesoCores.Count - 1 + peso;
            decimal media = (pesoCores.Sum() / aa);
            return Convert.ToInt32(Math.Round(media, 0));
        }

        private bool PixelValido(int imageWidth, int imageHeight, int x, int y)
        {
            if (x < 0)
                return false;

            if (y < 0)
                return false;

            if (x >= imageWidth)
                return false;

            if (y >= imageHeight)
                return false;

            return true;
        }
    }
}