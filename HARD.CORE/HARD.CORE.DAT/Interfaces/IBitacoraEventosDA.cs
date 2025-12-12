using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;

namespace HARD.CORE.DAT.Interfaces
{
    public interface IBitacoraEventosDA
    {
        List<BitacoraEventos> ObtenerTodos(int? claveEvento = null, int? claveTipoEvento = null, int? claveEntidad = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null);
        BitacoraEventos Obtener(int claveBitacoraEventos);
        List<BitacoraAcessos> BitacoraAcessos();
        int Insertar(BitacoraEventos bitacora);
    }
}
