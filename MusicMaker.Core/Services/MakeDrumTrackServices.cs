
public class MakeDrumTrackResponse {
    public int Code = 200;
    public string Message = "";
    public List<string> Errors = new List<string>();
    public string File{get;set;} = "";
}

public class MakeDrumTrackService
{
    private readonly IMidiServices midiServices;

    public MakeDrumTrackService(IMidiServices midiServices){
        this.midiServices = midiServices ?? throw new ArgumentNullException(nameof(midiServices));
    }

    MakeDrumTrackResponse MakeDrumTrack(MakeDrumTrackCommand command) {
        if(command == null)
        {
            throw new ArgumentException("command is required");
        }

        MakeDrumTrackResponse response = new MakeDrumTrackResponse();

        // make sure command is filled out
        if(command.Tracks.Count == 0){
            response.Message = "command.tracks is not defined";
            response.Code = 400;
            return response;
        }

        // make sure user is defined
        if(string.IsNullOrEmpty(command.UserId))
        {
            response.Message = "command.userId is not defined";
            response.Code = 400;
            return response;
        }

        return this.midiServices.MakeDrumTrack(command);
    }
}