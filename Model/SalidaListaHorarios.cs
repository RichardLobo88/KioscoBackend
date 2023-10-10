using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Kiosco.Model
{
    public class SalidaListaHorarios
    {
      public List<Horarios> horarios { get; set; }

         public  Resultado resultado { get; set; }


    }

    public class SalidaHorarios
    {
        public Horarios horario { get; set; }

        public Resultado resultado { get; set; }


    }
    
    public class Resultado {
     public string mensajes { get; set; }
        public bool  Error { get; set; }

        public Resultado(string mensajes) {

            if (mensajes.Equals("OK"))
            {
                this.mensajes = mensajes;
                this.Error = false;

            }
            else { 
            Error=true;
              this.mensajes = mensajes;
            }
        }     
    
   }
}
