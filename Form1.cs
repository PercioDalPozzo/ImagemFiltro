using ImagemFiltro.Efeitos.Blur;
using ImagemFiltro.Efeitos.PB;
using ImagemFiltro.Efeitos.RGB;
using ImagemFiltro.Efeitos.UltraBlur;
using System;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImagemFiltro
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
                imgOrigem.Load(of.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new EfeitoRGB().Aplicar(imgOrigem, imgResultado, EnumCor.Red);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new EfeitoRGB().Aplicar(imgOrigem, imgResultado, EnumCor.Green);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new EfeitoRGB().Aplicar(imgOrigem, imgResultado, EnumCor.Blue);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (imgResultado.Image == null)
                throw new Exception("Sem imagem.");

            imgResultado.Image.Save(@"c:\Temp\Imagem.png", ImageFormat.Png);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new EfeitoBlur().Aplicar(imgOrigem, imgResultado);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new EfeitoUltraBlur().Desfocar(imgOrigem, imgResultado, Convert.ToInt32(raio.Text), Convert.ToInt32(peso.Text));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new EfeitoPB().Aplicar(imgOrigem, imgResultado);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new EfeitoTomCinza().Aplicar(imgOrigem, imgResultado);
        }
    }
}
