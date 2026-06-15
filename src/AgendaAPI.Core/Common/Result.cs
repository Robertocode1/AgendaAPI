namespace AgendaAPI.Core.Common;

// Usamos una clase genérica. El <T> significa "el tipo de dato que devuelve en caso de éxito".
// Ejemplo: Result<Reservation> significa "o me das una Reservation, o me das un error".
public class Result<T>
{
    // Privados: nadie puede crear un Result directamente. 
    // Debe usar los métodos estáticos Success o Failure.
    private Result(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string? Error { get; }

    // Método estático para crear un Result exitoso.
    // Uso: Result<Reservation>.Success(reserva);
    public static Result<T> Success(T value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value), "El valor de éxito no puede ser null.");
        
        return new Result<T>(true, value, null);
    }

    // Método estático para crear un Result fallido.
    // Uso: Result<Reservation>.Failure("La fecha no puede ser en el pasado");
    public static Result<T> Failure(string error)
    {
        if (string.IsNullOrWhiteSpace(error))
            throw new ArgumentException("El mensaje de error no puede estar vacío.", nameof(error));
        
        return new Result<T>(false, default, error);
    }
}

// Versión sin valor de retorno, solo para operaciones que no devuelven nada.
// Ejemplo: "Cancelar reserva" no devuelve nada, solo éxito o fracaso.
public class Result
{
    private Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }

    public static Result Success() => new Result(true, null);

    public static Result Failure(string error)
    {
        if (string.IsNullOrWhiteSpace(error))
            throw new ArgumentException("El mensaje de error no puede estar vacío.", nameof(error));
        
        return new Result(false, error);
    }
}