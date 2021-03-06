using System;

public class Codec : IPacketReceived{

    public void handlePacket(string packet){
        
        if (packet == "move north"){
            commands.move(Directions.North);
        }
        else{
            Console.WriteLine("Received unknown command at the codec: {0}", packet);
        }
    }
    
    private ICommand commands;

    public Codec(ICommand commands){
        this.commands = commands;
    }


}