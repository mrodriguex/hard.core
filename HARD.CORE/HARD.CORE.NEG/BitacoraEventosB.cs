using HARD.CORE.DAT.Interfaces;
using HARD.CORE.NEG.Interfaces;
using HARD.CORE.OBJ;
using System;
using System.Collections.Generic;

namespace HARD.CORE.NEG
{
    public class BitacoraEventosB : IBitacoraEventosB
    {

        IBitacoraEventosDA _bitacoraEventosDA;

        public BitacoraEventosB(IBitacoraEventosDA bitacoraEventosDA)
        {
            _bitacoraEventosDA = bitacoraEventosDA;
        }

        public BitacoraEventos Obtener(int claveBitacoraEventos)
        {
            return _bitacoraEventosDA.Obtener(claveBitacoraEventos);
        }

        public List<BitacoraEventos> ObtenerTodos(int? claveEvento = null, int? claveTipoEvento = null,int? claveEntidad = null, DateTime? fechaInicial = null, DateTime? fechaFinal = null)
        {
            List<BitacoraEventos> bitacoraEventoss = _bitacoraEventosDA.ObtenerTodos(claveEvento: claveEvento, claveTipoEvento: claveTipoEvento, claveEntidad: claveEntidad, fechaInicial: fechaInicial, fechaFinal: fechaFinal);            
            return bitacoraEventoss;
        }

        public int Insertar(BitacoraEventos bitacoraEventos)
        {
            return _bitacoraEventosDA.Insertar(bitacoraEventos);
        }

    }

}
