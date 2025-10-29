using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ejercicio1
{
    public class Exercise
    {
        public class Transaccion
        {
            public int identificador { get; set; }
            public string fechaTransaccion { get; set; }
        }

        public class TransaccionEventArgs : EventArgs
        {
            public Transaccion transaccion { get; set; }
        }

        public class PasarelaDePago
        {
            public event EventHandler<TransaccionEventArgs> TransaccionFinalizada;

            protected virtual void EnTransaccionFinalizada(Transaccion transaccion_)
            {
                if (TransaccionFinalizada != null) 
                {
                    TransaccionFinalizada(this, new TransaccionEventArgs() { transaccion = transaccion_ }); 
                }
            }

            public void Pago(Transaccion transaccion_)
            {
                Console.WriteLine($"ID transacción: {transaccion_.identificador} con fecha: {transaccion_.fechaTransaccion}"); 
                Thread.Sleep(2000); 
                Console.WriteLine("Pago aprobado y fin de transacción."); 
                EnTransaccionFinalizada(transaccion_); 
            }
        }

        public class GestorDeEmail
        {
            public void EnviarNotificacion(object sender, TransaccionEventArgs e)
            {
                Console.WriteLine($"Notificación: El pago de la transacción {e.transaccion.identificador} fue creado correctamente el {e.transaccion.fechaTransaccion}.");
            }
        }

      
        public class GestorDeFacturacion 
        {
          
            public void EmitirFactura(object sender, TransaccionEventArgs e) 
            {
                Console.WriteLine($"La factura de la transacción {e.transaccion.identificador} fue creada el {e.transaccion.fechaTransaccion}.");
            }
        }

        public static void Main(string[] args)
        {
            Transaccion miTransaccion = new Transaccion()
            {
                identificador = 71645331,
                fechaTransaccion = "30/06/2010"
            };

            PasarelaDePago miPasarela = new PasarelaDePago();

            GestorDeEmail miGestorEmail = new GestorDeEmail();
            miPasarela.TransaccionFinalizada += miGestorEmail.EnviarNotificacion; 

            GestorDeFacturacion miGestorFacturacion = new GestorDeFacturacion();
            miPasarela.TransaccionFinalizada += miGestorFacturacion.EmitirFactura;

            miPasarela.Pago(miTransaccion);
        }
    }
}
