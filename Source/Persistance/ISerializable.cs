namespace STRV.Variables.Persistance
{
    /// Anything implementing this interface can be serialized using Value Persistor 
    public interface ISerializable
    {
        // Returns key used for serialization
        string GetKey();
        
        // Returns self represented as a string
        string GetStringValue();

        // Initializes itself with string value
        void SetStringValue(string value);
    }
}