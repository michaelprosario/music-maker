
public class MakeDrumTrackResponse {
    public int Code = 200;
    public string Message = "";
    public List<string> Errors = new List<string>();
    public string File{get;set} = "";
}

public class MakeDrumTrackService
{
    public MakeDrumTrackService(IMidServices midiServices){
    }

    MakeDrumTrackResponse makeDrumTrack(command: ) {
        MakeDrumTrackResponse response = new MakeDrumTrackResponse();

        // make sure command is defined
        if(!command){
            response.message = "command is not defined";
            response.code = 400;
            return response;
        }

        // make sure command is filled out
        if(command.tracks.length === 0){
            response.message = "command.tracks is not defined";
            response.code = 400;
            return response;
        }

        // make sure user is defined
        if(!command.userId || command.userId.length === 0){
            response.message = "command.userId is not defined";
            response.code = 400;
            return response;
        }

        return this.midiService.makeDrumTrack(command);
    }
}