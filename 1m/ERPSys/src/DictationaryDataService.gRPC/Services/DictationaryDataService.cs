using DictationaryDataService.Domain.AggregatesModel.DictationaryDataAggregate;
using DictationaryDataService.gRPC;
using DictationaryDataService.gRPC.v1;
using Grpc.Core;

namespace DictationaryDataService.gRPC.Services;

public class DictationaryDataService:v1.DictationaryDataService.DictationaryDataServiceBase
{
    
    public override async Task CreateDictationary(IAsyncStreamReader<DictaionaryCreateRequest> requestStream, IServerStreamWriter<DictaionaryCreateReply> responseStream,
        ServerCallContext context)
    {
        
        
        return base.CreateDictationary(requestStream, responseStream, context);
    }
}