using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using Catalog.gRPC.Apis;
using Catalog.gRPC.Application.Commands;
using Catalog.gRPC.Application.Models;
using Catalog.gRPC.v1;
using Catalogs.Domain.AggregateModel.CatalogRecordItemAggregate.Attributes;
using EventBus.Extentions;
using Grpc.Core;
using Type = Google.Protobuf.WellKnownTypes.Type;

namespace Catalog.gRPC.Services;

public class CatalogService:Catalog.gRPC.v1.CatalogService.CatalogServiceBase
{
    private ICatalogServices services;
    public CatalogService(ICatalogServices catalogServices)
    {
        services = catalogServices;
    }
  public override async Task CreateCatalod(IAsyncStreamReader<CatalogCreationRequest> requestStream,
      IServerStreamWriter<CatalogCreationReqply> responseStream, ServerCallContext context)
  {
    Guid requestId = Guid.Empty;

    var type_str_full = typeof(IntAttribute).AssemblyQualifiedName;
    
    var tt = System.Type.GetType(type_str_full);
    
    if(context.RequestHeaders.FirstOrDefault(h=>h.Key=="x-request-id")!=null)
          requestId = Guid.Parse(context.RequestHeaders.SingleOrDefault(h => h.Key == "x-request-id").Value);

    if (requestId == Guid.Empty)
    {
        services.Logger.LogWarning("Invalid integration event - RequestId is missing - {@IntegrationEvent}", requestStream);
    }

    using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> {new ("identifiedCommandId",requestId)}))
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            
            var catalogAppModel = new CatalogItemAppModel
            {
                Name = request.Name,
                Description = request.Description,
                UseStandardCommands = request.UseStandardCommands,
                Autonumbering = request.Autonumbering,
                Code = request.Code,
                Synonym = request.Synonym,
                CheckUnique = request.CheckUnique,
                ChoiceMode = request.ChoiceMode,
                CodeLength = request.CodeLength,
                CodeType = request.CodeType,
                DefaultPresentation = request.DefaultPresentation,
                DescriptionLength = request.DescriptionLength,
                EditType = request.EditType,
                LevelCount = request.LevelCount,
                CodeAllowedLength = request.CodeAllowedLength,
                CreateOnInput = request.CreateOnInput,
                FoldersOnTop = request.FoldersOnTop,
                FullTextSearch = request.FullTextSearch,
                DataLockControlMode = request.DataLockControlMode,
                AttributeDescriotions = request.AttributeDescriptions
                    .Select(rq_ad=> new AttributeDescriptionAppModel
                    {
                        Id =  rq_ad.Id,
                        Description = rq_ad.Description,
                        Synonym = rq_ad.Synonym,
                        AttributeTypeName = rq_ad.AttributeType,
                        AttributeName = rq_ad.AttributeName,
                        Properties = rq_ad.Properties.Select(p =>
                        new KeyValuePair<string, string>(p.Key, p.Value)
                            ).ToDictionary(kvp=>kvp.Key,kvp=>kvp.Value),
                        
                    }).ToList()
            
            };
         
            var createCatalogCommand = new CreateCatalogCommand(catalogAppModel);
      
            var requestCreateCatalog = new IdentifiedCommand<CreateCatalogCommand,int>(createCatalogCommand, requestId);
           
           services.Logger.LogInformation(
               "Sending command:{CommandName} - {IdProperty}: {CommandId} ({@Command})",
               requestCreateCatalog.GetGenericTypeName(),
               nameof(requestCreateCatalog.Id),
               requestCreateCatalog.Id,
               requestCreateCatalog);
           var result = await services.Mediator.Send(requestCreateCatalog);

           if (result!= default(int))
           {
               services.Logger.LogInformation("CreateCatalogComman succeeded - RequestId: {RequstId}", requestId);
           }
           else
           {
               services.Logger.LogWarning("CreateOrderCommand failed - RequestId: {RequestId}", requestId);
           }

           await responseStream.WriteAsync(new CatalogCreationReqply
           {
               Id = result.ToString(),
               Name = request.Name,
           });
        }
        
    }
    
    await  Task.CompletedTask; //base.CreateCatalod(requestStream, responseStream, context);
  }

  public override async Task AddAttribute(IAsyncStreamReader<AddingAttributeRequest> requestStream, IServerStreamWriter<AddingAttributeReply> responseStream, ServerCallContext context)
  {
      Guid requestId = Guid.Empty;

      var type_str_full = typeof(IntAttribute).AssemblyQualifiedName;
    
      var tt = System.Type.GetType(type_str_full);
    
      if(context.RequestHeaders.FirstOrDefault(h=>h.Key=="x-request-id")!=null)
          requestId = Guid.Parse(context.RequestHeaders.SingleOrDefault(h => h.Key == "x-request-id").Value);

      if (requestId == Guid.Empty)
      {
          services.Logger.LogWarning("Invalid integration event - RequestId is missing - {@IntegrationEvent}", requestStream);
      }

      using (services.Logger.BeginScope(
                 new List<KeyValuePair<string, object>> { new("identifiedCommandId", requestId) }))
      {
          await foreach (var request in requestStream.ReadAllAsync())
          {
              var attributeDescription = new AttributeDescriptionAppModel
              {
                  AttributeName = request.AttributeName,
                  Description = request.Description,
                  Synonym = request.Synonym,
                  AttributeTypeName = request.AttributeType,
                  Properties = request.Properties.Select(p =>
                          new KeyValuePair<string, string>(p.Key, p.Value))
                      .ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
              };
              var addAttributeDescriptionCommand = new AddAttributeDescriptionCommand(attributeDescription);

          }
          
          
      }
      

      return base.AddAttribute(requestStream, responseStream, context);
  }
}