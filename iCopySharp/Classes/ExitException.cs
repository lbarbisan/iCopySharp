using System;

namespace iCopy
{
    [Serializable]
    public class ExitException : Exception
    {
        public override string Message
        {
            get
            {
                return "Exit";
            }
        }
    }
}