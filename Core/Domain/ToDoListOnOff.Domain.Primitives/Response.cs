namespace ToDoListOnOff.Domain.Primitives;

    public class ResponseData<TResponse> : Response
    {
        /// <summary>
        /// Respuesta
        /// </summary>
        public TResponse? Data { get; set; }
    }
    public class ResponseDataPaginate<TResponse> : ResponseData<List<TResponse>>
    {
        /// <summary>
        /// Pagina actual
        /// </summary>
        public int ActualPage { get; set; }
        /// <summary>
        /// Cantidad de páginas
        /// </summary>
        public int CountPages { get; set; }
        /// <summary>
        /// Cantidad de datos almacenados
        /// </summary>
        public int CountData { get; set; }
    }

    public class Response 
    {
        /// <summary>
        /// Mensaje que entrega la aplicación
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Errores que puedan surgir en la aplicación
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
        /// <summary>
        /// Estado de la respuesta
        /// </summary>
        public StatusResponse StatusResponse { get; set; }
    }


    public enum StatusResponse
    {
        /// <summary>
        /// Estado Ok
        /// </summary>
        Ok = 200,
        /// <summary>
        /// Estado Ok sin contenido
        /// </summary>
        NoContent = 202,
        /// <summary>
        /// No autorizado (acceso)
        /// </summary>
        UnAuthorize = 401,
        /// <summary>
        /// Sin permisos (protegido)
        /// </summary>
        Forbbiden = 403,
        /// <summary>
        /// Ocurre un erro controlado
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Codigo para registros no encontrados
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// Ocurre un error desconocido
        /// </summary>
        Error = 500
    }

    /// <summary>
    /// Clase extensiva de response
    /// </summary>
    public static class ResponseExtension
    {
        /// <summary>
        /// Metodo que retorna el mismo response con un nuevo error en la lista
        /// </summary>
        /// <param name="response">Objeto base</param>
        /// <param name="newError">Error a agregar</param>
        /// <typeparam name="TResponse">Tipo del ResponseData</typeparam>
        /// <returns>Retorna el mismo objeto con un error nuevo</returns>
        public static ResponseData<TResponse> AddNewError<TResponse>(this ResponseData<TResponse> response, string newError)
        {
            response.Errors.Add(newError);
            return response;
        }
    }