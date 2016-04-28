

using System;


namespace ActiveDirectoryInteractor{
    /// <summary>
    /// Exception thats thrown when no computer name is given
    /// </summary>
    public class NoComputerNameProvidedException : Exception{
        /// <summary>
        /// Constructor
        /// </summary>
        public NoComputerNameProvidedException(){
                
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message to explain exception</param>
        public NoComputerNameProvidedException(string message) : base(message){

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message to explain exception</param>
        /// <param name="inner">Exception to add to this exception</param>
        public NoComputerNameProvidedException(string message, Exception inner) : base(message, inner){

        }
    }
}
