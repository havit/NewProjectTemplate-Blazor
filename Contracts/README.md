# Contracts

Assembly used by both *Web.Server* stack and *Web.Client* stack. It is being **transferred to the browser!**
* **MUST NOT** reference any expensive dependencies (*Facades*, *Services*, *DataLayer*, *Model*, *Entity*)
* **MUST NOT** expose any secrets

### Contents
* interfaces for *Facades* (exposed to Web.Client via gRPC stack)
  * do not have to be decorated with `[ServiceContract]`
* DTOs for gRPC communication (Requests, Results)
  * do not have to be decorated with `[DataContract]` nor `[DataMember]` attributes
  * except **generics** which are not included automatically and **HAVE** to use `[ProtoContract]` and `[ProtoMember]` attributes
    * do not use `[DataContract]` nor `[DataMember]`, these tend to be trimmed out by the linker