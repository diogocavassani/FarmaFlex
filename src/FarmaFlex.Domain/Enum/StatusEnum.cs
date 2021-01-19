using System.ComponentModel;

namespace APIFarmaFlex.Domain.Enum
{
    public enum StatusEnum : byte
    {

        [Description("Ativo")]
        Ativo = 1,

        [Description("Inativo")]
        Inativo = 2,
    }
}
