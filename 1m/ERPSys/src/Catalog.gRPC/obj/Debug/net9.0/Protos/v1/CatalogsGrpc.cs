// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/v1/catalogs.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Catalog.gRPC.v1 {
  public static partial class CatalogService
  {
    static readonly string __ServiceName = "Catalog.v1.CatalogService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.CatalogCreationRequest> __Marshaller_Catalog_v1_CatalogCreationRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.CatalogCreationRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.CatalogCreationReqply> __Marshaller_Catalog_v1_CatalogCreationReqply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.CatalogCreationReqply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.AddingAttributeRequest> __Marshaller_Catalog_v1_AddingAttributeRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.AddingAttributeRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.AddingAttributeReply> __Marshaller_Catalog_v1_AddingAttributeReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.AddingAttributeReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.AddNewCatalogRecordItemRequest> __Marshaller_Catalog_v1_AddNewCatalogRecordItemRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.AddNewCatalogRecordItemRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.AddNewCatalogRecordItemReply> __Marshaller_Catalog_v1_AddNewCatalogRecordItemReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.AddNewCatalogRecordItemReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.AddCatalogRecordItemRequest> __Marshaller_Catalog_v1_AddCatalogRecordItemRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.AddCatalogRecordItemRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.AddCatalogRecordItemReply> __Marshaller_Catalog_v1_AddCatalogRecordItemReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.AddCatalogRecordItemReply.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.GetCatalogItemRequest> __Marshaller_Catalog_v1_GetCatalogItemRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.GetCatalogItemRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Catalog.gRPC.v1.GetCatalogItemReply> __Marshaller_Catalog_v1_GetCatalogItemReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Catalog.gRPC.v1.GetCatalogItemReply.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Catalog.gRPC.v1.CatalogCreationRequest, global::Catalog.gRPC.v1.CatalogCreationReqply> __Method_CreateCatalod = new grpc::Method<global::Catalog.gRPC.v1.CatalogCreationRequest, global::Catalog.gRPC.v1.CatalogCreationReqply>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "CreateCatalod",
        __Marshaller_Catalog_v1_CatalogCreationRequest,
        __Marshaller_Catalog_v1_CatalogCreationReqply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Catalog.gRPC.v1.AddingAttributeRequest, global::Catalog.gRPC.v1.AddingAttributeReply> __Method_AddAttribute = new grpc::Method<global::Catalog.gRPC.v1.AddingAttributeRequest, global::Catalog.gRPC.v1.AddingAttributeReply>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "AddAttribute",
        __Marshaller_Catalog_v1_AddingAttributeRequest,
        __Marshaller_Catalog_v1_AddingAttributeReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Catalog.gRPC.v1.AddNewCatalogRecordItemRequest, global::Catalog.gRPC.v1.AddNewCatalogRecordItemReply> __Method_AddNewCatalogRecordItem = new grpc::Method<global::Catalog.gRPC.v1.AddNewCatalogRecordItemRequest, global::Catalog.gRPC.v1.AddNewCatalogRecordItemReply>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "AddNewCatalogRecordItem",
        __Marshaller_Catalog_v1_AddNewCatalogRecordItemRequest,
        __Marshaller_Catalog_v1_AddNewCatalogRecordItemReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Catalog.gRPC.v1.AddCatalogRecordItemRequest, global::Catalog.gRPC.v1.AddCatalogRecordItemReply> __Method_AddCatalogRecordItem = new grpc::Method<global::Catalog.gRPC.v1.AddCatalogRecordItemRequest, global::Catalog.gRPC.v1.AddCatalogRecordItemReply>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "AddCatalogRecordItem",
        __Marshaller_Catalog_v1_AddCatalogRecordItemRequest,
        __Marshaller_Catalog_v1_AddCatalogRecordItemReply);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Catalog.gRPC.v1.GetCatalogItemRequest, global::Catalog.gRPC.v1.GetCatalogItemReply> __Method_GetCatalogItem = new grpc::Method<global::Catalog.gRPC.v1.GetCatalogItemRequest, global::Catalog.gRPC.v1.GetCatalogItemReply>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "GetCatalogItem",
        __Marshaller_Catalog_v1_GetCatalogItemRequest,
        __Marshaller_Catalog_v1_GetCatalogItemReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Catalog.gRPC.v1.CatalogsReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of CatalogService</summary>
    [grpc::BindServiceMethod(typeof(CatalogService), "BindService")]
    public abstract partial class CatalogServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task CreateCatalod(grpc::IAsyncStreamReader<global::Catalog.gRPC.v1.CatalogCreationRequest> requestStream, grpc::IServerStreamWriter<global::Catalog.gRPC.v1.CatalogCreationReqply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task AddAttribute(grpc::IAsyncStreamReader<global::Catalog.gRPC.v1.AddingAttributeRequest> requestStream, grpc::IServerStreamWriter<global::Catalog.gRPC.v1.AddingAttributeReply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task AddNewCatalogRecordItem(grpc::IAsyncStreamReader<global::Catalog.gRPC.v1.AddNewCatalogRecordItemRequest> requestStream, grpc::IServerStreamWriter<global::Catalog.gRPC.v1.AddNewCatalogRecordItemReply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task AddCatalogRecordItem(grpc::IAsyncStreamReader<global::Catalog.gRPC.v1.AddCatalogRecordItemRequest> requestStream, grpc::IServerStreamWriter<global::Catalog.gRPC.v1.AddCatalogRecordItemReply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task GetCatalogItem(grpc::IAsyncStreamReader<global::Catalog.gRPC.v1.GetCatalogItemRequest> requestStream, grpc::IServerStreamWriter<global::Catalog.gRPC.v1.GetCatalogItemReply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(CatalogServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreateCatalod, serviceImpl.CreateCatalod)
          .AddMethod(__Method_AddAttribute, serviceImpl.AddAttribute)
          .AddMethod(__Method_AddNewCatalogRecordItem, serviceImpl.AddNewCatalogRecordItem)
          .AddMethod(__Method_AddCatalogRecordItem, serviceImpl.AddCatalogRecordItem)
          .AddMethod(__Method_GetCatalogItem, serviceImpl.GetCatalogItem).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, CatalogServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreateCatalod, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Catalog.gRPC.v1.CatalogCreationRequest, global::Catalog.gRPC.v1.CatalogCreationReqply>(serviceImpl.CreateCatalod));
      serviceBinder.AddMethod(__Method_AddAttribute, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Catalog.gRPC.v1.AddingAttributeRequest, global::Catalog.gRPC.v1.AddingAttributeReply>(serviceImpl.AddAttribute));
      serviceBinder.AddMethod(__Method_AddNewCatalogRecordItem, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Catalog.gRPC.v1.AddNewCatalogRecordItemRequest, global::Catalog.gRPC.v1.AddNewCatalogRecordItemReply>(serviceImpl.AddNewCatalogRecordItem));
      serviceBinder.AddMethod(__Method_AddCatalogRecordItem, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Catalog.gRPC.v1.AddCatalogRecordItemRequest, global::Catalog.gRPC.v1.AddCatalogRecordItemReply>(serviceImpl.AddCatalogRecordItem));
      serviceBinder.AddMethod(__Method_GetCatalogItem, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Catalog.gRPC.v1.GetCatalogItemRequest, global::Catalog.gRPC.v1.GetCatalogItemReply>(serviceImpl.GetCatalogItem));
    }

  }
}
#endregion
