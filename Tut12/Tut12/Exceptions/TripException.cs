namespace Tut12.Exceptions;

public class TripException : Exception
{
    public TripException(string message) : base(message){ }  
}
public class ClientAlreadyExistsException : TripException
{
    public ClientAlreadyExistsException() : base("Client with given PESEL already exists") { }
}

public class TripNotFoundException : TripException
{
    public TripNotFoundException() : base("Trip does not exist or already started") { }
}

public class ClientNotFoundException : TripException
{
    public ClientNotFoundException() : base("Client not found") { }
}

public class ClientHasTripsException : TripException
{
    public ClientHasTripsException() : base("Client has assigned trips and cannot be deleted") { }
}