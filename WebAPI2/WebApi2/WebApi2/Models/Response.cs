using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Currency.CustomModels
{
    public class Response

    {
        

        public bool IsSuccess { get; set; }

        /// <summary>
        /// Resultado  de operacion solicitada. Ej. OK,error
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// Conjunto de contratos con sus artículos
        /// </summary>
        public object Data { get; set; }

    

        public Response (bool IsSuccess, String Message, object Data)
        {
          
            this.IsSuccess = IsSuccess;
            this.Message = Message;
            this.Data = Data;
        }
    }
}
