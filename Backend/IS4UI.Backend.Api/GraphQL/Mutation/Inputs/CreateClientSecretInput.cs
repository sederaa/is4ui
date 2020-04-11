using System;

public class CreateClientSecretInput
{
    public string Description { get; set; }
    public string Value { get; set; }
    public DateTime? Expiration { get; set; }
    public string Type { get; set; }
}
