using HARD.CORE.OBJ;

using HARD.CORE.NEG.Interfaces;
using HARD.CORE.DAT.Interfaces;
using System.Collections.Generic;

namespace HARD.CORE.NEG
{
    public class CorreoVariableB : ICorreoVariableB
    {
        private readonly ICorreoVariableDA _correoVariableDA;

        public CorreoVariableB(ICorreoVariableDA correoVariableDA)
        {
            _correoVariableDA = correoVariableDA;
        }

        public CorreoVariable Obtener(int claveCorreoVariable)
        {
            // CorreoVariable correoVariable = new CorreoVariable();
            // correoVariable.ClaveCorreoVariable = claveCorreoVariable;
            // correoVariable.ClaveUsuarioUltimaActualizacion = "administrador";
            // correoVariable.Descripcion = $"Descripción de ejemplo {claveCorreoVariable}";
            // correoVariable.Estatus = true;
            // correoVariable.Etiqueta = $"Etiqueta de ejemplo {claveCorreoVariable}";
            // correoVariable.Valor = $"Valor de ejemplo {claveCorreoVariable}";

            // return correoVariable;

            return _correoVariableDA.Obtener(claveCorreoVariable);
        }

        public List<CorreoVariable> ObtenerTodos(int? claveTipoCorreo = null, bool? estatus = false)
        {
            // List<CorreoVariable> correoVariables = new List<CorreoVariable>();

            // for (int i = 1; i < 11; i++)
            // {
            //     correoVariables.Add(Obtener(claveCorreoVariable: i));
            // }
            // return correoVariables;
            return _correoVariableDA.ObtenerTodos(claveTipoCorreo: claveTipoCorreo, estatus: estatus);
        }

    }
}
