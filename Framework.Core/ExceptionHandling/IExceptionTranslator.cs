using System;

namespace Framework.Core.ExceptionHandling
{
    public interface IExceptionTranslator
    {
        void Register(Type typeOfException, string keyword);

        Exception Translate(Exception exception);
    }
}