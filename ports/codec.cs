using System;

class Codec : IPacketReceived{

    public void handlePacket(int portID, Byte[] bytes){
        //convert the byte packet into a command. 
        String data = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);

        if (data == "move north"){
            commands.move(Directions.North);
        }
        else{
            Console.WriteLine("Received unknown command at the codec: {0}", data);
        }
        
        
    }
    
    private ICommand commands;

    public Codec(ICommand commands){
        this.commands = commands;
    }


}