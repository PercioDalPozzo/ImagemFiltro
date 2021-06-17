using System;
using System.Drawing;

namespace ImagemFiltro.MetodoExtensao
{
    public static class ExtensaoColor
    {
        public static double GrauLuminosidade(this Color value)
        {
            return (value.R * 0.3) + (value.G * 0.59) + (value.B * 0.11);
        }

        public static int GrauLuminosidadeToInt(this Color value)
        {
            return Convert.ToInt32(value.GrauLuminosidade());
        }
    }
}
