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

    /*var type_str_full = typeof(IntAttribute).AssemblyQualifiedName;
    
    var tt = System.Type.GetType(type_str_full);*/
    
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
                  CatalogItemId = request.CataloItemId,
                  AttributeName = request.AttributeName,
                  Description = request.Description,
                  Synonym = request.Synonym,
                  AttributeTypeName = request.AttributeType,
                  Properties = request.Properties.Select(p =>
                          new KeyValuePair<string, string>(p.Key, p.Value))
                      .ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
              };
              var addAttributeDescriptionCommand = new AddAttributeDescriptionCommand(attributeDescription);
                
              var requestAddAttributeDescription= new IdentifiedCommand<AddAttributeDescriptionCommand,(int,int)>
                                                                                    (addAttributeDescriptionCommand, requestId);
            
              services.Logger.LogInformation(
                  "Sending command:{CommandName} - {IdProperty}: {CommandId} ({@Command})",
                  requestAddAttributeDescription.GetGenericTypeName(),
                  nameof(requestAddAttributeDescription.Id),
                  requestAddAttributeDescription.Id,
                  requestAddAttributeDescription);
    
                var result = await services.Mediator.Send(requestAddAttributeDescription);

                if (result.Item1 != default(int) && result.Item2 != default(int))
                {
                    services.Logger.LogInformation("CreateOrderCommand succeeded - RequestId: {RequestId}", requestId);
                }
                else
                {
                    services.Logger.LogWarning("CreateOrderCommand failed - RequestId: {RequestId}", requestId);
                }

                await responseStream.WriteAsync(new AddingAttributeReply
                {
                    Id = result.ToString(),
                    CatalogItemId = result.Item2.ToString(),
                });

          }
          
          
      }
   
     
  }

  public override async Task AddNewCatalogRecordItem(IAsyncStreamReader<AddNewCatalogRecordItemRequest> requestStream, IServerStreamWriter<AddNewCatalogRecordItemReply> responseStream,
      ServerCallContext context)
  {
      Guid requestId = Guid.Empty;

   
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
              var catalocRecorditem = new CatalogRecordItemAppModel
              {
                  CatalogItemId = request.CatalogItemId
              };
             
              var addNewCatalogRecordItemCommand = new AddNewCatalogRecordItemCommand(catalocRecorditem);

              var requestAddNewcatalogRecordItem =
                  new IdentifiedCommand<AddNewCatalogRecordItemCommand,int>(addNewCatalogRecordItemCommand, requestId);
              services.Logger.LogInformation(
                  "Sending command:{CommandName}: {CommandId} ({@Command})",
                  addNewCatalogRecordItemCommand.GetGenericTypeName(),
                  requestAddNewcatalogRecordItem.Id,
                  requestAddNewcatalogRecordItem);
                 
              var result = await services.Mediator.Send(requestAddNewcatalogRecordItem);
              
              if(result != default(int))
              {
                  services.Logger.LogInformation("CreateOrderCommand succeeded - RequestId: {RequestId}", requestId);
              }
              else
              {
                  services.Logger.LogWarning("CreateOrderCommand failed - RequestId: {RequestId}", requestId);
              }

              await responseStream.WriteAsync(
              
                  new AddNewCatalogRecordItemReply
                  {
                        CatalogItemRecordId  = result.ToString(),
                  }
              );
          }
      }
  }

  public override async Task AddCatalogRecordItem(IAsyncStreamReader<AddCatalogRecordItemRequest> requestStream, IServerStreamWriter<AddCatalogRecordItemReply> responseStream,
      ServerCallContext context)
  {
      Guid requestId = Guid.Empty;


    
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
              var catalorRecorditem = new CatalogRecordItemAppModel
              {
                  CatalogItemId = request.CatalogItemId,
                  Attributes = request.Attributes.Select(
                      a => new AttributeAppModel()
                      {
                          Name = a.Name,
                          Type = a.Type,
                          Value = a.Value
                      }).ToList()
              };
              var addCatalogRecordItemCommand = new AddCatalogRecordItemCommand(catalorRecorditem);
              
              var requestAddCatalogRecordItem = new  IdentifiedCommand<AddCatalogRecordItemCommand,int>(addCatalogRecordItemCommand, requestId);
            
              services.Logger.LogInformation(
                  "Sending command:{CommandName}: {CommandId} ({@Command})",
                  addCatalogRecordItemCommand.GetGenericTypeName(),
                  requestAddCatalogRecordItem.Id,
                  requestAddCatalogRecordItem);
            
              var result = await services.Mediator.Send(requestAddCatalogRecordItem);
              
              if(result != default(int))
              {
                  services.Logger.LogInformation("CreateOrderCommand succeeded - RequestId: {RequestId}", requestId);
              }
              else
              {
                  services.Logger.LogWarning("CreateOrderCommand failed - RequestId: {RequestId}", requestId);
              }

              await responseStream.WriteAsync(new AddCatalogRecordItemReply
              {
                  CatalogItemRecordId = result.ToString(),
              });

          }
      }
  }

  public override async Task GetCatalogItem(IAsyncStreamReader<GetCatalogItemRequest> requestStream, IServerStreamWriter<GetCatalogItemReply> responseStream, ServerCallContext context)
  {
      Guid requestId = Guid.Empty;
      
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

              var catalogItem = await services.CatalogItemQueries.GetCatalogItemAsync(Int32.Parse(request.CatalogId));
          
              await responseStream.WriteAsync(new GetCatalogItemReply
              {
                  
              });
          }
      }
  }
}
