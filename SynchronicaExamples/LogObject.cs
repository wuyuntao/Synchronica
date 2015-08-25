using System;

namespace Synchronica.Examples
{
    abstract class LogObject
    {
        protected void Log(string msg, params object[] args)
        {
            msg = string.Format(msg, args);

            Console.WriteLine("{0:hh:mm:ss}|{1}|{2}", DateTime.Now, this, msg);
        }
    }
}
