using HARD.CORE.DAT;
using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARD.CORE.NEG
{
    public class ArchivoB : IArchivoB
    {
        private readonly IArchivosDescargasDA _archivosDescargasDA;

        public ArchivoB(IArchivosDescargasDA archivosDescargasDA)
        {
            _archivosDescargasDA = archivosDescargasDA;
        }


        // public byte[] ObtenerPoliticasPrivacidad()
        // {
        //     return _archivosDescargasDA.ObtenerPoliticasPrivacidad();
        // }
        public byte[] ObtenerDocumentoTanqueSTPS(int claveCliente, int numeroEconomico)
        {
            return _archivosDescargasDA.ObtenerSTPS(claveCliente, numeroEconomico);
        }
        // public byte[] ObtenerCertificadoCalidad(int folioRemision, string serieRemision)
        // {
        //     return _archivosDescargasDA.ObtenerPoliticasPrivacidad();
        // }
        // public byte[] ObtenerFotoNivelInicial(int folioRemision, string serieRemision)
        // {
        //     return _archivosDescargasDA.ObtenerPoliticasPrivacidad();
        // }
        // public byte[] ObtenerFotoNivelFinal(int folioRemision, string serieRemision)
        // {
        //     return _archivosDescargasDA.ObtenerPoliticasPrivacidad();
        // }
        // public byte[] ObtenerFotoPresionInicial(int folioRemision, string serieRemision)
        // {
        //     return _archivosDescargasDA.ObtenerPoliticasPrivacidad();
        // }
        // public byte[] ObtenerFotoPresionFinal(int folioRemision, string serieRemision)
        // {
        //     return _archivosDescargasDA.ObtenerPoliticasPrivacidad();
        // }

    }
}
