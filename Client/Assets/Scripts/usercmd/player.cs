//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Proto/MyDemo.proto
namespace usercmd
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoginC2SMsg")]
  public partial class LoginC2SMsg : global::ProtoBuf.IExtensible
  {
    public LoginC2SMsg() {}
    
    private string _name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoginS2CMsg")]
  public partial class LoginS2CMsg : global::ProtoBuf.IExtensible
  {
    public LoginS2CMsg() {}
    
    private uint _playerId = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"playerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint playerId
    {
      get { return _playerId; }
      set { _playerId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"DemoTypeCmd")]
    public enum DemoTypeCmd
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"LoginReq", Value=1)]
      LoginReq = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"LoginRes", Value=2)]
      LoginRes = 2
    }
  
}