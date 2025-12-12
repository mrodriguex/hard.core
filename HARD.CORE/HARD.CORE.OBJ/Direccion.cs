namespace HARD.CORE.OBJ
{
    public class Direccion
    {

        public TipoUbicacion TipoUbicacion { get; set; }
        public int ClaveUbicacion { get; set; }
        public string Ubicacion { get; set; }
        public Pais Pais { get; set; }
        public Estado Estado { get; set; }
        public string DelegacionMunicipio { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencias { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string DireccionCompleta { get; set; }
        public Telefono Telefono { get; set; }

        public Direccion()
        {
            Ubicacion = "";
            DelegacionMunicipio = "";
            Colonia = "";
            Calle = "";
            NumeroExterior = "";
            NumeroInterior = "";
            CodigoPostal = "0";
            Referencias = "";
            DireccionCompleta = "";
        }

        public Direccion(TipoUbicacion tipoUbicacion, int claveUbicacion, string ubicacion, Pais pais, Estado estado, string delegacionMunicipio, string colonia, string calle, string numeroExterior, string numeroInterior, string codigoPostal, string referencias, decimal latitud, decimal longitud, Telefono telefono)
        {

            TipoUbicacion = tipoUbicacion;
            ClaveUbicacion = claveUbicacion;
            Ubicacion = ubicacion;
            Pais = pais;
            Estado = estado;
            DelegacionMunicipio = delegacionMunicipio;
            Colonia = colonia;
            Calle = calle;
            NumeroExterior = numeroExterior;
            NumeroInterior = (numeroInterior.Trim() == "") ? "N/A" : numeroInterior;
            CodigoPostal = codigoPostal;
            Referencias = referencias;
            Latitud = latitud;
            Longitud = longitud;
            Telefono = telefono;
            try
            {
                DireccionCompleta = this.ToString();
            }
            catch { }

        }

        public override string ToString()
        {

            if (Estado == null)
            {
                return "";
            }

            NumeroExterior = NumeroExterior.Trim();
            NumeroInterior = NumeroInterior.Trim();

            return Calle.Trim()
                + ((NumeroExterior != "") ? " No Ext. " + NumeroExterior : "")
                + ((NumeroInterior != "") ? " No Int. " + NumeroInterior : "")
                + ", Col. " + Colonia.Trim()
                + ". " + DelegacionMunicipio.Trim()
                + ((Estado != null) ? ", " + Estado.Descripcion.Trim() : "")
                + ((Pais != null) ? ", " + Pais.Descripcion.Trim() : "")
                + ". C.P. " + CodigoPostal.Trim();

        }

    }
}
