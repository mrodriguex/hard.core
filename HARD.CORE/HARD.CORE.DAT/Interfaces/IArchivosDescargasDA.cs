using HARD.CORE.OBJ;
using System.Collections.Generic;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IArchivosDescargasDA
    {
        // public byte[] ObtenerPoliticasPrivacidad();
        public byte[] ObtenerSTPS(int claveCliente, int numeroEconomico);
    }
}
