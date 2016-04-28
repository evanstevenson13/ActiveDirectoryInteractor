

using System;


namespace Device42Interactor{
    public class NoComputerNameProvidedException : Exception{
        public NoComputerNameProvidedException(){
                
        }

        public NoComputerNameProvidedException(string message) : base(message){

        }

        public NoComputerNameProvidedException(string message, Exception inner) : base(message, inner){

        }
    }
}
