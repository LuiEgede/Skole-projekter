namespace Heltevagten.Exceptions;

public class HeroUnavailableException : Exception
{
    public HeroUnavailableException()
    {
    }

    public HeroUnavailableException(string message) : base(message)
    {
    }
}