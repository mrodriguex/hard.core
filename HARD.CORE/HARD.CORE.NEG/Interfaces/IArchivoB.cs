using HARD.CORE.DAT;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG.Interfaces
{
    public interface IArchivoB
    {
        // public byte[] ObtenerPoliticasPrivacidad();
        public byte[] ObtenerDocumentoTanqueSTPS(int claveCliente, int numeroEconomico);
        // public byte[] ObtenerCertificadoCalidad(int folioRemision, string serieRemision);
        // public byte[] ObtenerFotoNivelInicial(int folioRemision, string serieRemision);
        // public byte[] ObtenerFotoNivelFinal(int folioRemision, string serieRemision);
        // public byte[] ObtenerFotoPresionInicial(int folioRemision, string serieRemision);
        // public byte[] ObtenerFotoPresionFinal(int folioRemision, string serieRemision);

    }
}
